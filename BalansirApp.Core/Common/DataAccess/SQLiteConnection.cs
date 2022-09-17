using BalansirApp.Core.Acts;
using BalansirApp.Core.Migrations.Tools.DDL;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using BalansirApp.Core.Products;
using LinqToDB;
using LinqToDB.Data;

namespace BalansirApp.Core.Common.DataAccess
{
    public class SQLiteConnection : DataConnection
    {
        public IDataDefinitionBase DataDefinitionBase { get; }
        public ITable<Product> Products => this.GetTable<Product>();
        public ITable<Act> Acts => this.GetTable<Act>();

        // CTOR
        public SQLiteConnection(IAppFilesLocator appFilesLocator) 
            : base(ProviderName.SQLite, appFilesLocator.ConnectionString) 
        {
            this.DataDefinitionBase = new DataDefinitionBase(this);
        }
    }
}
