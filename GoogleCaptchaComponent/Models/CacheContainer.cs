using System.Collections.Generic;

namespace GoogleCaptchaComponent.Models;

internal class CacheContainer
{
    public HashSet<string> LoadedScripts { get;  }

    public CacheContainer()
    {
        LoadedScripts = new();
    }
}