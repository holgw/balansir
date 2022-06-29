using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Products.DataAccess;
using BalansirApp.Core.Products.UseCases;

namespace BalansirApp.Core.Products
{
    class ProductsService : AbstractEntityService<ProductView, ProductsQueryParam>, IProductsService
    {
        // CTOR
        public ProductsService(IAppFilesLocator appFilesLocator) :
            base(appFilesLocator)
        {
        }

        // METHODS: Public
        public override ItemsPageQueryResult<ProductView, ProductsQueryParam> GetEntityListView(ProductsQueryParam queryParam)
        {
            ItemsPageQueryResult<ProductView, ProductsQueryParam> result = null;

            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var productDAO = new ProductDAO(db);
                var useCase = new GetProductsListView_UseCase(productDAO);
                result = useCase.Execute(queryParam);
            });

            return result;
        }
        public override ProductView GetEntityView(int id)
        {
            ProductView productView = null;

            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var productDAO = new ProductDAO(db);
                var useCase = new GetProductView_UseCase(productDAO);
                productView = useCase.Execute(id);
            });

            return productView;
        }
        public override void SaveEntity(ProductView productView)
        {
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var productDAO = new ProductDAO(db);
                var useCase = new SaveProduct_UseCase(productDAO);
                useCase.Execute(productView);
            });
        }
        public override void DeleteEntity(ProductView entity)
        {
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var productDAO = new ProductDAO(db);
                var actDAO = new ActDAO(db);
                var useCase = new DeleteProduct_UseCase(productDAO, actDAO);
                useCase.Execute(entity);
            });
        }
    }
}
