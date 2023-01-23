## Features

![](https://i.ibb.co/1zskmPX/Relazor-180x180.png)

-Supports Blazor dependency Injection

-Simple to use

-Easy to style the captcha component

-Uses event invocation for validating captcha

-Supports both reCaptcha v2 And v3


## Intallation
 
 ### Package Manager Console 
 ```Install-Package GoogleCaptchaComponent -Version 3.0.0```
 
 ### Dotnet CLI
 ```dotnet add package GoogleCaptchaComponent --version 3.0.0```
 
 ### Configuration
 
 add the following code to ```Program.cs``` . You need to get a site Key from Google
 
 ```csharp
 builder.Services.AddGoogleCaptcha(configuration =>
        {
            configuration.ServerSideValidationRequired = true; //If you don't need server side validation with secret key, set it to false
            configuration.SiteKey = config;
            configuration.CaptchaVersion = CaptchaConfiguration.Version.V2; // V3 is also the option now
        });
 ```
 
 Keep in mind that by setting ```CaptchaVersion``` to ```CaptchaConfiguration.Version.V3``` server side validation will be required and has to be implemented
 
 add the following code to ```_Imports.razor``` file
 
 ```razor
 @using GoogleCaptchaComponent.Components
@using GoogleCaptchaComponent.Events
@using GoogleCaptchaComponent.Services
 ```
 then add the following code to ```index.html``` file at the end of the ```body``` tag
 
 ```html
<script src="_content/GoogleCaptchaComponent/Scripts/ScriptLoader.js"></script>
```
 
 ## Usage
 first of all add the Captcha Component in the place that you need like this:
 
 ```razor
<GoogleRecaptcha 
    SuccessCallBack="SuccessCallBack" 
    TimeOutCallBack="TimeOutCallBack" 
    ServerValidationErrorCallBack="ServerSideValidationError" 
    ServerSideValidationHandler="ServerSideValidationHandler" >
</GoogleRecaptcha>

 ```
 ```SuccessCallBack``` event is fired when user has successfully validated captcha. If captcha version is ```V3``` or ```ServerSideValidationRequired``` is set to ```true``` than this event will be fired after successful validation of ```ServerSideValidationHandler```.After successful validation, response token of reCaptcha is available via ```CaptchaResponse``` propery of ```CaptchaSuccessEventArgs```. Code of this handler's event can be something like this:

 
 ```Csharp
 void SuccessCallBack(CaptchaSuccessEventArgs e)
    {
        Disabled = false; //Disable attribute of button becomes false

         captchaResponse = e.CaptchaResponse;

        base.StateHasChanged();
    }
 ```
 
 ```TimeOutCallBack``` is called when the validation time of reCaptcha response has expired. its handler code can be something like this
 
  ```Csharp
  void TimeOutCallBack(CaptchaTimeOutEventArgs e)
    {
        Disabled = true; //Button becomes disable again
        Console.WriteLine($"Captcha Time Out with message {e.ErrorMessage}");
        errorMessage = $"Captcha Timeout: {e.ErrorMessage}";
    }
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

### Demo
![](https://i.ibb.co/nr08cyG/chrome-capture.gif)


### Give it a chance!
If you like this project give it a star. If you find any issues feel free to create issue. Your help is greatly appreciated so feel free to give me PRs


