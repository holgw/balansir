using BalansirApp.Core.Acts;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Abstractions;
using BalansirApp.Core.Products;

namespace BalansirApp.Core.Migrations
{
    class InitialMigration : AbstractMigration
    {
        // CTOR
        public InitialMigration(SQLiteConnection db) : base(db)
        {
        }

        public override void ApplyMigration()
        {
            _db.DataDefinitionBase.AddTable<Product>()
                .AddColumn(x => x.Name, SQLiteColumnType.VARCHAR, false, false, true)
                .AddIndex(x => x.Name, true)
                .AddColumn(x => x.Code, SQLiteColumnType.VARCHAR, false, false, true)
                .AddIndex(x => x.Code, true)
                .AddColumn(x => x.Units, SQLiteColumnType.VARCHAR)
                .AddColumn(x => x.Description, SQLiteColumnType.VARCHAR)
                .AddColumn(x => x.Balance, SQLiteColumnType.DECIMAL);

            _db.DataDefinitionBase.AddTable<Act>()
                .AddColumn(x => x.TimeStamp, SQLiteColumnType.DATETIME, false, false, true)
                .AddIndex(x => x.TimeStamp)
                .AddColumn(x => x.ProductId, SQLiteColumnType.INTEGER, false, false, true)
                .AddIndex(x => x.ProductId)
                .AddColumn(x => x.Delta, SQLiteColumnType.DECIMAL, false, false, true)
                .AddColumn(x => x.Comment, SQLiteColumnType.VARCHAR);
        }
    }
}
