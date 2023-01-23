using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace GoogleCaptcha.Exmaple;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        var tokenData = new Dictionary<string, string>()
        {
            {"CaptchaSiteToken", "Your Site Key"},
            {"CaptchaSecretToken", "Your Secret Key"}
        };

        var memoryConfig = new MemoryConfigurationSource { InitialData = tokenData };

        builder.Configuration.Add(memoryConfig);

        var config = builder.Configuration["CaptchaSiteToken"];

        builder.Services.AddGoogleCaptcha(configuration =>
        {
            configuration.ServerSideValidationRequired = true;
            configuration.SiteKey = config;
            configuration.CaptchaVersion = CaptchaConfiguration.Version.V2;
        });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        await builder.Build().RunAsync();
    }
}