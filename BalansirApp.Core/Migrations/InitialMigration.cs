using BalansirApp.Core.Acts;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Abstractions;
using BalansirApp.Core.Migrations.Tools;
using BalansirApp.Core.Products;
using System;

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
            _db.AddTable<Product>();
            _db.AddColumn<Product, string>(x => x.Name, SQLiteColumnType.VARCHAR, false, false, true);
            _db.AddColumn<Product, string>(x => x.Code, SQLiteColumnType.VARCHAR, false, false, true);
            _db.AddColumn<Product, string>(x => x.Units, SQLiteColumnType.VARCHAR);
            _db.AddColumn<Product, string>(x => x.Description, SQLiteColumnType.VARCHAR);
            _db.AddColumn<Product, decimal>(x => x.Balance, SQLiteColumnType.DECIMAL);
            _db.AddIndex<Product, string>(x => x.Name, true);
            _db.AddIndex<Product, string>(x => x.Code, true);

            _db.AddTable<Act>();
            _db.AddColumn<Act, DateTime>(x => x.TimeStamp, SQLiteColumnType.DATETIME, false, false, true);
            _db.AddColumn<Act, int>(x => x.ProductId, SQLiteColumnType.INTEGER, false, false, true);
            _db.AddColumn<Act, decimal>(x => x.Delta, SQLiteColumnType.DECIMAL, false, false, true);
            _db.AddColumn<Act, string>(x => x.Comment, SQLiteColumnType.VARCHAR);
            _db.AddIndex<Act, DateTime>(x => x.TimeStamp);
            _db.AddIndex<Act, int>(x => x.ProductId);
        }
    }
}
