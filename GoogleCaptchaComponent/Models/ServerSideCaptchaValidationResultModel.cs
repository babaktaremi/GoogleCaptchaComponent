namespace GoogleCaptchaComponent.Models;

/// <summary>
/// General validation used for server site validation (requires secret key. DON'T put secret key in client application)
/// </summary>
/// <param name="IsSuccess">indicates weather server side verification was success ro not</param>
/// <param name="ValidationMessage">message related to the operation result</param>
public record ServerSideCaptchaValidationResultModel(bool IsSuccess = false,
    string ValidationMessage = "Server side validation handler not found");