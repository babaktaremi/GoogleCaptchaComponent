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

**-V3 :** :  Duplicate and redundant code got removed. Configuration now has more options and I got rid of most of the bugs. This version also supports reCaptcha V3 and now you have the option to use v2 or v3 according to your need. This version is useful for projects that have been developed using NET 7 and above

**-V4 :** Update project to NET 8 . Theme configuration added and now both V2 and V3 can be used in the same project and different pages (based on your needs you can use V2 in one page and V3 in another page). Also server side validation is required for both V2 and V3 in this version . This version also have some minor breaking changes. So keep that in mind when updating.

**future:** There is a long way ahead. Keep posted for more updates and features. I think about version 5 all major updates will have huge breaking changes. I will continue supporting and updating this project until either I'm too old for it or an official solution is available for it. 

## Intallation
 
 #### Package Manager Console 
 
 ```
 Install-Package GoogleCaptchaComponent 
 ```
 
 #### Dotnet CLI
 
 ```
 dotnet add package GoogleCaptchaComponent 
 ```
 
 ## Configuration
 
 Add the following code to `Program.cs` . You need to get a site Key from Google
 
 ```csharp
        builder.Services.AddGoogleCaptcha(configuration =>
        {
            configuration.V2SiteKey = "Your V2 site key from Google developer console";
            configuration.V3SiteKey = "Your V3 Site key from Google developer console";
            configuration.DefaultVersion = CaptchaConfiguration.Version.V2;
            configuration.DefaultTheme = CaptchaConfiguration.Theme.Light;
            configuration.DefaultLanguage = CaptchaLanguages.English;
        });
 ```
 `DefaultVersion` is the version that will be used if no other versions are specified in component parameters. 
 `DefaultTheme`  is the theme that will be used if no other themes are specified in component parameters. Note that you can only apply version for Recaptcha V2 at the moment.
 `DefaultLanguage` is the language that will be used if no other language are specified in component parameters. Note that this can only
 apply for ReCaptcha V2 at the moment.
 
 Add the following code to `_Imports.razor` file
 
 ```razor
@using GoogleCaptchaComponent.Components
@using GoogleCaptchaComponent.Events
@using GoogleCaptchaComponent.Models
@using GoogleCaptchaComponent.Services
@using GoogleCaptchaComponent.Configuration
 ```
 
 Add the following code to `index.html` file at the end of the `body` tag
 
 ```html
<script src="_content/GoogleCaptchaComponent/Scripts/ScriptLoader.js"></script>
```
 
 ## Usage
 
 first of all add the Captcha Component in the place that you need like this:
 
 ```razor
<GoogleRecaptcha SuccessCallBack="SuccessCallBack"
                 TimeOutCallBack="TimeOutCallBack"
                 ServerValidationErrorCallBack="ServerSideValidationError"
                 ServerSideValidationHandler="ServerSideValidationHandler"
                 Version="CaptchaConfiguration.Version.V2"
                 Theme="CaptchaConfiguration.Theme.Light"
                 Language="CaptchaLanguages.English"
                 >
</GoogleRecaptcha>
 ```
 
 `SuccessCallBack` event is fired when user has successfully validated captcha. This event will be fired after successful validation of `ServerSideValidationHandler` .After successful validation, response token of reCaptcha is available via `CaptchaResponse` property of `CaptchaSuccessEventArgs` class. Code of this handler can be something like this:

 
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
 
 ***Remember*** : This event must have a related handler. Without handler it will throw `CallBackDelegateException`
 
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

**`Version`** : You can specify version explicitly via this parameter . If you don't specify version , the `DefaultVersion` will be used instead.

**`Theme`** : You can specify theme via this parameter (Theme is only applied to V2). If you don't specify theme , the `DefaultTheme` will be used instead.

**`Language`**: You can specify the ReCaptcha language via this parameter (Language is only applied to V2), If you don't specify the language, the `DefaultLanguage` will be used instead which is the English Language.

## Refreshing The Captcha Manually

Inject the ```IRecaptchaService``` and call the ```ReloadAsync``` method. If there is a recaptcha component on page , it will reload the component.

```razor
@inject IRecaptchaService RecaptchaService

<button class="btn btn-primary" @onclick="ReloadRecapatcha">reload</button>
```

```csharp
   private async Task ReloadRecapatcha()
   {
       await RecaptchaService.ReloadAsync();
   }
```




## Demo

![](https://i.ibb.co/nr08cyG/chrome-capture.gif)


## Give it a chance!
If you like this project give it a star. If you find any issues feel free to create them. Your help is greatly appreciated so feel free to give me PRs and your prefered scenarios.


