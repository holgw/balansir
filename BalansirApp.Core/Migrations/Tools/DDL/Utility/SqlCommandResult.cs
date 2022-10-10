using System;

namespace BalansirApp.Core.Migrations.Tools.DDL.Utility
{
    class SqlCommandResult
    {
        public string SqlCommand { get; set; }

        public int AffectedRowsCount { get; set; }

        public Exception Exception { get; set; }

        // CTOR
        public SqlCommandResult(string sqlCommand)
        {
            SqlCommand = sqlCommand ?? throw new ArgumentNullException(nameof(sqlCommand));
        }
    }
}
