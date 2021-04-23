using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleCaptchaComponent.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleCaptchaComponent
{
   public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddGoogleCaptcha(this IServiceCollection services,string siteKey)
        {
            services.Configure<CaptchaConfiguration>(o => o.SiteKey = siteKey);
            return services;
        }
    }
}
