namespace GoogleCaptchaComponent.Configuration;

/// <summary>
/// General Configuration for reCaptcha
/// </summary>
public class CaptchaConfiguration
{
    private bool _serverValidationEnabled;
    /// <summary>
    /// Specific site key received from google developer console
    /// </summary>
    public string SiteKey { get; set; }

    /// <summary>
    /// A flag which indicates that captcha response result needs server side validation with secret key
    /// </summary>
    public bool ServerSideValidationRequired {
        get => CaptchaVersion==Version.V3 || _serverValidationEnabled;
        set => _serverValidationEnabled=value;
    }

    /// <summary>
    /// Indicating Captcha Version. Note That if V3 is selected Server side validation becomes required and mandatory
    /// </summary>
    public Version CaptchaVersion { get; set; }

    public enum Version
    {
        V2,V3
    }
}