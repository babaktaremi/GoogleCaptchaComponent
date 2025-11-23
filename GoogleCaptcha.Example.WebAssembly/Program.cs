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
    options.V3SiteKey = "6LfNOgQrAAAAAEohmkPB2NvZOu979KVZnTN_nS6A";
    options.V2SiteKey = "6LedOwQrAAAAAOyXUYkAK978AxgbvgVh66dR5NW9";
    options.DefaultTheme = CaptchaConfiguration.Theme.Dark;
    options.DefaultLanguage =CaptchaLanguages.English;
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();