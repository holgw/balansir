using NLog.Config;
using NLog;
using NLog.Targets;
using Microsoft.Extensions.DependencyInjection;
using BalansirApp.Core.Common.DataAccess;
using System.IO;
using BalansirApp.Core.Loggers.Interfaces;
using BalansirApp.Core.Logging;

namespace BalansirApp.Core.Loggers.DI
{
    internal static class Extensions
    {
        public static void AddLoggers(this ServiceCollection services)
        {
            services.AddLogFactory();

            services.AddSingleton<IMainLogger>(x => 
            {
                var logger = x.GetService<LogFactory>().GetLogger(nameof(IMainLogger));
                return new MainLogger(logger);
            });

            services.AddSingleton<IMigrationsLogger>(x =>
            {
                var logger = x.GetService<LogFactory>().GetLogger(nameof(IMigrationsLogger));
                return new MigrationsLogger(logger);
            });
        }

        private static void AddLogFactory(this ServiceCollection services)
        {
            services.AddSingleton(x =>
            {
                var appFilesLocator = x.GetService<IAppFilesLocator>();

                FileTarget GetFileTarget(string fileName)
                {
                    return new FileTarget()
                    {
                        FileName = Path.Combine(appFilesLocator.LogsFolder, fileName),
                        MaxArchiveFiles = 5,
                        ArchiveAboveSize = 1048576,
                    };
                }

                var migrationLogFile = GetFileTarget($"{nameof(IMigrationsLogger)}.log");
                var mainLogFile = GetFileTarget($"{nameof(IMainLogger)}.log");

                var config = new LoggingConfiguration();
                config.AddRuleForAllLevels(migrationLogFile, nameof(IMigrationsLogger));
                config.AddRuleForAllLevels(mainLogFile, nameof(IMainLogger));

                return new LogFactory { Configuration = config };
            });
        }
    }
}
