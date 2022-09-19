using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Config;
using System;

namespace Tests.LoggerTests
{
    [Obsolete]
    public class LoggerTest
    {
        [TestMethod]
        public void Test1()
        {
            var migrationLogFile = new NLog.Targets.FileTarget() 
            { 
                FileName = @"Logs\MigrationLogFile.txt",
                MaxArchiveFiles = 5,
                ArchiveAboveSize = 2,
            };
            var mainLogFile = new NLog.Targets.FileTarget() 
            { 
                FileName = @"Logs\otherLogFile.txt",
                MaxArchiveFiles = 5,
                ArchiveAboveSize = 2,
            };

            // Rules for mapping loggers to targets
            var config = new LoggingConfiguration();
            config.AddRuleForAllLevels(mainLogFile, "MainLog");
            config.AddRuleForAllLevels(migrationLogFile, "MigrationLog");

            var logFac = new LogFactory();
            logFac.Configuration = config;

            var logger1 = logFac.GetLogger("MigrationLog");
            logger1.Log(LogLevel.Info, "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq1");
            logger1.Log(LogLevel.Warn, "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww1");
            logger1.Log(LogLevel.Error, "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee1");

            var logger2 = logFac.GetLogger("MainLog");
            logger2.Log(LogLevel.Info, "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq2");
            logger2.Log(LogLevel.Warn, "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww2");
            logger2.Log(LogLevel.Error, "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee2");
        }
    }
}
