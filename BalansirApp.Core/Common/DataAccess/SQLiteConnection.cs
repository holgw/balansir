using BalansirApp.Core.Acts;
using BalansirApp.Core.Products;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;

namespace BalansirApp.Core.Common.DataAccess
{
    public class MySQLiteConnection : DataConnection
    {
        public ITable<Product> Products => this.GetTable<Product>();
        public ITable<Act> Acts => this.GetTable<Act>();

        // CTOR
        public MySQLiteConnection(IAppFilesLocator appFilesLocator) 
            : base(ProviderName.SQLiteMS, appFilesLocator.ConnectionString) 
        {
        }
    }
}
