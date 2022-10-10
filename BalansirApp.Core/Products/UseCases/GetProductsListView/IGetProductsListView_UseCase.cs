using BalansirApp.Core.Common.UseCases.Interfaces;
using BalansirApp.Core.Products.DataAccess;

namespace BalansirApp.Core.Products.UseCases.GetProductsListView
{
    public interface IGetProductsListView_UseCase : IGetListView_UseCase<ProductView, ProductsQueryParam>
    {
    }
}
