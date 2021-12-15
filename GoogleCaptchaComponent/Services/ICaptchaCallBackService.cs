using System;
using GoogleCaptchaComponent.Events;

namespace GoogleCaptchaComponent.Services
{
    public interface ICaptchaCallBackService
    {
        public  EventHandler<CaptchaSuccessEventArgs> SuccessCallBack { get; set; }
        public  EventHandler<CaptchaTimeOutEventArgs> TimeOutCallBack { get; set; }
    }
}
