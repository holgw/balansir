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
        private readonly IDataDefinitionBase _dataDefinitionBase;
        private readonly IDbBackupManager _dbBackupManager;
        private readonly List<IMigration> _migrations;        

        public const int DbVersion = 1;

        // CTOR
        public DbMigrationsManager(SQLiteConnection db, IDataDefinitionBase dataDefinitionBase, IDbBackupManager dbBackupManager)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dataDefinitionBase = dataDefinitionBase ?? throw new ArgumentNullException(nameof(dataDefinitionBase));
            _dbBackupManager = dbBackupManager ?? throw new ArgumentNullException(nameof(dbBackupManager));

            _migrations = new List<IMigration>()
            {
                new InitialMigration(db, _dataDefinitionBase),
            };
        }

        public void CheckAndApplyMigrations()
        {
            int currentDbVersion = _db.GetUserVersion();
            if (currentDbVersion != DbVersion)
            {
                _dbBackupManager.BackupFile();
                _migrations.ForEach(x => x.ApplyMigration());
                
                

                _db.SetUserVersion(DbVersion);
            }
        }
    }
}
