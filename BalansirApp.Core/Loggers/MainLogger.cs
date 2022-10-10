using BalansirApp.Core.Loggers.Interfaces;
using NLog;

namespace BalansirApp.Core.Loggers
{
    class MainLogger : BaseLogger, IMainLogger
    {
        // CTOR
        public MainLogger(ILogger logger) : base(logger)
        {
        }
    }
}
