using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoogleCaptcha.Exmaple
{
    public class GoogleCaptchaCheckResponseResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
