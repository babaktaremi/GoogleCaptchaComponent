using System;

namespace GoogleCaptchaComponent.Exceptions;

public class InvalidCaptchaVersionExceptionException:Exception
{
    public InvalidCaptchaVersionExceptionException()
    {
    }

    public InvalidCaptchaVersionExceptionException(string message)
        : base(message)
    {
    }

    public InvalidCaptchaVersionExceptionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}