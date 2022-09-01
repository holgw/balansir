using BalansirApp.Core.Acts;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Migrations.Tools;
using BalansirApp.Core.Products;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BalansirApp.Core.Common.DataAccess
{
    /// <summary>
    /// Инструмент для инициализации БД --
    /// создания файла БД, таблиц и т.д.
    /// </summary>
    internal class DbMaintainService : IDbMaintainService
    {
        private readonly IAppFilesLocator _appFilesLocator;

        // CTOR
        public DbMaintainService(IAppFilesLocator appFilesLocator)
        {
            _appFilesLocator = appFilesLocator ?? throw new System.ArgumentNullException(nameof(appFilesLocator));
        }

        // METHODS: Public
        public void InitializeDatabase()
        {
            // Если файла БД по указанному пути не существует,
            // то файл будет создан автоматически
            _appFilesLocator.ExecuteDbConnection(db =>
            {
               
                // Создадим таблицы в БД, если их не существует
                //db.CreateTable<Product>();
                //db.CreateTable<Act>();
                //var result = db.Execute($"CREATE UNIQUE INDEX IF NOT EXISTS ProductSet_Name on ProductSet(Name);");

                // TODO: Мб лог исключений из DDL должен быть общим для всех DDL?
                var dbManager = new MigrationsManager(db);
                dbManager.CheckAndApplyMigrations();
            });
        }
    }

    public class TableInfo
    {
        public int cid { get; set; }
        public string name { get; set; }
    }

    public static class SQLiteKeywords
    {
        public const string PrimaryKey = "PRIMARY KEY";
        public const string Autoincrement = "AUTOINCREMENT";
        public const string Null = "NULL";
        public const string NotNull = "NOT NULL";
        public const string Unique = "UNIQUE";
        public const string AlterTable = "ALTER TABLE";
        public const string AddColumn = "ADD COLUMN";
        public const string Create = "CREATE";
        public const string Index = "INDEX";
        public const string Table = "TABLE";
        public const string On = "ON";
        public const string Pragma = "PRAGMA";
        public const string UserVersion = "USER_VERSION";
    }

    public enum SQLiteColumnType
    {
        BIGINT,
        INTEGER,
        BOOLEAN,
        DATE,
        DATETIME,
        TIME,
        DECIMAL,
        REAL,
        VARCHAR,
        CHAR,
    }

    /// <summary>
    /// Инструмент для вызова базовых команд SQLite DDL
    /// </summary>
    public class SQLiteSimpleDataDefinitionManager
    {
        //public bool AddColumn
    }
}
