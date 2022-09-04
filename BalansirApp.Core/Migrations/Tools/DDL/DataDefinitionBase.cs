using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.DDL.Extensions;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using System;
using System.Collections.Generic;

namespace BalansirApp.Core.Migrations.Tools.DDL
{
    class DataDefinitionBase : IDataDefinitionBase
    {
        private readonly SQLiteConnection _db;

        public List<Exception> Exceptions { get; } = new List<Exception>();

        // CTOR
        public DataDefinitionBase(SQLiteConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IDataDefinitionTable<TTable> AddTable<TTable>()
        {
            var newException = _db.AddTable<TTable>();

            if (newException != null)
                Exceptions.Add(newException);

            return new DataDefinitionTable<TTable>(_db);
        }
    }
}
