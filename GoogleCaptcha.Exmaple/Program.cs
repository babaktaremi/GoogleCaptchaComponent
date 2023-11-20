using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.AspNetCore.Components.Web;

namespace GoogleCaptcha.Exmaple;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var tokenData = new Dictionary<string, string>()
        {
            {"CaptchaSiteToken", "Your V2 site key from Google developer console"},
            {"CaptchaSiteTokenV3", "Your V3 Site key from Google developer console"},
        };

        var memoryConfig = new MemoryConfigurationSource { InitialData = tokenData };

        builder.Configuration.Add(memoryConfig);

        var v2SiteKey = builder.Configuration["CaptchaSiteToken"];
        var v3SiteKey = builder.Configuration["CaptchaSiteTokenV3"];

        builder.Services.AddGoogleCaptcha(configuration =>
        {
            configuration.V2SiteKey = v2SiteKey;
            configuration.V3SiteKey = v3SiteKey;
            configuration.DefaultVersion = CaptchaConfiguration.Version.V2;
            configuration.DefaultTheme = CaptchaConfiguration.Theme.Light;
        });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        await builder.Build().RunAsync();
    }
}