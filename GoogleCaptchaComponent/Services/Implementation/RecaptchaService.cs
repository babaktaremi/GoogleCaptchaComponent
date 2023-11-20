using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Models;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace GoogleCaptchaComponent.Services.Implementation
{
    internal class RecaptchaService : IRecaptchaService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly CacheContainer _cacheContainer;
        public RecaptchaService(IJSRuntime jsRuntime, CacheContainer cacheContainer)
        {
            _jsRuntime = jsRuntime;
            _cacheContainer = cacheContainer;
        }

        public async Task ReloadAsync()
        {
            if (_cacheContainer.CurrentVersion == CaptchaConfiguration.Version.V3)
                return;

            await _jsRuntime.InvokeVoidAsync("reloadCaptcha");
        }
    }
}
