using System;

namespace GoogleCaptchaComponent.Events;

/// <summary>
/// Fires when time of captcha validation is expired and user has to revalidate captcha
/// </summary>
public class CaptchaTimeOutEventArgs:EventArgs
{
    public string ErrorMessage { get;  }

    public CaptchaTimeOutEventArgs(string errorMessage=default)
    {
        ErrorMessage = errorMessage;
    }
}