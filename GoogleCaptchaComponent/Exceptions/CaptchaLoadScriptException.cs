using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCaptchaComponent.Exceptions
{
    /// <summary>
    /// Occurs when wrong site key or version is selected
    /// </summary>
    public class CaptchaLoadScriptException: Exception
    {
        public CaptchaLoadScriptException()
        {
        }

        public CaptchaLoadScriptException(string message)
            : base(message)
        {
        }

        public CaptchaLoadScriptException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
