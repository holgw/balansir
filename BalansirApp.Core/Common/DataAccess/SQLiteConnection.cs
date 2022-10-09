using BalansirApp.Core.Acts;
using BalansirApp.Core.Products;
using LinqToDB;
using LinqToDB.Data;

namespace BalansirApp.Core.Common.DataAccess
{
    public class SQLiteConnection : DataConnection
    {
        public ITable<Product> Products => this.GetTable<Product>();
        public ITable<Act> Acts => this.GetTable<Act>();

        // CTOR
        public SQLiteConnection(IAppFilesLocator appFilesLocator) 
            : base(ProviderName.SQLiteMS, appFilesLocator.ConnectionString) 
        {
        }
    }
}
