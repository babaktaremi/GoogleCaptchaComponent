using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleCaptchaComponent.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace GoogleCaptchaComponent.Services.Implementation
{
    internal class RecaptchaService: IRecaptchaService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IOptions<CaptchaConfiguration> _captchaConfiguration;

        public RecaptchaService(IJSRuntime jsRuntime, IOptions<CaptchaConfiguration> captchaConfiguration)
        {
            _jsRuntime = jsRuntime;
            _captchaConfiguration = captchaConfiguration;
        }

        public async Task ReloadAsync()
        {
            if(_captchaConfiguration.Value.CaptchaVersion == CaptchaConfiguration.Version.V3)
                return;

            await _jsRuntime.InvokeVoidAsync("reloadCaptcha");
        }
    }
}
