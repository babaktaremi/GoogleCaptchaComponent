using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GoogleCaptcha.Example.WebAssembly;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddGoogleCaptcha(options =>
{
    options.DefaultVersion = CaptchaConfiguration.Version.V2;
    options.V3SiteKey = "V2 site key";
    options.V2SiteKey = "V3 site key";
    options.DefaultTheme = CaptchaConfiguration.Theme.Dark;
    options.DefaultLanguage =CaptchaLanguages.English;
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();