using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Abstractions;
using BalansirApp.Core.Migrations.Tools.DDL.Extensions;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using System;
using System.Collections.Generic;

namespace BalansirApp.Core.Migrations.Tools
{
    class DbMigrationsManager : IDbMigrationsManager
    {
        private readonly SQLiteConnection _db;
        private readonly IDbBackupManager _dbBackupManager;
        private readonly List<IMigration> _migrations;        

        public const int DbVesrion = 1;

        // CTOR
        public DbMigrationsManager(SQLiteConnection db, IDbBackupManager dbBackupManager)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dbBackupManager = dbBackupManager ?? throw new ArgumentNullException(nameof(dbBackupManager));

            _migrations = new List<IMigration>()
            {
                new InitialMigration(db),
            };
        }

        public void CheckAndApplyMigrations()
        {
            if (_db is null)
                throw new ArgumentNullException(nameof(_db));

            int currentDbVersion = _db.GetUserVersion();
            if (currentDbVersion != DbVesrion)
            {
                _dbBackupManager.BackupFile();
                _migrations.ForEach(x => x.ApplyMigration());
                _db.SetUserVersion(DbVesrion);
            }
        }
    }
}
