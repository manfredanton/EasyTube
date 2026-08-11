using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TwipDevEasyTube;
using TwipDevEasyTube.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient ohne BaseAddress – YouTubeService verwendet absolute URLs
builder.Services.AddScoped(_ => new HttpClient
{
	BaseAddress = new Uri("https://www.googleapis.com")
});

// Services
builder.Services.AddScoped<IYouTubeService, YouTubeService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IRequestCounterService, RequestCounterService>();
builder.Services.AddScoped<SearchStateService>();
builder.Services.AddScoped<SearchHistoryService>();

await builder.Build().RunAsync();
