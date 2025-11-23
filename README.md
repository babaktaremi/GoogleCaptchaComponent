[![NuGet Badge](https://img.shields.io/nuget/dt/googlecaptchacomponent)](https://www.nuget.org/packages/googlecaptchacomponent)
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

**`Action`**: You can specify the action name that will be used for reCaptcha V3. If you don't specify the action, then none will be used.

**`RenderOnLoad`**: If set to false and if the captcha is V3, the captcha will not be rendered on page load. Instead you can manually execute the captcha on a action, like a form submission. If the captcha is V2, this parameter will be ignored.

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

## Captcha V3 Actions
With Captcha V3 it is recommended if you are protecting an action like a user logging in to only execute it when the action happens, not on page load.
As well as allowing you to specify the action name in each place you execute reCAPTCHA, you enable the following new features:

* A detailed break-down of data for your top ten actions in the admin console
* Adaptive risk analysis based on the context of the action, because abusive behavior can vary.

A full example can be found in [CounterV3 for blazor WASM](GoogleCaptcha.Exmaple/Pages/CounterV3.razor) or [CounterV3 for Server Side Blazor](GoogleCaptcha.Example.Server/Pages/CounterV3.razor)

If you would like to use this then when rendering a reCAPTCHA V3 component you can specify the action name, a ref, and not to render on load like this:
```razor
<GoogleRecaptcha 
    @ref="captcha"
    DefaultAction="DefaultAction"
    RenderOnLoad="false"
    ...
/>
@code {
    GoogleRecaptcha captcha;
...
```
Then when calling your action you execute the captcha and then inside of the SuccessCallBack you can submit the form with captcha response to validate on the server.
```csharp
    //Can be a form submission or any other action
    private async Task ExecuteAction()
    {
        await captcha.ExecuteV3WithActionAsync();
    }
    
      void SuccessCallBack(CaptchaSuccessEventArgs e)
    {
        var response = e.CaptchaResponse;
       //Submit form with response
    }
```

The ```ExecuteV3WithActionAsync``` also takes an action name. If nothing is provided , the ```DefaultAction``` will be used. in order to use actions, you have to provide the value of one these parameters (Either ```actionName``` parameter on ```ExecuteV3WithActionAsync``` method or ```DefaultAction```)
Also note that this method will throw ```InvalidCaptchaVersionExceptionException``` if used on V2 version.

## Demo

![](https://i.ibb.co/nr08cyG/chrome-capture.gif)


## Give it a chance!
If you like this project give it a star. If you find any issues feel free to create them. Your help is greatly appreciated so feel free to give me PRs and your prefered scenarios.


