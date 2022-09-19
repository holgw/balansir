using NLog;

namespace BalansirApp.Core.Logging
{
    class MigrationsLogger : BaseLogger, IMigrationsLogger
    {
        // CTOR
        public MigrationsLogger(ILogger logger) : base(logger)
        {
        }
    }
}
