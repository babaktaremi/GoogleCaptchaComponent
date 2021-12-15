using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleCaptchaComponent
{
   public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddGoogleCaptcha(this IServiceCollection services,string siteKey)
        {
            services.Configure<CaptchaConfiguration>(o => o.SiteKey = siteKey);

            services.AddScoped<ICaptchaCallBackService, CaptchaCallBackService>();

            return services;
        }
    }
}
