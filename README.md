[![NuGet Badge](https://buildstats.info/nuget/googlecaptchacomponent)](https://www.nuget.org/packages/googlecaptchacomponent)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://opensource.org/licenses/MIT)
![Twitter Follow](https://img.shields.io/twitter/url?label=Follow&style=social&url=https%3A%2F%2Ftwitter.com%2FBabakTaremi)

## Features

![](https://i.ibb.co/1zskmPX/Relazor-180x180.png)

-Supports Blazor dependency Injection

-Simple to use

-Easy to style the captcha component

-Uses event invocation for validating captcha

-Supports both reCaptcha v2 And v3

-Supports Server Side captcha response validation flow

## Release History

**-V1:** : Experimental. Lots of bugs and **now depricated**

**-V2:** : Major Bug Fixes. Interface clean up and added support for events in order to be more flexible and other devs can handle more customized scenarios. But this version also included some major bugs and the method provided for captcha validation wasn't so straight forward. Also the Server side validation flow using secret key was not present and users had to implement most of this feature themselves. **Depricated and no longer supported**

**-V3 (Current):** : By far the best release. I finally found the time and energy to write in the way I want. API got a lot cleaner. Duplicate and redundant code got removed. Configuration now has more options and I got rid of most of the bugs. This version also supports reCaptcha V3 and now you have the option to use v2 or v3 according to your need. 

**future:** There is a long way ahead. Keep posted for more updates and features. I think about version 5 all major updates will have huge breaking changes. I will continue supporting and updating this project until either I'm too old for it or an official solution is available for it. 

## Intallation
 
 #### Package Manager Console 
 
 ```
 Install-Package GoogleCaptchaComponent -Version 3.0.3
 ```
 
 #### Dotnet CLI
 
 ```
 dotnet add package GoogleCaptchaComponent --version 3.0.3
 ```
 
 ## Configuration
 
 Add the following code to `Program.cs` . You need to get a site Key from Google
 
 ```csharp
 builder.Services.AddGoogleCaptcha(configuration =>
        {
            configuration.ServerSideValidationRequired = true; 
            configuration.SiteKey = "Your Site Key"; // Site key can be received from reCaptcha admin console
            configuration.CaptchaVersion = CaptchaConfiguration.Version.V2; // V3 is also the option now
        });
 ```
 
 If you don't need server side validation with secret key or have another way of captcha var set `ServerSideValidationRequired` to `false`

 Keep in mind that by setting `CaptchaVersion` to `CaptchaConfiguration.Version.V3` server side validation will be required and has to be implemented or it will throw `CallBackDelegateException`
 
 Add the following code to `_Imports.razor` file
 
 ```razor
@using GoogleCaptchaComponent.Components
@using GoogleCaptchaComponent.Events
 ```
 
 Add the following code to `index.html` file at the end of the `body` tag
 
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
 
 `SuccessCallBack` event is fired when user has successfully validated captcha. If captcha version is `V3` or `ServerSideValidationRequired` is set to `true` than this event will be fired after successful validation of `ServerSideValidationHandler` .After successful validation, response token of reCaptcha is available via `CaptchaResponse` propery of `CaptchaSuccessEventArgs` class. Code of this handler can be something like this:

 
 ```Csharp
 void SuccessCallBack(CaptchaSuccessEventArgs e)
    {
        Disabled = false; //Disable attribute of button becomes false for example
        captchaResponse = e.CaptchaResponse; //result recieved from reCaptcha
        base.StateHasChanged();
    }
 ```
 
 `TimeOutCallBack` is called when the validation time of reCaptcha response has expired. Code of this handler can be something like this:
 
  ```Csharp
  void TimeOutCallBack(CaptchaTimeOutEventArgs e)
    {
        Disabled = true; //Button becomes disable again
        Console.WriteLine($"Captcha Time Out with message {e.ErrorMessage}");
        errorMessage = $"Captcha Timeout: {e.ErrorMessage}";
    }
 ```
 
 `ServerValidationErrorCallBack` is triggered when the validation of captcha response with secret key (that an internal API that holds that secret key had done it) was not successful. Keep in mind that this validation result value is determined by `ServerSideValidationHandler` result. Code of this handler can be something like this:
 
 ```Csharp
  private void ServerSideValidationError(CaptchaServerSideValidationErrorEventArgs e)
    {
        errorMessage = $"Captcha server side validation error: {e.ErrorMessage}";
    }
 ```
 
 `ServerSideValidationHandler` is where you can chack the captcha's validity by secret key.
 
 ***Important*** : Never put secret in client code. Secret key must be stored in a safe place so that only your Internal trusted API can have access to it.
 
 ***Remember*** : This event must have a related handler if `ServerSideValidationRequired` property is set to `true` or `CaptchaVersion` is set to 
 `CaptchaConfiguration.Version.V3`. without handler it will throw `CallBackDelegateException`
 
 The return of this event (or better called funtion) is `Task<ServerSideCaptchaValidationResultModel>` . The Model `ServerSideCaptchaValidationResultModel` has two properties: 
 
 **`IsSuccess`** : Whether the result of server side validation was successful or not. if `true` the `SuccessCallBack` will be triggered. Otherwise `ServerValidationErrorCallBack` is triggered.
 
 **`ValidationMessage`** : Message related to the server side validation result.
 
 the argument `ServerSideCaptchaValidationRequestModel` is **automatically instantiated** and no further configuration is needed.
 
 Code of this handler can be something like this:

```csharp
 /// <summary>
 /// Captcha Verification Should be done by an internal api which holds the secret key
 /// </summary>
 /// <returns></returns>
    private async Task<ServerSideCaptchaValidationResultModel> ServerSideValidationHandler(ServerSideCaptchaValidationRequestModel requestModel)
    {
        using var httpClient = new HttpClient();
        var apiResult = await httpClient.GetFromJsonAsync<GoogleCaptchaCheckResponseResult>($"https://api.mysecurewebsite.com/VerifyCaptcha?token={requestModel.CaptchaResponse}");
        return new ServerSideCaptchaValidationResultModel(apiResult.Success, string.Join("\n",apiResult.ErrorCodes ?? new List<string>(){"No Error"}));
    }
```

## Demo

![](https://i.ibb.co/nr08cyG/chrome-capture.gif)


## Give it a chance!
If you like this project give it a star. If you find any issues feel free to create them. Your help is greatly appreciated so feel free to give me PRs and your prefered scenarios.


