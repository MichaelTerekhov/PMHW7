using System;
using System.Diagnostics;

namespace RequestProcessor.App.Logging.Impl
{
    internal class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine($"[{DateTime.UtcNow}] {message}");
        }

        public void Log(Exception exception, string message)
        {
            Debug.WriteLine($"[{DateTime.UtcNow}] ALERT" + $"The {exception.Message} was caused by {exception.InnerException}\n" +
                $"[Main failure]{message}");
        }
    }
}
