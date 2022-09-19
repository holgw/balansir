using BalansirApp.Core.Acts;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Abstractions;
using BalansirApp.Core.Migrations.Tools.DDL.Utility;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using BalansirApp.Core.Products;

namespace BalansirApp.Core.Migrations
{
    class InitialMigration : AbstractMigration
    {       
        // CTOR
        public InitialMigration(SQLiteConnection db, IDataDefinitionBase dataDefinitionBase) 
            : base(db, dataDefinitionBase)
        {
        }

        public override void ApplyMigration()
        {
            _dataDefinitionBase.AddTable<Product>()
                .AddColumn(x => x.Name, SQLiteColumnType.VARCHAR, false, false, true)
                .AddIndex(x => x.Name, true)
                .AddColumn(x => x.Code, SQLiteColumnType.VARCHAR, false, false, true)
                .AddIndex(x => x.Code, true)
                .AddColumn(x => x.Units, SQLiteColumnType.VARCHAR)
                .AddColumn(x => x.Description, SQLiteColumnType.VARCHAR)
                .AddColumn(x => x.Balance, SQLiteColumnType.DECIMAL);

            _dataDefinitionBase.AddTable<Act>()
                .AddColumn(x => x.TimeStamp, SQLiteColumnType.DATETIME, false, false, true)
                .AddIndex(x => x.TimeStamp)
                .AddColumn(x => x.ProductId, SQLiteColumnType.INTEGER, false, false, true)
                .AddIndex(x => x.ProductId)
                .AddColumn(x => x.Delta, SQLiteColumnType.DECIMAL, false, false, true)
                .AddColumn(x => x.Comment, SQLiteColumnType.VARCHAR);
        }
    }
}
