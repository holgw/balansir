using BalansirApp.Core.Common.DataAccess;
using System;

namespace BalansirApp.Core.Migrations.Abstractions
{
    abstract class AbstractMigration : IMigration
    {
        protected readonly SQLiteConnection _db;

        // CTOR
        public AbstractMigration(SQLiteConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public abstract void ApplyMigration();
    }
}
