using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GoogleCaptchaComponent.Services.Implementation
{
    internal class RecaptchaService: IRecaptchaService
    {
        private readonly IJSRuntime _jsRuntime;

        public RecaptchaService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ReloadAsync()
        {
            await _jsRuntime.InvokeVoidAsync("reloadCaptcha");
        }
    }
}
