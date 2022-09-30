using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using System;

namespace BalansirApp.Core.Migrations.Abstractions
{
    abstract class AbstractMigration : IMigration
    {
        protected readonly MySQLiteConnection _db;
        protected readonly IDataDefinitionBase _dataDefinitionBase;

        // CTOR
        public AbstractMigration(MySQLiteConnection db, IDataDefinitionBase dataDefinitionBase)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dataDefinitionBase = dataDefinitionBase ?? throw new ArgumentNullException(nameof(dataDefinitionBase));
        }

        public abstract void ApplyMigration();
    }
}
