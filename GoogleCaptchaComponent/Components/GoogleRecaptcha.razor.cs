using System.ComponentModel;
using System.Threading.Tasks;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Events;
using GoogleCaptchaComponent.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace GoogleCaptchaComponent.Components
{
    public partial class GoogleRecaptcha
    {
        private static string _captchaResponse;

        [Inject] public IJSRuntime Js { get; set; }
        [Inject] public IOptions<CaptchaConfiguration> CaptchaConfiguration { get; set; }
        [Inject] ICaptchaCallBackService CallbackService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Js.InvokeVoidAsync("loadScript", "https://www.google.com/recaptcha/api.js");

            await Js.InvokeVoidAsync("loadScript", "_content/GoogleCaptchaComponent/Scripts/JsOfReCAPTCHA.js");

            if (firstRender)
            {
                await Js.InvokeVoidAsync("render_recaptcha", DotNetObjectReference.Create(this), "recaptcha_container", CaptchaConfiguration.Value.SiteKey);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
        public void CallbackOnSuccess(string response)
        {
            _captchaResponse = response;
            CallbackService.SuccessCallBack?.Invoke(this,new CaptchaSuccessEventArgs(response));
        }

        [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
        public void CallbackOnExpired()
        {
            CallbackService.TimeOutCallBack?.Invoke(this,new CaptchaTimeOutEventArgs());
        }


        public static bool IsCaptchaValidated() => !string.IsNullOrEmpty(_captchaResponse);
    }
}
