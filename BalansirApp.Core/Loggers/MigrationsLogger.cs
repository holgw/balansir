using BalansirApp.Core.Loggers.Interfaces;
using NLog;

namespace BalansirApp.Core.Loggers
{
    class MigrationsLogger : BaseLogger, IMigrationsLogger
    {
        // CTOR
        public MigrationsLogger(ILogger logger) : base(logger)
        {
        }
    }
}
