using BalansirApp.Core.Loggers.Interfaces;
using NLog;
using System;

namespace BalansirApp.Core.Logging
{
    class BaseLogger : IBaseLogger
    {
        private readonly ILogger _logger;

        // CTOR
        public BaseLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogInfo(string title, string message = null)
        {
            this.Log(LogLevel.Info, title, message);
        }

        public void LogDebug(string title, string message)
        {
            this.Log(LogLevel.Debug, title, message);
        }

        public void LogError(string title, string message = null, Exception ex = null)
        {
            if (message == null)
                message = String.Empty;

            if (ex != null)
                message += $"[Exception: {ex}]";

            this.Log(LogLevel.Info, title, message);
        }

        void Log(LogLevel logLevel, string title, string message)
        {
            if (!String.IsNullOrEmpty(title))
                title += ": ";

            if (!String.IsNullOrEmpty(message))
                title += message;

            _logger.Log(logLevel, title);
        }
    }
}
