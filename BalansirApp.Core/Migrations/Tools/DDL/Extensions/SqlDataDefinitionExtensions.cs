using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.DDL.Utility;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BalansirApp.Core.Migrations.Tools.DDL.Extensions
{
    /// <summary>
    /// Расширения для вызова базовых команд SQLite DDL
    /// </summary>
    static class SqlDataDefinitionExtensions
    {
        public static int GetUserVersion(this MySQLiteConnection db)
        {
            string command = $"{SQLiteKeywords.Pragma} {SQLiteKeywords.UserVersion}";
            return db.Execute<int>(command);
        }

        public static void SetUserVersion(this MySQLiteConnection db, int version)
        {
            string command = $"{SQLiteKeywords.Pragma} {SQLiteKeywords.UserVersion} = {version}";
            db.Execute(command);
        }

        public static SqlCommandResult AddTable<TTable>(this MySQLiteConnection db)
        {
            string tableName = typeof(TTable).GetAttributeValue<TableAttribute, string>(a => a.Name);
            string pkColumn = $"(Id " +
                $"{SQLiteColumnType.INTEGER} " +
                $"{SQLiteKeywords.PrimaryKey} " +
                $"{SQLiteKeywords.Autoincrement} " +
                $"{SQLiteKeywords.NotNull})";

            var cmdParts = new List<string>()
            {
                SQLiteKeywords.Create,
                SQLiteKeywords.Table,
                tableName,
                pkColumn,
            };

            string command = string.Join(" ", cmdParts);
            return db.ExecuteSafe(command);
        }

        public static SqlCommandResult AddColumn<TTable, TColumn>(
            this MySQLiteConnection db,
            Expression<Func<TTable, TColumn>> propertyLambda,
            SQLiteColumnType columnType,
            bool isPrimaryKey = false,
            bool isAutoincrement = false,
            bool isNotNull = false)
        {
            string tableName = typeof(TTable).GetAttributeValue<TableAttribute, string>(a => a.Name);
            string columnName = AttributeExtensions.GetProperty(propertyLambda).Name;

            var cmdParts = new List<string>()
            {
                SQLiteKeywords.AlterTable,
                tableName,
                SQLiteKeywords.AddColumn,
                columnName,
                columnType.ToString()
            };

            if (isPrimaryKey)
            {
                cmdParts.Add(SQLiteKeywords.PrimaryKey);
            }

            if (isAutoincrement)
            {
                cmdParts.Add(SQLiteKeywords.Autoincrement);
            }

            if (isNotNull)
            {
                cmdParts.Add(SQLiteKeywords.NotNull);
            }

            string command = string.Join(" ", cmdParts);
            return db.ExecuteSafe(command);
        }

        public static SqlCommandResult AddIndex<TTable, TColumn>(
            this MySQLiteConnection db,
            Expression<Func<TTable, TColumn>> propertyLambda,
            bool isUnique = false)
        {
            string tableName = typeof(TTable).GetAttributeValue<TableAttribute, string>(a => a.Name);
            string columnName = AttributeExtensions.GetProperty(propertyLambda).Name;
            string indexName = $"{tableName}_{columnName}";
            string targetName = $"{tableName}({columnName})";

            var cmdParts = new List<string>()
            {
                SQLiteKeywords.Create,
            };

            if (isUnique)
            {
                cmdParts.Add(SQLiteKeywords.Unique);
            }

            cmdParts.Add(SQLiteKeywords.Index);
            cmdParts.Add(indexName);
            cmdParts.Add(SQLiteKeywords.On);
            cmdParts.Add(targetName);

            string command = string.Join(" ", cmdParts);
            return db.ExecuteSafe(command);
        }

        public static SqlCommandResult ExecuteSafe(this MySQLiteConnection db, string sqlCommand)
        {
            var result = new SqlCommandResult(sqlCommand);

            try
            {
                int affectedRowsCount = db.Execute(sqlCommand);
                result.AffectedRowsCount = affectedRowsCount;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }
    }
}
