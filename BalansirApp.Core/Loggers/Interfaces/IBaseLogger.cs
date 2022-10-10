using System;

namespace BalansirApp.Core.Loggers.Interfaces
{
    interface IBaseLogger
    {
        void LogInfo(string title, string message = null);

        void LogDebug(string title, string message);

        void LogError(string title, string message = null, Exception ex = null);
    }
}
