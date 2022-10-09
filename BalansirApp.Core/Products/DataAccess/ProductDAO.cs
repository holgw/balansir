using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using LinqToDB;
using System.Linq;

namespace BalansirApp.Core.Products.DataAccess
{
    public class ProductDAO : AbstractDAO<Product, ProductsQueryParam>, IProductDAO
    {
        protected override ITable<Product> Table => _db.Products;

        // CTOR
        public ProductDAO(SQLiteConnection db) : base(db)
        {
        }

        // METHODS: Protected
        protected override IQueryable<Product> Query(ProductsQueryParam queryParam)
        {
            var q = base.Query(queryParam);

            if (queryParam != null)
            {
                if (!string.IsNullOrEmpty(queryParam.ProductName))
                {
                    q = q.Where(x =>
                        x.Code == queryParam.ProductName ||
                        x.Name.Contains(queryParam.ProductName) ||
                        x.Description.Contains(queryParam.ProductName)
                    );
                }
            }

            return q.OrderBy(x => x.Name);
        }
    }
}
