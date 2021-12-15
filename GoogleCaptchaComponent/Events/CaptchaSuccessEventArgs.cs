using System;

namespace GoogleCaptchaComponent.Events
{
   public  class CaptchaSuccessEventArgs:EventArgs
    {
        public CaptchaSuccessEventArgs(string captchaResponse)
        {
            CaptchaResponse = captchaResponse;
        }
        public string CaptchaResponse { get;  }
    }
}
