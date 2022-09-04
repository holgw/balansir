using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Products.DataAccess;

namespace BalansirApp.Core.Products.UseCases
{
    class GetProductView_UseCase :
        AbstractGetView_UseCase<Product, ProductView, ProductsQueryParam>,
        IGetProductView_UseCase
    {
        // CTOR
        public GetProductView_UseCase(IDAO<Product, ProductsQueryParam> dao) :
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
