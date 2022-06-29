using BalansirApp.Core.Common;
using BalansirApp.Core.Products.DataAccess;

namespace BalansirApp.Core.Products
{
    public interface IProductsService : IEntityService<ProductView, ProductsQueryParam>
    {
    }
}