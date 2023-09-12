using System;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleCaptchaComponent;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Add Needed reCaptcha services for blazor
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Site key received in developer console</param>
    /// <returns></returns>
    public static IServiceCollection AddGoogleCaptcha(this IServiceCollection services,Action<CaptchaConfiguration> configuration)
    {
        services.Configure<CaptchaConfiguration>(configuration);

        services.AddSingleton<CacheContainer>();

        return services;
    }

}