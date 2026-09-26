namespace app.Services;

/// <summary>
/// Where student profiles, progress, and per-user events live on the
/// self-hosted generic-crud backend (see store-backend/API.md + SKILL.md).
/// One <see cref="AppId"/> UUID identifies this whole install-base; every
/// student gets small JSON docs under it. Pure static helpers (no I/O) so the
/// naming/uniqueness rules stay in one testable place — see
/// <see cref="QuestServerClient"/> (transport) and
/// <see cref="QuestAccountService"/> (account flows).
/// </summary>
public static class QuestServerConfig
{
    // The backend URL never sits in plaintext in the repo: it is stored
    // XOR-encrypted (base64) here and in wwwroot/appsettings.json's
    // QuestServer:EncryptedBaseUrl, then decrypted once at startup (see
    // Program.cs) and kept in memory + sessionStorage. This keeps the
    // endpoint out of casual repo scans — note it is obfuscation, not hard
    // security: the key ships with the app, so a determined reader of the
    // published bundle can still recover it.
    private const string EndpointKey = "quest-endpoint-key-v1";

    public const string EncryptedDefaultBaseUrl = "GQERAwcXSkEAFQMAHB1CHhZUXgJQAwYNGgQAAQcXAxoIChEDBQILQh0cFwcAFlpJABhLFwoHCwZECEgaXwNV";

    /// <summary>Plaintext backend URL (decrypted on first access).</summary>
    public static string DefaultBaseUrl => DecryptBaseUrl(EncryptedDefaultBaseUrl) ?? "";

    /// <returns>Decrypted URL, or null when the input isn't valid cipher.</returns>
    public static string? DecryptBaseUrl(string? encrypted)
    {
        if (string.IsNullOrWhiteSpace(encrypted)) return null;
        try
        {
            var cipher = Convert.FromBase64String(encrypted.Trim());
            if (cipher.Length == 0) return null;
            var plain = new byte[cipher.Length];
            for (var i = 0; i < cipher.Length; i++)
                plain[i] = (byte)(cipher[i] ^ EndpointKey[i % EndpointKey.Length]);
            var url = System.Text.Encoding.UTF8.GetString(plain);
            return url.StartsWith("https://", StringComparison.Ordinal) ? url : null;
        }
        catch
        {
            return null;
        }
    }

    // UUID identifying the csharp-code-quest install-base on that backend.
    // All profile/progress/event docs live under this id — never across users.
    public const string DefaultAppId = "c8f07072-dcfc-4a7b-a8e1-7004b37da42d";

    // Claim docs guard uniqueness client-side (the server's PUT is an
    // unconditional upsert, so a claim is GET-then-PUT + verify-read):
    public static string NameClaimId(string username) => $"user:name:{NormUsername(username)}";
    public static string EmailClaimId(string email) => $"user:email:{NormEmail(email)}";

    // Per-student docs, keyed by the stable user id (never by raw name):
    public static string ProfileId(string userId) => $"profile:{userId}";
    public static string ProgressId(string userId) => $"progress:{userId}";
    public static string EventsId(string userId) => $"events:{userId}";

    // Case-insensitive identity: "Ada" and "ada" (or "ADA@school.edu" vs
    // "ada@school.edu") are the same account, matching ImportStateAsync's
    // existing OrdinalIgnoreCase username comparison.
    public static string NormUsername(string username) => username.Trim().ToLowerInvariant();
    public static string NormEmail(string email) => email.Trim().ToLowerInvariant();

    public static bool IsValidUsername(string? username) =>
        !string.IsNullOrWhiteSpace(username) &&
        username.Trim().Length is >= 2 and <= 24;

    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        var v = email.Trim();
        var at = v.IndexOf('@');
        return at > 0 && at < v.Length - 1 && v.IndexOf('@', at + 1) < 0 &&
               v[(at + 1)..].Contains('.') && v.Length <= 254 && v.All(c => !char.IsWhiteSpace(c));
    }

    // Demo-class accounts are plain server profiles with IsDemo set (created
    // manually in the database, never advertised in the UI), so their
    // progress/events track per user like everyone else — they just skip
    // the school-email requirement.
}
