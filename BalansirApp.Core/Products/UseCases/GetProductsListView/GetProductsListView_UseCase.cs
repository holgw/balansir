using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Products.DataAccess;
using BalansirApp.Core.Products.DataAccess.Interfaces;

namespace BalansirApp.Core.Products.UseCases.GetProductsListView
{
    class GetProductsListView_UseCase :
        AbstractGetListView_UseCase<Product, ProductView, ProductsQueryParam>,
        IGetProductsListView_UseCase
    {
        // CTOR
        public GetProductsListView_UseCase(IProductDAO dao) : base(dao)
        {
        }

        // METHODS: Protected
        protected override ProductView BuildView(Product record)
        {
            return ProductView.Map(record);
        }
    }
}
