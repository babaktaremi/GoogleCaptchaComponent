# GoogleCaptchaComponent
Implementation of google captcha as a blazor compoenent


## Getting Started

### 1. Installing GoogleCaptchaComponent
You can install GoogleCaptchaComponent with [NuGet Package Manager Console](https://www.nuget.org/packages/GoogleCaptchaComponent/):

    Install-Package GoogleCaptchaComponent
    
Or via the .NET Core command-line interface:

    dotnet add package GoogleCaptchaComponent
    
### 2.Adding The Requierd Service

In Program.cs use the following code to add the needed services


```C#
 builder.Services.AddGoogleCaptcha("Your_Site_Key");
```

### 3.Adding Static Files in index.html

just copy the following line at the bottom of index.html page 

    <script src="_content/GoogleCaptchaComponent/Scripts/ScriptLoader.js"></script>
    
### 4. Adding the using statements
Just add the following using statements in _Imports.razor

      @using GoogleCaptchaComponent
      @using GoogleCaptchaComponent.Configuration

    
### 5.Usage

Add the following Componenet to the place where you need to use Google Captcha

      <GoogleRecaptcha></GoogleRecaptcha>

### 6. VAlidation

for validation, check if GoogleRecaptcha.reCAPTCHA_response is null or empty like the following exmaple

```C#
  if (string.IsNullOrEmpty(GoogleRecaptcha.reCAPTCHA_response))
        {
           //Captcha Invalid
        }
        else
        {
           //Captcha valid
        }
```
