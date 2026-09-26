using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace app.Services;

/// <summary>
/// Thin transport over the generic-crud store API (see
/// store-backend/API.md): GET one doc, PUT upsert, PATCH shallow merge.
/// Every method is offline-first — network failures, rate limits (429), and
/// unexpected shapes surface as <see cref="StoreResult.Unavailable"/> instead
/// of throwing, so sync never blocks gameplay. A 404 is
/// <see cref="StoreResult.Missing"/> (the server WAS reached). Callers pass
/// doc ids; ids are escaped here since usernames/emails can contain
/// URL-reserved chars.
/// </summary>
public sealed class QuestServerClient(HttpClient http, QuestServerOptions options)
{
    public enum StoreResult
    {
        /// <summary>Network/parse/rate-limit failure — server state unknown.</summary>
        Unavailable,
        /// <summary>Server reached, doc id does not exist.</summary>
        Missing,
        /// <summary>Server reached, doc exists (see <see cref="DocGet.Doc"/>).</summary>
        Found,
    }

    public sealed record DocGet(StoreResult Result, JsonElement? Doc);

    public sealed record DocListItem(string Id, JsonElement Doc);

    private string DocUrl(string docId) =>
        $"{options.BaseUrl.TrimEnd('/')}/api/store/{options.AppId}/{Uri.EscapeDataString(docId)}";

    /// <summary>
    /// Lists every doc under this install-base ({id, doc} pairs). Used by
    /// the local owner-only admin view; never called during gameplay.
    /// Returns an empty list (never throws) when unreachable.
    /// </summary>
    public async Task<List<DocListItem>> ListDocsAsync()
    {
        try
        {
            using var response = await http.GetAsync(
                $"{options.BaseUrl.TrimEnd('/')}/api/store/{options.AppId}");
            if (!response.IsSuccessStatusCode) return new();
            using var body = await response.Content.ReadFromJsonAsync<JsonDocument>();
            if (body is null || !body.RootElement.TryGetProperty("items", out var items)) return new();
            var list = new List<DocListItem>();
            foreach (var item in items.EnumerateArray())
            {
                if (!item.TryGetProperty("id", out var id) ||
                    id.ValueKind != JsonValueKind.String) continue;
                if (!item.TryGetProperty("doc", out var doc)) continue;
                list.Add(new DocListItem(id.GetString()!, doc.Clone()));
            }
            return list;
        }
        catch
        {
            return new();
        }
    }

    public async Task<DocGet> GetDocAsync(string docId)
    {
        try
        {
            using var response = await http.GetAsync(DocUrl(docId));
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new DocGet(StoreResult.Missing, null);
            if (!response.IsSuccessStatusCode)
                return new DocGet(StoreResult.Unavailable, null);
            using var body = await response.Content.ReadFromJsonAsync<JsonDocument>();
            if (body is null || !body.RootElement.TryGetProperty("doc", out var doc))
                return new DocGet(StoreResult.Unavailable, null);
            return new DocGet(StoreResult.Found, doc.Clone());
        }
        catch
        {
            return new DocGet(StoreResult.Unavailable, null);
        }
    }

    public async Task<bool> PutDocAsync(string docId, object doc)
    {
        try
        {
            using var response = await http.PutAsJsonAsync(DocUrl(docId), new { doc });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> PatchDocAsync(string docId, object patch)
    {
        try
        {
            using var response = await http.PatchAsJsonAsync(DocUrl(docId), new { patch });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

/// <summary>Resolved server endpoint + app id (see wwwroot/appsettings.json).</summary>
public sealed class QuestServerOptions
{
    public string BaseUrl { get; set; } = QuestServerConfig.DefaultBaseUrl;
    public string AppId { get; set; } = QuestServerConfig.DefaultAppId;
}
