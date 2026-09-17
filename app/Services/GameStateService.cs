using System.Text.Json;
using app.Content;
using Microsoft.JSInterop;

namespace app.Services;

/// <summary>
/// Owns the player's persisted <see cref="GameState"/> (points, streak,
/// per-task progress, unlocked achievements) and every rule for how it changes.
/// Persisted to the browser's localStorage as one JSON blob — matches the
/// original prototype's approach (see ../../REQUIREMENTS.md), no accounts/
/// backend involved. Register as scoped/singleton in Program.cs and call
/// <see cref="LoadAsync"/> once before first use.
/// </summary>
public sealed class GameStateService(IJSRuntime js)
{
    private const string StorageKey = "csharpCodeQuestState";

    // A failed run doesn't zero the streak on the very first mistake — a
    // student gets this many failed attempts on a task, in a row, without an
    // intervening pass anywhere, before the streak actually breaks. See
    // content/GameSystems.md's "give some slack" note.
    private const int FailureTolerance = 2;

    // A task's final hint tier is the full working solution (see
    // HintTier.IsSolution) — showing it on demand with zero attempts required
    // defeats the point of hinting at all. Require this many failed Run Code
    // attempts on the SPECIFIC task before that last tier can be revealed;
    // earlier, softer hint tiers stay freely available the whole time.
    public const int SolutionUnlockAttempts = 3;

    public GameState State { get; private set; } = new();

    public async Task LoadAsync()
    {
        try
        {
            var json = await js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                State = JsonSerializer.Deserialize<GameState>(json) ?? new GameState();
            }
        }
        catch
        {
            // localStorage unavailable (private browsing, etc.) — keep default
            // in-memory state; the session just won't persist across reloads.
        }

        // A returning player's Username survives in localStorage, but each
        // page load/reload starts a BRAND NEW Clarity session recording —
        // clarity('identify', ...) is what tells Clarity that new session
        // belongs to the same person as their earlier ones, so it must be
        // re-called here too, not just once at first-time username entry
        // (see SetUsernameAsync). Skipping this was the bug: only a player's
        // very first session was ever "identified," so every return visit
        // showed up as a separate, unlinked anonymous session in Clarity.
        await TagClaritySessionAsync();

        // index.html's inline pre-boot script already applies the saved theme
        // before Blazor even starts (avoiding a flash of the wrong theme), so
        // this is mostly a safety-net re-apply for the rare case that script
        // couldn't read localStorage but this call can (or vice versa).
        await ApplyThemeAsync();

        // Best available "when did this player start" proxy — sets once,
        // ever, on whichever LoadAsync call finds it still unset (their very
        // first session, or their first session after this field shipped).
        // Paired with CompletedAt so Pages/Certificate.razor can show "done
        // in N days."
        if (State.StartedAt is null)
        {
            State.StartedAt = DateTime.UtcNow;
            await SaveAsync();
        }
    }

    /// <summary>
    /// Sets documentElement's data-theme attribute (or clears it for
    /// "system") to match <see cref="GameState.Theme"/> — see
    /// wwwroot/js/theme.js. Best-effort/non-throwing, same as every other JS
    /// interop call here.
    /// </summary>
    private async Task ApplyThemeAsync()
    {
        try
        {
            await js.InvokeVoidAsync("themeManager.apply", State.Theme);
        }
        catch
        {
            // theme.js not loaded yet / JS interop unavailable — non-fatal,
            // the page still renders in whichever theme the CSS defaults to.
        }
    }

    /// <summary>
    /// Sets the player's theme ("light", "dark", or "system") and applies it
    /// immediately — see <see cref="GameState.Theme"/>.
    /// </summary>
    public async Task SetThemeAsync(string theme)
    {
        State.Theme = theme;
        await SaveAsync();
        await ApplyThemeAsync();
    }

    /// <summary>
    /// Advances the theme System -&gt; Light -&gt; Dark -&gt; System. One shared
    /// cycle order (rather than duplicating it in every page with a theme
    /// toggle button) so Home.razor's and Stats.razor's toggles can never
    /// drift out of sync with each other.
    /// </summary>
    public Task CycleThemeAsync() => SetThemeAsync(State.Theme switch
    {
        "light" => "dark",
        "dark" => "system",
        _ => "light",
    });

    public string ThemeIcon => State.Theme switch
    {
        "light" => "☀️",
        "dark" => "🌙",
        _ => "🖥️",
    };

    public string ThemeLabel => State.Theme switch
    {
        "light" => "Light theme (tap for dark)",
        "dark" => "Dark theme (tap to follow system)",
        _ => "System theme (tap for light)",
    };

    private async Task SaveAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(State);
            await js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
        catch
        {
            // Same as above — persistence is best-effort.
        }
    }

    /// <summary>
    /// Stores the player's chosen name and tags the current Microsoft Clarity
    /// session with it (see wwwroot/index.html for the Clarity snippet). This
    /// is purely so session recordings/analytics in the Clarity dashboard can
    /// be identified by name — Clarity has no API to read data back into the
    /// page, so this can't power an in-app leaderboard (see REQUIREMENTS.md's
    /// "Clarity integration" note for why).
    /// </summary>
    public async Task SetUsernameAsync(string username)
    {
        State.Username = username;
        await SaveAsync();
        await TagClaritySessionAsync();
    }

    public async Task SetSoundEnabledAsync(bool enabled)
    {
        State.SoundEnabled = enabled;
        await SaveAsync();
    }

    public Task ToggleSoundAsync() => SetSoundEnabledAsync(!State.SoundEnabled);

    /// <summary>
    /// Plays one of the synthesized sound effects in wwwroot/js/sfx.js, unless
    /// the player has muted sound effects. This is the single gateway every
    /// caller should go through — pass a <see cref="SoundEffect"/> constant
    /// (never a raw string) so the whole catalog stays in one place. Best-
    /// effort/non-throwing — Web Audio being blocked or unsupported shouldn't
    /// break gameplay.
    /// </summary>
    public async Task PlaySoundAsync(string name)
    {
        if (!State.SoundEnabled) return;
        try
        {
            await js.InvokeVoidAsync("sfx.play", name);
        }
        catch
        {
            // Web Audio unavailable — non-fatal, sound is a nicety.
        }
    }

    private async Task TagClaritySessionAsync()
    {
        if (string.IsNullOrWhiteSpace(State.Username)) return;
        try
        {
            // clarity(...) is the global function installed by the tracking
            // snippet in wwwroot/index.html; calling an arbitrary global JS
            // function by name is a normal Blazor JS-interop call.
            //
            // identify(customId, customSessionId, customPageId, friendlyName):
            // the username is used as BOTH the stable customId (what actually
            // links every session — this device, another device, tomorrow —
            // back to the same person in Clarity's People view) and the
            // friendlyName (so that view shows the readable username instead
            // of a hash). Passing null for the two middle positional args
            // leaves Clarity's own per-session/per-page ids untouched.
            await js.InvokeVoidAsync("clarity", "identify", State.Username, null, null, State.Username);
            await js.InvokeVoidAsync("clarity", "set", "username", State.Username);
            await js.InvokeVoidAsync("clarity", "set", "points", State.Points.ToString());
            await js.InvokeVoidAsync("clarity", "set", "streak", State.Streak.ToString());
        }
        catch
        {
            // Clarity script blocked/failed to load (ad blocker, offline) —
            // non-fatal, the game itself doesn't depend on analytics.
        }
    }

    /// <summary>
    /// Re-tags the current Clarity session with the latest points/streak so
    /// the dashboard can be filtered/segmented by progress. Best-effort, fires
    /// after any state-changing action; never throws into game logic.
    /// </summary>
    private async Task ReportProgressToClarityAsync()
    {
        if (string.IsNullOrWhiteSpace(State.Username)) return;
        try
        {
            await js.InvokeVoidAsync("clarity", "set", "points", State.Points.ToString());
            await js.InvokeVoidAsync("clarity", "set", "streak", State.Streak.ToString());
        }
        catch
        {
            // Same as above — analytics tagging is best-effort only.
        }
    }

    /// <summary>
    /// Fires one custom Clarity event, tagged with the given key/value pairs
    /// (e.g. level/task ids) via clarity('set', ...) right before it, so the
    /// event shows up filterable in the dashboard. Best-effort/non-throwing —
    /// every player-action tracking call in this file goes through here so
    /// the site owner can see hint usage, solution copies, quiz outcomes,
    /// visual replays, achievement unlocks, and level completions in Clarity,
    /// without any of it depending on Clarity actually being reachable.
    /// </summary>
    private async Task TrackClarityEventAsync(string eventName, params (string Key, string Value)[] tags)
    {
        try
        {
            foreach (var (key, value) in tags)
            {
                await js.InvokeVoidAsync("clarity", "set", key, value);
            }
            await js.InvokeVoidAsync("clarity", "event", eventName);
        }
        catch
        {
            // Clarity unavailable — non-fatal, see TagClaritySessionAsync above.
        }
    }

    /// <summary>
    /// Turns the current save into a plain string a player can copy out and
    /// paste in on another device — the only "sync" this app has, since
    /// there's no backend/account system (see class doc). Just base64 of the
    /// same JSON persisted to localStorage; nothing fancy, easy to eyeball as
    /// "looks like a real token" without actually being human-readable.
    /// </summary>
    public string ExportState()
    {
        var json = JsonSerializer.Serialize(State);
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
    }

    public enum ImportResult
    {
        Success,
        InvalidToken,
        UsernameMismatch,
    }

    /// <summary>
    /// Restores a save produced by <see cref="ExportState"/>. Requires the
    /// player to also type the username the token was exported under —
    /// purely a "did you mean to do this / is this actually your token"
    /// sanity check, not real authentication (there's no account system to
    /// authenticate against; anyone with the token AND the right username
    /// string can restore it, same as anyone with physical access to a
    /// browser's localStorage already could).
    /// </summary>
    public async Task<ImportResult> ImportStateAsync(string token, string confirmUsername)
    {
        GameState imported;
        try
        {
            var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token.Trim()));
            imported = JsonSerializer.Deserialize<GameState>(json) ?? throw new JsonException();
        }
        catch (Exception ex) when (ex is FormatException or JsonException or ArgumentException)
        {
            return ImportResult.InvalidToken;
        }

        if (string.IsNullOrWhiteSpace(imported.Username) ||
            !string.Equals(imported.Username.Trim(), confirmUsername.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            return ImportResult.UsernameMismatch;
        }

        State = imported;
        await SaveAsync();
        await TagClaritySessionAsync();
        await ApplyThemeAsync();
        return ImportResult.Success;
    }

    /// <summary>
    /// Wipes this browser's save and starts over from a brand-new
    /// <see cref="GameState"/> — the username prompt reappears on next load,
    /// same as a first-ever visit. Does NOT touch Clarity: the old username's
    /// session history stays exactly as it was there (Clarity has no delete
    /// API for this app to call, and no reason to use one — see
    /// <see cref="TagClaritySessionAsync"/>, which simply won't tag anything
    /// further until a new username is set). This is local-only and
    /// irreversible for THIS browser, so callers should point the player at
    /// <see cref="ExportState"/> first if they might want this progress back.
    /// </summary>
    public async Task ResetAsync()
    {
        State = new GameState { StartedAt = DateTime.UtcNow };
        await SaveAsync();
        await ApplyThemeAsync();
    }

    public bool IsTaskCompleted(int levelId, int taskId) =>
        State.Tasks.TryGetValue(GameState.TaskKey(levelId, taskId), out var p) && p.Completed;

    public int HintsRevealedFor(int levelId, int taskId) =>
        State.Tasks.TryGetValue(GameState.TaskKey(levelId, taskId), out var p) ? p.HintsRevealed : 0;

    public int FailedAttemptsFor(int levelId, int taskId) =>
        State.Tasks.TryGetValue(GameState.TaskKey(levelId, taskId), out var p) ? p.FailedAttempts : 0;

    /// <summary>
    /// Whether the NEXT hint tier that would be revealed for this task is the
    /// solution tier and it's still locked (not enough failed attempts yet).
    /// Home.razor uses this to swap the "Next Hint" button for a locked
    /// message instead of letting the solution be pulled up for free.
    /// </summary>
    public bool IsNextHintLockedSolution(int levelId, GameTask task)
    {
        var revealed = HintsRevealedFor(levelId, task.Id);
        if (revealed >= task.Hints.Count) return false;
        var nextTier = task.Hints[revealed];
        if (!nextTier.IsSolution) return false;
        return FailedAttemptsFor(levelId, task.Id) < SolutionUnlockAttempts;
    }

    public bool IsLevelComplete(Level level) =>
        level.Tasks.Count > 0 && level.Tasks.All(t => IsTaskCompleted(level.Id, t.Id));

    public bool IsQuizPassed(int levelId, int afterTaskId) =>
        State.PassedQuizzes.Contains(GameState.QuizKey(levelId, afterTaskId));

    /// <summary>
    /// The quiz stage (if any, and not already passed) gating progress past
    /// the task with id <paramref name="afterTaskId"/> in this level.
    /// </summary>
    public QuizStage? PendingQuizAfter(Level level, int afterTaskId) =>
        level.QuizStagesOrEmpty.FirstOrDefault(q => q.AfterTaskId == afterTaskId && !IsQuizPassed(level.Id, afterTaskId));

    /// <summary>
    /// Passing a quiz stage can itself unlock an achievement (e.g. quiz-whiz,
    /// quiz-master), so this checks for new achievements the same way
    /// RecordAttemptAsync does — otherwise those unlocks would sit undetected
    /// in UnlockedAchievements until some later, unrelated task completion
    /// happened to run CheckNewAchievements, showing the popup at the wrong
    /// moment (or never, if the quiz was the player's last action).
    /// </summary>
    public async Task<IReadOnlyList<Achievement>> RecordQuizPassedAsync(int levelId, int afterTaskId, IReadOnlyList<Level> levels)
    {
        if (State.PassedQuizzes.Add(GameState.QuizKey(levelId, afterTaskId)))
        {
            await SaveAsync();
        }
        await TrackClarityEventAsync("quiz_passed", ("level", levelId.ToString()), ("quiz", afterTaskId.ToString()));
        var newAchievements = CheckNewAchievements(levels);
        if (newAchievements.Count > 0)
        {
            await SaveAsync();
        }
        return newAchievements;
    }

    /// <summary>
    /// Tracks a quiz attempt that fell short of the pass threshold — Home.razor
    /// calls this when QuizView reports a failed result, purely for the site
    /// owner's own Clarity analytics (e.g. spotting a quiz that's too hard).
    /// </summary>
    public Task RecordQuizFailedAttemptAsync(int levelId, int afterTaskId, int correctCount, int totalCards) =>
        TrackClarityEventAsync(
            "quiz_failed_attempt",
            ("level", levelId.ToString()), ("quiz", afterTaskId.ToString()), ("score", $"{correctCount}/{totalCards}"));

    /// <summary>
    /// A level is playable once the previous level (in list order) is fully
    /// complete; the first level is always unlocked.
    /// </summary>
    public bool IsLevelLocked(IReadOnlyList<Level> levels, int levelIndex)
    {
        if (levelIndex <= 0) return false;
        return !IsLevelComplete(levels[levelIndex - 1]);
    }

    public async Task<int> RevealNextHintAsync(int levelId, GameTask task)
    {
        var key = GameState.TaskKey(levelId, task.Id);
        if (!State.Tasks.TryGetValue(key, out var progress))
        {
            progress = new TaskProgress();
            State.Tasks[key] = progress;
        }

        if (progress.HintsRevealed < task.Hints.Count && !IsNextHintLockedSolution(levelId, task))
        {
            progress.HintsRevealed++;
            var revealedSolution = progress.HintsRevealed == task.Hints.Count && task.Hints[^1].IsSolution;
            if (revealedSolution)
            {
                progress.UsedSolutionHint = true;
            }
            await SaveAsync();
            await TrackClarityEventAsync(
                revealedSolution ? "solution_hint_revealed" : "hint_revealed",
                ("level", levelId.ToString()), ("task", task.Id.ToString()), ("hint_tier", progress.HintsRevealed.ToString()));
        }

        return progress.HintsRevealed;
    }

    /// <summary>
    /// Tracks a student clicking "Copy solution" — Home.razor calls this
    /// alongside the actual clipboard write. Purely an analytics signal (see
    /// TrackClarityEventAsync); doesn't change persisted game state, since
    /// UsedSolutionHint (set when the solution hint tier is revealed, above)
    /// already covers the achievement/points logic that cares about this.
    /// </summary>
    public Task RecordSolutionCopiedAsync(int levelId, int taskId) =>
        TrackClarityEventAsync("solution_copied", ("level", levelId.ToString()), ("task", taskId.ToString()));

    public sealed record AttemptResult(bool Passed, int PointsAwarded, int Streak, IReadOnlyList<Achievement> NewAchievements, bool LevelCompleted, bool QuestCompleted);

    /// <summary>
    /// Records the outcome of one Run click against a task. Points/streak only
    /// move the first time a given task is passed — replaying an already-solved
    /// task is free (encourages tinkering without it becoming a farming loop).
    /// </summary>
    public async Task<AttemptResult> RecordAttemptAsync(int levelId, GameTask task, IReadOnlyList<Level> levels, bool passed)
    {
        var key = GameState.TaskKey(levelId, task.Id);
        if (!State.Tasks.TryGetValue(key, out var progress))
        {
            progress = new TaskProgress();
            State.Tasks[key] = progress;
        }

        var level = levels.FirstOrDefault(l => l.Id == levelId);
        var wasLevelComplete = level is not null && IsLevelComplete(level);

        var pointsAwarded = 0;
        var newlyCompleted = passed && !progress.Completed;

        if (passed)
        {
            State.ConsecutiveFailures = 0;
            if (newlyCompleted)
            {
                progress.Completed = true;
                pointsAwarded = task.Points;
                State.Points += pointsAwarded;
                State.Streak++;
                State.MaxStreak = Math.Max(State.MaxStreak, State.Streak);

                if (progress.HintsRevealed == 0)
                {
                    State.NoHintCompletions++;
                }
                if (progress.UsedSolutionHint)
                {
                    State.ComebackCompletions++;
                }
            }
        }
        else
        {
            State.ConsecutiveFailures++;
            if (State.ConsecutiveFailures > FailureTolerance)
            {
                State.Streak = 0;
            }
            progress.FailedAttempts++;
        }

        if (newlyCompleted)
        {
            RecordDayPlayed();
        }

        var newAchievements = CheckNewAchievements(levels);
        var levelJustCompleted = newlyCompleted && level is not null && !wasLevelComplete && IsLevelComplete(level);
        var questJustCompleted = levelJustCompleted && CheckQuestCompletion(levels);
        await SaveAsync();
        if (newlyCompleted)
        {
            await TrackClarityEventAsync("task_completed", ("level", levelId.ToString()), ("task", task.Id.ToString()));

            if (levelJustCompleted)
            {
                await TrackClarityEventAsync("level_completed", ("level", levelId.ToString()));
            }
            if (questJustCompleted)
            {
                await TrackClarityEventAsync("quest_completed", ("points", State.Points.ToString()));
            }
        }
        await ReportProgressToClarityAsync();
        return new AttemptResult(passed, pointsAwarded, State.Streak, newAchievements, levelJustCompleted, questJustCompleted);
    }

    /// <summary>
    /// Marks today (UTC calendar date) as a day this player completed a task,
    /// and rolls <see cref="GameState.CurrentDayStreak"/>/<see
    /// cref="GameState.MaxDayStreak"/> forward — gamifies coming back on
    /// different days, distinct from the task-completion Streak. A no-op for
    /// every completion after the first one on a given day, so it can't be
    /// farmed by finishing many tasks in one sitting.
    /// </summary>
    private void RecordDayPlayed()
    {
        var today = DateTime.UtcNow.Date;
        var todayKey = today.ToString("yyyy-MM-dd");
        if (State.DaysPlayed.Contains(todayKey)) return;

        var yesterdayKey = today.AddDays(-1).ToString("yyyy-MM-dd");
        State.CurrentDayStreak = State.DaysPlayed.Contains(yesterdayKey) ? State.CurrentDayStreak + 1 : 1;
        State.MaxDayStreak = Math.Max(State.MaxDayStreak, State.CurrentDayStreak);
        State.DaysPlayed.Add(todayKey);
    }

    /// <summary>
    /// Stamps <see cref="GameState.CompletedAt"/> the first time every task in
    /// every level is complete (powers Pages/Certificate.razor) — a no-op if
    /// already stamped or not yet actually complete. Returns whether it just
    /// transitioned to complete on THIS call, so callers can tell "already
    /// finished a while ago" apart from "finished the whole quest just now."
    /// </summary>
    private bool CheckQuestCompletion(IReadOnlyList<Level> levels)
    {
        if (State.CompletedAt is not null) return false;
        if (levels.Count == 0 || !levels.All(IsLevelComplete)) return false;
        State.CompletedAt = DateTime.UtcNow;
        return true;
    }

    // Small, fixed reward for choosing to replay a visual task's animation
    // after finishing it — see GameState.PlayedVisualTasks for why this is a
    // separate, deliberately modest reward moment from the task's own points,
    // and one-time per task so it can't be farmed by clicking Play repeatedly.
    private const int VisualPlayBonus = 5;

    public sealed record PlayResult(int PointsAwarded, IReadOnlyList<Achievement> NewAchievements);

    public async Task<PlayResult> RecordVisualPlayAsync(int levelId, GameTask task, IReadOnlyList<Level> levels)
    {
        var key = GameState.TaskKey(levelId, task.Id);
        var pointsAwarded = 0;

        if (State.PlayedVisualTasks.Add(key))
        {
            pointsAwarded = VisualPlayBonus;
            State.Points += pointsAwarded;
            await TrackClarityEventAsync("visual_played", ("level", levelId.ToString()), ("task", task.Id.ToString()));
        }

        var newAchievements = CheckNewAchievements(levels);
        await SaveAsync();
        return new PlayResult(pointsAwarded, newAchievements);
    }

    // Achievements unlocked THIS session (in-memory only, never persisted —
    // resets on reload) — Stats.razor uses this to give a just-unlocked
    // achievement a one-shot glow when the player browses the grid, instead
    // of it looking identical to one earned weeks ago.
    public IReadOnlySet<string> RecentlyUnlockedAchievements => _recentlyUnlockedAchievements;
    private readonly HashSet<string> _recentlyUnlockedAchievements = new();

    private List<Achievement> CheckNewAchievements(IReadOnlyList<Level> levels)
    {
        var newAchievements = new List<Achievement>();
        foreach (var achievement in Achievements.All)
        {
            if (!State.UnlockedAchievements.Contains(achievement.Id) && achievement.IsUnlocked(State, levels))
            {
                State.UnlockedAchievements.Add(achievement.Id);
                _recentlyUnlockedAchievements.Add(achievement.Id);
                newAchievements.Add(achievement);
                _ = TrackClarityEventAsync("achievement_unlocked", ("achievement", achievement.Id));
            }
        }
        return newAchievements;
    }

    /// <summary>
    /// True only the first time this is called after the quest is complete —
    /// Certificate.razor uses it to play a one-time entrance celebration on
    /// first seeing the finished certificate, without repeating it on every
    /// later visit/reload.
    /// </summary>
    public async Task<bool> TryBeginCertificateCelebrationAsync()
    {
        if (State.CompletedAt is null || State.CertificateCelebrated) return false;
        State.CertificateCelebrated = true;
        await SaveAsync();
        return true;
    }
}
