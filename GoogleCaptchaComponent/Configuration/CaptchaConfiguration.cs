using GoogleCaptchaComponent.Models;

namespace GoogleCaptchaComponent.Configuration;

/// <summary>
/// General Configuration for reCaptcha
/// </summary>
public class CaptchaConfiguration
{
    /// <summary>
    /// Site key that will be used for V2 Recaptcha. 
    /// </summary>
    public string V2SiteKey { get; set; }

    /// <summary>
    /// Site key that will be used for V3 Recaptcha
    /// </summary>
    public string V3SiteKey { get; set; }

    /// <summary>
    /// Indicating Default Captcha Version. This version is applied if there is no version explicitly declared on component
    /// </summary>
    public Version DefaultVersion { get; set; }

    /// <summary>
    /// Setting default captcha theme. This theme will be used if there is no theme explicitly declared on component. Note that theme is only applied to V2
    /// </summary>
    public Theme DefaultTheme { get; set; }

    /// <summary>
    /// Setting default captcha language. This language will be used if there is no language explicitly declared on component. Note that language is only applied to V2
    /// </summary>
    public CaptchaLanguages DefaultLanguage { get; set; } = CaptchaLanguages.English;

    public enum Version
    {
        V2,V3
    }

    public enum Theme
    {
        Dark,Light
    }
}