using System.Collections.Generic;
using GoogleCaptchaComponent.Configuration;

namespace GoogleCaptchaComponent.Models;

internal class CacheContainer
{
    public HashSet<string> LoadedScripts { get;  }
    public CaptchaConfiguration.Version CurrentVersion { get; set; }

    public CacheContainer()
    {
        LoadedScripts = new();
    }
}