using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Products.DataAccess;

namespace BalansirApp.Core.Products.UseCases
{
    class GetProductsListView_UseCase :
        AbstractGetListView_UseCase<Product, ProductView, ProductsQueryParam>
    {
        // CTOR
        public GetProductsListView_UseCase(IDAO<Product, ProductsQueryParam> dao) :
            base(dao)
        {
        }

        // METHODS: Protected
        protected override ProductView BuildView(Product record)
        {
            return ProductView.Map(record);
        }
    }
}
