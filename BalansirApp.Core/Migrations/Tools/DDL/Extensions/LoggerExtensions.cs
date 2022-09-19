using BalansirApp.Core.Logging;
using BalansirApp.Core.Migrations.Tools.DDL.Utility;

namespace BalansirApp.Core.Migrations.Tools.DDL.Extensions
{
    internal static class LoggerExtensions
    {
        public static void LogSqlCommandResult(this IMigrationsLogger migrationsLogger, SqlCommandResult result)
        {
            if (result.Exception == null)
            {
                migrationsLogger.LogInfo(result.SqlCommand, $"Affected rows = {result.AffectedRowsCount}");
            }
            else
            {
                migrationsLogger.LogError(result.SqlCommand, null, result.Exception);
            }
        }
    }
}
