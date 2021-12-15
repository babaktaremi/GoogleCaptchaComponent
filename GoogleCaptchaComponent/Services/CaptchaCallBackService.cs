using System;
using GoogleCaptchaComponent.Events;

namespace GoogleCaptchaComponent.Services
{
    internal class CaptchaCallBackService:ICaptchaCallBackService
    {
        public EventHandler<CaptchaSuccessEventArgs> SuccessCallBack { get; set; }
        public EventHandler<CaptchaTimeOutEventArgs> TimeOutCallBack { get; set; }
    }
}
