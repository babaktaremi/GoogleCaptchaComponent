## Features

![](https://i.ibb.co/1zskmPX/Relazor-180x180.png)

- Supports Blazor Dependecy Injection
- Simple to use
- easy to style the captcha component
- uses event invokation for validating captcha



## Intallation
 
 ### Package Manager Console 
 ```Install-Package GoogleCaptchaComponent -Version 2.0.0```
 
 ### Dotnet CLI
 ```dotnet add package GoogleCaptchaComponent --version 2.0.0```
 
 ### Configuration
 
 add the following code to ```Program.cs``` . You need to get a site Key from Google
 
 ```csharp
   builder.Services.AddGoogleCaptcha("Your Site Key");
 ```
 add the following code to ```_Imports.razor``` file
 
 ```razor
 @using GoogleCaptchaComponent.Components
@using GoogleCaptchaComponent.Events
@using GoogleCaptchaComponent.Services
 ```
 
 ## Usage
 first of all add the Captcha Component in the place that you need like this:
 
 ```razor
 <GoogleRecaptcha></GoogleRecaptcha>
 ```
 
 
 There Are two scenarios where you need to have a validated captcha:
 - Check captcha validation before posting information to an API
 - Do something right after the captcha is validated
 
 for the first usage there is an Static Method in Google Captcha that you can use it like this:
 
 ```Csharp
  public void PostingToMyApi()
    {
        if (GoogleRecaptcha.IsCaptchaValidated())
        {
            //Posting to API
        }
        else
        {
            //Showing message that captcha is not valid
        }
    }
 ```
 for the second usage you need to do a couple of things
 
 first inject the interface called ```ICaptchaCallBackService``` to your component. you can do like this
 
 in razor component:
 
 ```razor
 @inject ICaptchaCallBackService callBackService
 ```
 
 or in the code behind of the component like this
 
 ```csharp
  [Inject] 
  ICaptchaCallBackService CallbackService { get; set; }
 ```
 
 there are two events named ```SuccessCallBack``` and ```TimeOutCallBack```
 the ```SuccessCallBack``` you the eventArg class is ```CaptchaSuccessEventArgs``` which gives you the captcha response provided by google captcha API. And the ```CaptchaTimeOutEventArgs``` is simply a marker that shows the time of captcha validation is passed.
 
 Imagine this simple scenario. I want to enable the count button after the captcha is validated. I can do the following after injecting ```ICaptchaCallBackService```:
 
 ```razor

    protected override void OnAfterRender(bool firstRender)
    {
        callBackService.SuccessCallBack += SuccessCallBack;
        callBackService.TimeOutCallBack+=TimeOutCallBack;

        base.OnAfterRender(firstRender);
    }

    private void TimeOutCallBack(object sender, CaptchaTimeOutEventArgs e)
    {
        Console.WriteLine("Captcha Time Out");
    }

    private void SuccessCallBack(object sender, CaptchaSuccessEventArgs e)
    {
        Disabled = false;

         base.StateHasChanged();
    }

    private int currentCount = 0;

    private bool Disabled = true;

    private void IncrementCount()
    {

        currentCount++;
    }
}

 ```

first I override the ```OnAfterRender``` method and register event handlers for ```SuccessCallBack``` and ```TimeOutCallBack```. After successful captcha validation ```SuccessCallBack``` gets invoked and I can do things I need inside it.
Note that you don't necessarily need  to wirte an eventHandler for ```TimeOutCallBack```
the google captcha automatically renews the captcha for you. But if you need to do extra things you can use this event.

### Give it a chance!
If you like this project and let me know what you think about it.


