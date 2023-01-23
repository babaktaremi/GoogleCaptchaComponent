using System;

namespace GoogleCaptchaComponent.Events;

/// <summary>
/// Fires on success captcha validation with given response by recaptcha.
/// </summary>
public  class CaptchaSuccessEventArgs:EventArgs
{
    public CaptchaSuccessEventArgs(string captchaResponse)
    {
        CaptchaResponse = captchaResponse;
    }
    public string CaptchaResponse { get;  }
}