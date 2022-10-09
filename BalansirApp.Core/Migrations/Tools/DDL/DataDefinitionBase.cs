using BalansirApp.Core.Loggers.Interfaces;
using BalansirApp.Core.Migrations.Tools.DDL.Extensions;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using SQLite;
using System;

namespace BalansirApp.Core.Migrations.Tools.DDL
{
    class DataDefinitionBase : IDataDefinitionBase
    {
        private readonly IMigrationsLogger _logger;
        private readonly SQLiteConnection _db;

        // CTOR
        public DataDefinitionBase(IMigrationsLogger logger, SQLiteConnection db)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IDataDefinitionTable<TTable> AddTable<TTable>()
        {
            var newResult = _db.AddTable<TTable>();
            _logger.LogSqlCommandResult(newResult);
            return new DataDefinitionTable<TTable>(_logger, _db);
        }
    }
}
