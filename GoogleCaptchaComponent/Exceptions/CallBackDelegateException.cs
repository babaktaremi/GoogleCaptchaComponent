using System;

namespace GoogleCaptchaComponent.Exceptions;

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