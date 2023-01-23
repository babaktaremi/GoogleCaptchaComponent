namespace GoogleCaptchaComponent.Models;

/// <summary>
///  validation request used for server site validation (requires secret key. DON'T put secret key in client application)
/// </summary>
/// <param name="CaptchaResponse">Received captcha response after successful client challenge</param>
public record ServerSideCaptchaValidationRequestModel(string CaptchaResponse);