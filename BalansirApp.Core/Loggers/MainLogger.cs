using BalansirApp.Core.Loggers.Interfaces;
using NLog;

namespace BalansirApp.Core.Logging
{
    class MainLogger : BaseLogger, IMainLogger
    {
        // CTOR
        public MainLogger(ILogger logger) : base(logger)
        {
        }
    }
}
