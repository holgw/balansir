using BalansirApp.Core.Migrations.Tools.DDL.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BalansirApp.Core.Migrations.Tools.Interfaces
{
    public interface IDataDefinitionTable<TTable>
    {
        List<Exception> Exceptions { get; }

        IDataDefinitionTable<TTable> AddColumn<TColumn>(
            Expression<Func<TTable, TColumn>> propertyLambda, 
            SQLiteColumnType columnType, 
            bool isPrimaryKey = false, 
            bool isAutoincrement = false, 
            bool isNotNull = false);
        
        IDataDefinitionTable<TTable> AddIndex<TColumn>(
            Expression<Func<TTable, TColumn>> propertyLambda, 
            bool isUnique = false);
    }
}