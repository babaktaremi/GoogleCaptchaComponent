using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Events;
using GoogleCaptchaComponent.Exceptions;
using GoogleCaptchaComponent.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace GoogleCaptchaComponent.Components;


/// <summary>
/// Main google captcha component to use in razor files
/// </summary>
public partial class GoogleRecaptcha
{


    [Inject] public IJSRuntime Js { get; set; }
    [Inject] internal IOptions<CaptchaConfiguration> CaptchaConfiguration { get; set; }

    /// <summary>
    /// Success captcha validation event
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback<CaptchaSuccessEventArgs> SuccessCallBack { get; set; }

    /// <summary>
    /// captcha validation Timeout event
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback<CaptchaTimeOutEventArgs> TimeOutCallBack { get; set; }

    /// <summary>
    ///  captcha validation error event
    /// </summary>
    [Parameter]
    public EventCallback<CaptchaServerSideValidationErrorEventArgs> ServerValidationErrorCallBack { get; set; }

    /// <summary>
    /// Handler for implementing server side validation
    /// </summary>
    [Parameter]
    public Func<ServerSideCaptchaValidationRequestModel, Task<ServerSideCaptchaValidationResultModel>> ServerSideValidationHandler { get; set; }

    /// <summary>
    /// Specified configuration in startup
    /// </summary>
    public CaptchaConfiguration CurrentConfiguration => CaptchaConfiguration.Value;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {


        if (firstRender)
        {
            try
            {
                if (CaptchaConfiguration.Value.CaptchaVersion == Configuration.CaptchaConfiguration.Version.V3)
                    await Js.InvokeVoidAsync("loadScript", $"https://www.google.com/recaptcha/api.js?render={CaptchaConfiguration.Value.SiteKey}");
                else
                    await Js.InvokeVoidAsync("loadScript", "https://www.google.com/recaptcha/api.js");

                await Js.InvokeVoidAsync("loadScript", "_content/GoogleCaptchaComponent/Scripts/JsOfReCAPTCHA.js");

                if (CaptchaConfiguration.Value.CaptchaVersion == Configuration.CaptchaConfiguration.Version.V2)
                    await Js.InvokeVoidAsync("render_recaptcha_v2", DotNetObjectReference.Create(this), "recaptcha_container", CaptchaConfiguration.Value.SiteKey);
                else
                    await Js.InvokeVoidAsync("render_recaptcha_v3", DotNetObjectReference.Create(this), CaptchaConfiguration.Value.SiteKey);
            }
            catch (Exception e)
            {
                throw new CaptchaLoadScriptException(
                    "Invalid site key or wrong reCaptcha version. Make sure your site key is valid and is for proper version",
                    e);
            }
        }

       

        await base.OnAfterRenderAsync(firstRender);
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task CallbackOnSuccess(string response)
    {
        if (CaptchaConfiguration.Value.ServerSideValidationRequired)
        {
            if (ServerSideValidationHandler == null)
            {
                if(CaptchaConfiguration.Value.CaptchaVersion==Configuration.CaptchaConfiguration.Version.V3)
                    throw new CallBackDelegateException(
                        $"Server side validation is required for reCaptcha-V3 but there is no handler found for {nameof(ServerSideValidationHandler)}");

                else
                    throw new CallBackDelegateException(
                        $"Server side validation is set to true but there is no handler found for {nameof(ServerSideValidationHandler)}");
            }
               

            try
            {
                var serverSideValidationResult = await ServerSideValidationHandler(new ServerSideCaptchaValidationRequestModel(response));


                if (!serverSideValidationResult.IsSuccess)
                {
                    if (!ServerValidationErrorCallBack.HasDelegate)
                        throw new CallBackDelegateException(
                            $"Server side reCaptcha validation is failed but no handler found for {nameof(ServerValidationErrorCallBack)}");

                    await ServerValidationErrorCallBack.InvokeAsync(
                        new CaptchaServerSideValidationErrorEventArgs(serverSideValidationResult.ValidationMessage));
                }

            }

            catch (Exception e)
            {
                throw new CallBackDelegateException("Error invoking related server validation callback", e);
            }
        }


        if (!SuccessCallBack.HasDelegate)
            throw new CallBackDelegateException(
                $"no Callback handler found for {nameof(SuccessCallBack)}");

        await SuccessCallBack.InvokeAsync(new CaptchaSuccessEventArgs(response));
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task CallbackOnExpired()
    {
        if (!TimeOutCallBack.HasDelegate)
            throw new CallBackDelegateException(
                $"no Callback handler found for {nameof(TimeOutCallBack)}");

        await InvokeAsync(StateHasChanged);

        await TimeOutCallBack.InvokeAsync(new CaptchaTimeOutEventArgs("captcha validation expired"));
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task CallBackError(string message)
    {
        if (!ServerValidationErrorCallBack.HasDelegate)
            throw new CallBackDelegateException(
                $"no Callback handler found for {nameof(ServerValidationErrorCallBack)}");

        await InvokeAsync(StateHasChanged);

        await ServerValidationErrorCallBack.InvokeAsync(new CaptchaServerSideValidationErrorEventArgs(message));
    }

}