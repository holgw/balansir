using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Logging;
using BalansirApp.Core.Migrations.Tools.DDL.Extensions;
using BalansirApp.Core.Migrations.Tools.DDL.Utility;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using System;
using System.Linq.Expressions;

namespace BalansirApp.Core.Migrations.Tools.DDL
{
    class DataDefinitionTable<TTable> : IDataDefinitionTable<TTable>
    {
        private readonly IMigrationsLogger _logger;
        private readonly SQLiteConnection _db;

        // CTOR
        public DataDefinitionTable(IMigrationsLogger logger, SQLiteConnection db)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IDataDefinitionTable<TTable> AddColumn<TColumn>(
            Expression<Func<TTable, TColumn>> propertyLambda,
            SQLiteColumnType columnType,
            bool isPrimaryKey = false,
            bool isAutoincrement = false,
            bool isNotNull = false)
        {
            Func<SqlCommandResult> func = () => _db.AddColumn(
                propertyLambda,
                columnType,
                isPrimaryKey,
                isAutoincrement,
                isNotNull
            );
            return BaseMethod(func);
        }

        public IDataDefinitionTable<TTable> AddIndex<TColumn>(
            Expression<Func<TTable, TColumn>> propertyLambda,
            bool isUnique = false)
        {
            return BaseMethod(() => _db.AddIndex(propertyLambda, isUnique));
        }

        DataDefinitionTable<TTable> BaseMethod(Func<SqlCommandResult> func)
        {
            var newResult = func();

            _logger.LogSqlCommandResult(newResult);

            return this;
        }
    }
}
