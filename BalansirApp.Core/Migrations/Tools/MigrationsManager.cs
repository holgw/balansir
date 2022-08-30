using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Abstractions;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using System;
using System.Collections.Generic;

namespace BalansirApp.Core.Migrations.Tools
{
    class MigrationsManager : IMigrationsManager
    {
        private readonly SQLiteConnection _db;
        private readonly List<IMigration> _migrations;

        public const int DbVesrion = 1;

        // CTOR
        public MigrationsManager(SQLiteConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            _migrations = new List<IMigration>()
            {
                new InitialMigration(db),
            };
        }

        public void CheckAndApplyMigrations()
        {
            int currentDbVersion = _db.GetUserVersion();
            if (currentDbVersion != DbVesrion)
            {
                _migrations.ForEach(x => x.ApplyMigration());
                _db.SetUserVersion(DbVesrion);
            }
        }
    }
}
