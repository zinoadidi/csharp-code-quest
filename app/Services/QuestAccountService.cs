using System.Text.Json;

namespace app.Services;

/// <summary>
/// Server-backed student accounts on the generic-crud backend: profiles,
/// cross-device progress, and per-user events. localStorage stays the
/// offline cache (see <see cref="GameStateService"/>); this service owns the
/// server side. Uniqueness is enforced with deterministic claim docs
/// (user:name:&lt;norm&gt;, user:email:&lt;norm&gt; → {userId}) via
/// GET-then-PUT plus a verify-read, because the server's PUT is an
/// unconditional upsert with no conditional-create. Every public method is
/// best-effort and never throws: failures surface as Offline/Taken/... so
/// the game keeps working without the server.
/// </summary>
public sealed class QuestAccountService(QuestServerClient store)
{
    public enum AccountStatus
    {
        Ok,
        /// <summary>Server unreachable (or rate-limited) — try again later.</summary>
        Offline,
        UsernameTaken,
        EmailTaken,
        /// <summary>No claim matched the login/restore identifier.</summary>
        NotFound,
        /// <summary>Restore email doesn't match the account's school email.</summary>
        EmailMismatch,
        InvalidInput,
    }

    public sealed record AccountResult(AccountStatus Status, ServerProfile? Profile, GameState? Progress);

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    // ---- registration (new users) -------------------------------------------

    public async Task<AccountResult> RegisterAsync(string username, string email, GameState local)
    {
        username = username.Trim();
        email = email.Trim();
        if (!QuestServerConfig.IsValidUsername(username) || !QuestServerConfig.IsValidEmail(email))
            return new AccountResult(AccountStatus.InvalidInput, null, null);

        var nameClaim = await store.GetDocAsync(QuestServerConfig.NameClaimId(username));
        if (nameClaim.Result == QuestServerClient.StoreResult.Unavailable)
            return new AccountResult(AccountStatus.Offline, null, null);
        if (nameClaim.Result == QuestServerClient.StoreResult.Found)
            return new AccountResult(AccountStatus.UsernameTaken, null, null);

        var emailClaim = await store.GetDocAsync(QuestServerConfig.EmailClaimId(email));
        if (emailClaim.Result == QuestServerClient.StoreResult.Unavailable)
            return new AccountResult(AccountStatus.Offline, null, null);
        if (emailClaim.Result == QuestServerClient.StoreResult.Found)
            return new AccountResult(AccountStatus.EmailTaken, null, null);

        // Both names are free — claim them plus profile + progress. A
        // concurrent device could have claimed in between (no transactions
        // server-side), so verify-read: the winner is whoever the claim
        // points at afterward.
        var userId = Guid.NewGuid().ToString("N");
        var profile = ServerProfile.Create(userId, username, email, isDemo: false);
        if (!await store.PutDocAsync(QuestServerConfig.NameClaimId(username), new { userId }))
            return new AccountResult(AccountStatus.Offline, null, null);
        if (!await store.PutDocAsync(QuestServerConfig.EmailClaimId(email), new { userId }))
            return new AccountResult(AccountStatus.Offline, null, null);

        var verify = await store.GetDocAsync(QuestServerConfig.NameClaimId(username));
        if (verify.Result != QuestServerClient.StoreResult.Found ||
            ClaimUserId(verify.Doc) is not { } winner || winner != userId)
            return new AccountResult(AccountStatus.UsernameTaken, null, null);

        local.UserId = userId;
        local.Username = username;
        local.Email = email;
        local.IsDemo = false;
        await store.PutDocAsync(QuestServerConfig.ProfileId(userId), profile.For(userId, username, email));
        await PushProgressNowAsync(local);
        return new AccountResult(AccountStatus.Ok, profile, null);
    }

    // ---- login (username OR school email) -----------------------------------

    public async Task<AccountResult> LoginAsync(string identifier)
    {
        identifier = identifier.Trim();
        if (identifier.Length == 0)
            return new AccountResult(AccountStatus.InvalidInput, null, null);

        var userId = await ResolveUserIdAsync(identifier);
        if (userId is null)
            return new AccountResult(AccountStatus.NotFound, null, null);
        if (userId == string.Empty)
            return new AccountResult(AccountStatus.Offline, null, null);

        return await LoadAccountAsync(userId);
    }

    // ---- migration (existing local-only users keep their progress) ----------

    /// <summary>
    /// An existing player (local save, no server account yet) links up: their
    /// current local progress becomes the server's initial progress doc. If
    /// the username claim is free it is claimed; if it already points at a
    /// profile with no email (or the same email) it is adopted; otherwise the
    /// name belongs to someone else and this reports <see
    /// cref="AccountStatus.UsernameTaken"/> so they can use server restore
    /// instead of forking the name.
    /// </summary>
    public async Task<AccountResult> MigrateExistingAsync(string username, string email, GameState local)
    {
        username = username.Trim();
        email = email.Trim();
        if (!QuestServerConfig.IsValidUsername(username) || !QuestServerConfig.IsValidEmail(email))
            return new AccountResult(AccountStatus.InvalidInput, null, null);

        var nameClaim = await store.GetDocAsync(QuestServerConfig.NameClaimId(username));
        if (nameClaim.Result == QuestServerClient.StoreResult.Unavailable)
            return new AccountResult(AccountStatus.Offline, null, null);

        if (nameClaim.Result == QuestServerClient.StoreResult.Found)
        {
            var claimedId = ClaimUserId(nameClaim.Doc);
            if (claimedId is null)
                return new AccountResult(AccountStatus.Offline, null, null);
            var existing = await LoadAccountAsync(claimedId);
            if (existing.Status != AccountStatus.Ok || existing.Profile is null)
                return new AccountResult(AccountStatus.Offline, null, null);
            var profile = existing.Profile;
            // Someone else's finished account — don't fork it.
            if (!string.IsNullOrEmpty(profile.Email) &&
                !string.Equals(profile.Email, email, StringComparison.OrdinalIgnoreCase))
                return new AccountResult(AccountStatus.UsernameTaken, null, null);
            // Same owner (or an email-less stub): attach the school email and
            // keep whichever progress is further along.
            if (string.IsNullOrEmpty(profile.Email))
            {
                var emailClaim = await store.GetDocAsync(QuestServerConfig.EmailClaimId(email));
                if (emailClaim.Result == QuestServerClient.StoreResult.Unavailable)
                    return new AccountResult(AccountStatus.Offline, null, null);
                if (emailClaim.Result == QuestServerClient.StoreResult.Found &&
                    ClaimUserId(emailClaim.Doc) != claimedId)
                    return new AccountResult(AccountStatus.EmailTaken, null, null);
                await store.PutDocAsync(QuestServerConfig.EmailClaimId(email), new { userId = claimedId });
                profile = profile.WithEmail(email);
                await store.PutDocAsync(QuestServerConfig.ProfileId(claimedId), profile);
            }
            return await AdoptAsync(profile, local, existing.Progress);
        }

        // Free name — also make sure the email isn't taken before claiming.
        var free = await store.GetDocAsync(QuestServerConfig.EmailClaimId(email));
        if (free.Result == QuestServerClient.StoreResult.Unavailable)
            return new AccountResult(AccountStatus.Offline, null, null);
        if (free.Result == QuestServerClient.StoreResult.Found)
            return new AccountResult(AccountStatus.EmailTaken, null, null);

        var userId = Guid.NewGuid().ToString("N");
        var created = ServerProfile.Create(userId, username, email, isDemo: false);
        if (!await store.PutDocAsync(QuestServerConfig.NameClaimId(username), new { userId }))
            return new AccountResult(AccountStatus.Offline, null, null);
        var verify = await store.GetDocAsync(QuestServerConfig.NameClaimId(username));
        if (verify.Result != QuestServerClient.StoreResult.Found || ClaimUserId(verify.Doc) != userId)
            return new AccountResult(AccountStatus.UsernameTaken, null, null);
        await store.PutDocAsync(QuestServerConfig.EmailClaimId(email), new { userId });
        await store.PutDocAsync(QuestServerConfig.ProfileId(userId), created);
        local.UserId = userId;
        local.Username = username;
        local.Email = email;
        local.IsDemo = false;
        await PushProgressNowAsync(local);
        return new AccountResult(AccountStatus.Ok, created, null);
    }

    // ---- server restore (cross-device, replaces the manual code flow) -------

    /// <summary>
    /// Restores the account matching <paramref name="identifier"/> (username
    /// or school email). When the account already has a school email and the
    /// caller supplies one that differs, this refuses with
    /// <see cref="AccountStatus.EmailMismatch"/> — the email is the second
    /// factor proving the device belongs to the same student. When the
    /// account has no email yet (pre-email users), the supplied school email
    /// is mapped onto the account (after its own uniqueness check).
    /// </summary>
    public async Task<AccountResult> RestoreAsync(string identifier, string email)
    {
        identifier = identifier.Trim();
        email = email.Trim();
        if (identifier.Length == 0)
            return new AccountResult(AccountStatus.InvalidInput, null, null);

        var userId = await ResolveUserIdAsync(identifier);
        if (userId is null)
            return new AccountResult(AccountStatus.NotFound, null, null);
        if (userId == string.Empty)
            return new AccountResult(AccountStatus.Offline, null, null);

        var loaded = await LoadAccountAsync(userId);
        if (loaded.Status != AccountStatus.Ok || loaded.Profile is null)
            return loaded;
        var profile = loaded.Profile;

        if (string.IsNullOrEmpty(profile.Email))
        {
            if (email.Length == 0)
                return new AccountResult(AccountStatus.InvalidInput, profile, loaded.Progress);
            if (!QuestServerConfig.IsValidEmail(email))
                return new AccountResult(AccountStatus.InvalidInput, profile, loaded.Progress);
            var emailClaim = await store.GetDocAsync(QuestServerConfig.EmailClaimId(email));
            if (emailClaim.Result == QuestServerClient.StoreResult.Unavailable)
                return new AccountResult(AccountStatus.Offline, null, null);
            if (emailClaim.Result == QuestServerClient.StoreResult.Found &&
                ClaimUserId(emailClaim.Doc) != userId)
                return new AccountResult(AccountStatus.EmailTaken, null, null);
            await store.PutDocAsync(QuestServerConfig.EmailClaimId(email), new { userId });
            profile = profile.WithEmail(email);
            await store.PutDocAsync(QuestServerConfig.ProfileId(userId), profile);
        }
        else if (email.Length > 0 &&
                 !string.Equals(profile.Email, email, StringComparison.OrdinalIgnoreCase))
        {
            return new AccountResult(AccountStatus.EmailMismatch, profile, null);
        }

        return new AccountResult(AccountStatus.Ok, profile, loaded.Progress);
    }

    // ---- progress + event sync (offline-first, debounced) -------------------

    private DateTime _lastPush = DateTime.MinValue;
    private GameState? _pendingPush;
    private bool _flushScheduled;

    /// <summary>
    /// Best-effort push of the player's progress doc. At most one PUT per 10
    /// seconds; a newer state arriving inside the window is flushed right
    /// after. Never throws; a no-op until the player has a server account.
    /// </summary>
    public void PushProgress(GameState state)
    {
        if (string.IsNullOrEmpty(state.UserId)) return;
        lock (this)
        {
            _pendingPush = state;
            if (DateTime.UtcNow - _lastPush < TimeSpan.FromSeconds(10))
            {
                if (_flushScheduled) return;
                _flushScheduled = true;
                _ = FlushPendingAsync();
                return;
            }
        }
        _ = PushProgressNowAsync(state);
    }

    private async Task FlushPendingAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
        GameState? next;
        lock (this)
        {
            next = _pendingPush;
            _pendingPush = null;
            _flushScheduled = false;
        }
        if (next is not null) await PushProgressNowAsync(next);
    }

    public async Task PushProgressNowAsync(GameState state)
    {
        if (string.IsNullOrEmpty(state.UserId)) return;
        _lastPush = DateTime.UtcNow;
        try
        {
            await store.PutDocAsync(
                QuestServerConfig.ProgressId(state.UserId),
                new { updatedAt = DateTime.UtcNow, state });
        }
        catch
        {
            // Best-effort only.
        }
    }

    private readonly List<ServerEvent> _eventQueue = new();
    private bool _eventFlushScheduled;

    /// <summary>
    /// Mirrors one analytics event (the same ones going to Clarity) onto the
    /// player's server events doc, capped at the last 200 entries. Fire-and-
    /// forget; never throws; a no-op until the player has a server account.
    /// </summary>
    public void RecordEvent(string? userId, string name, IReadOnlyDictionary<string, string>? tags = null)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(name)) return;
        lock (_eventQueue)
        {
            _eventQueue.Add(new ServerEvent(name, DateTime.UtcNow, tags));
            if (_eventQueue.Count > 400)
                _eventQueue.RemoveRange(0, _eventQueue.Count - 400);
            if (_eventFlushScheduled) return;
            _eventFlushScheduled = true;
        }
        _ = FlushEventsAsync(userId);
    }

    private async Task FlushEventsAsync(string userId)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        List<ServerEvent> batch;
        lock (_eventQueue)
        {
            batch = new List<ServerEvent>(_eventQueue);
            _eventQueue.Clear();
            _eventFlushScheduled = false;
        }
        try
        {
            var log = await LoadEventsAsync(userId);
            if (log is null) return; // Offline — batch is dropped; next events re-read the doc.
            log.AddRange(batch);
            if (log.Count > 200) log.RemoveRange(0, log.Count - 200);
            await store.PutDocAsync(
                QuestServerConfig.EventsId(userId),
                new EventsDoc { Events = log, UpdatedAt = DateTime.UtcNow });
        }
        catch
        {
            // Best-effort only.
        }
    }

    /// <summary>
    /// One-time backfill for players whose progress predates the server
    /// integration: appends <paramref name="backlog"/> (a summary built from
    /// their local save, see GameStateService.BuildBacklog) to their server
    /// events doc the first time this version runs. Skips when the doc
    /// already carries a backlog marker, so re-installs and second devices
    /// never duplicate it. Returns false when the server couldn't be reached
    /// (the caller retries later); never throws.
    /// </summary>
    public async Task<bool> PushBacklogOnceAsync(string userId, IReadOnlyList<ServerEvent> backlog)
    {
        if (string.IsNullOrEmpty(userId)) return true;
        try
        {
            var log = await LoadEventsAsync(userId);
            if (log is null) return false;
            if (log.Any(e => e.Name == BacklogMarker)) return true;
            log.AddRange(backlog);
            if (backlog.All(e => e.Name != BacklogMarker))
                log.Add(new ServerEvent(BacklogMarker, DateTime.UtcNow, null));
            if (log.Count > 200) log.RemoveRange(0, log.Count - 200);
            return await store.PutDocAsync(
                QuestServerConfig.EventsId(userId),
                new EventsDoc { Events = log, UpdatedAt = DateTime.UtcNow });
        }
        catch
        {
            return false;
        }
    }

    public const string BacklogMarker = "progress_backlog";

    /// <summary>
    /// Newest-first tail of a player's server events (for the local
    /// owner-only admin view). Empty (never throws) when unreachable or
    /// when the player has no events yet.
    /// </summary>
    public async Task<IReadOnlyList<ServerEvent>> GetRecentEventsAsync(string userId, int count = 30)
    {
        try
        {
            var log = await LoadEventsAsync(userId);
            if (log is null || log.Count == 0) return Array.Empty<ServerEvent>();
            return log.TakeLast(Math.Max(1, count)).Reverse().ToList();
        }
        catch
        {
            return Array.Empty<ServerEvent>();
        }
    }

    /// <returns>The current server events log, or null when unreachable.</returns>
    private async Task<List<ServerEvent>?> LoadEventsAsync(string userId)
    {
        var existing = await store.GetDocAsync(QuestServerConfig.EventsId(userId));
        if (existing.Result == QuestServerClient.StoreResult.Unavailable)
            return null;
        var log = new List<ServerEvent>();
        if (existing.Result == QuestServerClient.StoreResult.Found && existing.Doc.HasValue)
        {
            try
            {
                var prev = JsonSerializer.Deserialize<EventsDoc>(
                    existing.Doc.Value.GetRawText(), JsonOpts);
                if (prev?.Events is not null) log.AddRange(prev.Events);
            }
            catch
            {
                // Corrupt doc — start fresh rather than dropping new events.
            }
        }
        return log;
    }

    // ---- internals ----------------------------------------------------------

    /// <returns>
    /// userId for a username-or-email identifier; null when no claim exists;
    /// empty string when the server couldn't be reached.
    /// </returns>
    private async Task<string?> ResolveUserIdAsync(string identifier)
    {
        var byName = await store.GetDocAsync(QuestServerConfig.NameClaimId(identifier));
        if (byName.Result == QuestServerClient.StoreResult.Found)
            return ClaimUserId(byName.Doc);
        var byEmail = await store.GetDocAsync(QuestServerConfig.EmailClaimId(identifier));
        if (byEmail.Result == QuestServerClient.StoreResult.Found)
            return ClaimUserId(byEmail.Doc);
        if (byName.Result == QuestServerClient.StoreResult.Unavailable ||
            byEmail.Result == QuestServerClient.StoreResult.Unavailable)
            return string.Empty;
        return null;
    }

    private async Task<AccountResult> LoadAccountAsync(string userId)
    {
        var profileGet = await store.GetDocAsync(QuestServerConfig.ProfileId(userId));
        if (profileGet.Result != QuestServerClient.StoreResult.Found || !profileGet.Doc.HasValue)
            return new AccountResult(
                profileGet.Result == QuestServerClient.StoreResult.Unavailable
                    ? AccountStatus.Offline : AccountStatus.NotFound, null, null);
        ServerProfile? profile;
        try
        {
            profile = JsonSerializer.Deserialize<ServerProfile>(
                profileGet.Doc.Value.GetRawText(), JsonOpts);
        }
        catch
        {
            return new AccountResult(AccountStatus.Offline, null, null);
        }
        if (profile is null)
            return new AccountResult(AccountStatus.NotFound, null, null);

        GameState? progress = null;
        var progressGet = await store.GetDocAsync(QuestServerConfig.ProgressId(userId));
        if (progressGet.Result == QuestServerClient.StoreResult.Found && progressGet.Doc.HasValue)
        {
            try
            {
                var doc = progressGet.Doc.Value;
                var raw = doc.TryGetProperty("state", out var state) ? state.GetRawText() : doc.GetRawText();
                progress = JsonSerializer.Deserialize<GameState>(raw, JsonOpts);
            }
            catch
            {
                progress = null; // Corrupt progress — profile still loads.
            }
        }
        return new AccountResult(AccountStatus.Ok, profile, progress);
    }

    /// <summary>
    /// Adopts a server account for this device, keeping whichever side
    /// (local or server) is further along so migration never wipes progress.
    /// </summary>
    private async Task<AccountResult> AdoptAsync(ServerProfile profile, GameState local, GameState? server)
    {
        var localDone = local.Tasks.Values.Count(t => t.Completed);
        var serverDone = server?.Tasks.Values.Count(t => t.Completed) ?? -1;
        if (server is not null && serverDone >= localDone)
        {
            server.UserId = profile.UserId;
            server.Email = profile.Email;
            server.Username = profile.Username;
            server.IsDemo = profile.IsDemo;
            return new AccountResult(AccountStatus.Ok, profile, server);
        }
        local.UserId = profile.UserId;
        local.Username = profile.Username;
        local.Email = profile.Email;
        local.IsDemo = profile.IsDemo;
        await PushProgressNowAsync(local);
        return new AccountResult(AccountStatus.Ok, profile, null);
    }

    private static string? ClaimUserId(JsonElement? doc)
    {
        if (!doc.HasValue) return null;
        try
        {
            if (doc.Value.TryGetProperty("userId", out var id) &&
                id.ValueKind == JsonValueKind.String)
                return id.GetString();
        }
        catch
        {
            // Malformed claim — treat as missing below.
        }
        return null;
    }

    public sealed class ServerProfile
    {
        public string UserId { get; set; } = "";
        public string Username { get; set; } = "";
        public string? Email { get; set; }
        public bool IsDemo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static ServerProfile Create(string userId, string username, string? email, bool isDemo) =>
            new()
            {
                UserId = userId,
                Username = username,
                Email = email,
                IsDemo = isDemo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

        public ServerProfile WithEmail(string email) =>
            new()
            {
                UserId = UserId,
                Username = Username,
                Email = email,
                IsDemo = IsDemo,
                CreatedAt = CreatedAt,
                UpdatedAt = DateTime.UtcNow,
            };

        /// <summary>Canonical doc shape (norm keys keep lookups exact).</summary>
        public object For(string userId, string username, string? email) => new
        {
            userId,
            username,
            usernameNorm = QuestServerConfig.NormUsername(username),
            email,
            emailNorm = email is null ? null : QuestServerConfig.NormEmail(email),
            isDemo = IsDemo,
            createdAt = CreatedAt,
            updatedAt = DateTime.UtcNow,
        };
    }

    public sealed record ServerEvent(string Name, DateTime At, IReadOnlyDictionary<string, string>? Tags);

    private sealed class EventsDoc
    {
        public List<ServerEvent> Events { get; set; } = new();
        public DateTime UpdatedAt { get; set; }
    }
}
