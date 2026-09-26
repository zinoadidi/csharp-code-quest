using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using app;
using app.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Server endpoint for student profiles/progress/events (generic-crud
// backend). The URL is stored encrypted (see QuestServerConfig): the
// appsettings value wins when it decrypts, otherwise the compiled-in
// default does. Either way the app keeps working offline when neither
// yields a URL.
var serverSection = builder.Configuration.GetSection("QuestServer");
var serverUrl = QuestServerConfig.DecryptBaseUrl(serverSection["EncryptedBaseUrl"])
    ?? QuestServerConfig.DefaultBaseUrl;
builder.Services.AddScoped(sp => new QuestServerOptions
{
    BaseUrl = serverUrl,
    AppId = serverSection["AppId"] ?? QuestServerConfig.DefaultAppId,
});
builder.Services.AddScoped<QuestServerClient>();
builder.Services.AddScoped<QuestAccountService>();
builder.Services.AddScoped<CSharpRunner>();
builder.Services.AddScoped<GameStateService>();

var host = builder.Build();

// Keep the decrypted endpoint in this tab's sessionStorage so it is
// resolved once per session (and visible to owner tooling); the game
// itself runs off the in-memory options above either way.
try
{
    var js = host.Services.GetRequiredService<IJSRuntime>();
    await js.InvokeVoidAsync("sessionStorage.setItem", "questServerBaseUrl", serverUrl);
}
catch
{
    // Storage unavailable (private mode, etc.) — non-fatal.
}

await host.RunAsync();
