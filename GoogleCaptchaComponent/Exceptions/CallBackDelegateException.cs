using System;

namespace GoogleCaptchaComponent.Exceptions;

/// <summary>
/// Occurs when related call back method is not implemented or throws unhandled exception
/// </summary>
public class CallBackDelegateException:Exception
{
    public CallBackDelegateException()
    {
    }

    public CallBackDelegateException(string message)
        : base(message)
    {
    }

    public CallBackDelegateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}