using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Products.DataAccess;
using BalansirApp.Core.Products.UseCases.DeleteProduct;
using BalansirApp.Core.Products.UseCases.GetProductsListView;
using BalansirApp.Core.Products.UseCases.GetProductView;
using BalansirApp.Core.Products.UseCases.SaveProduct;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BalansirApp.Core.Products
{
    class ProductsService : AbstractEntityService<ProductView, ProductsQueryParam>, IProductsService
    {
        // CTOR
        public ProductsService(IServiceProvider serviceProvider) : 
            base(serviceProvider)
        {
        }

        // METHODS: Public
        protected override ItemsPageQueryResult<ProductView, ProductsQueryParam> GetEntityListViewAction(IServiceScope serviceScope, ProductsQueryParam queryParam)
        {
            var useCase = serviceScope.ServiceProvider.GetService<IGetProductsListView_UseCase>();
            return useCase.Execute(queryParam);
        }
        protected override ProductView GetEntityViewAction(IServiceScope serviceScope, int id)
        {
            var useCase = serviceScope.ServiceProvider.GetService<IGetProductView_UseCase>();
            return useCase.Execute(id);
        }
        protected override void SaveEntityAction(IServiceScope serviceScope, ProductView productView)
        {
            var useCase = serviceScope.ServiceProvider.GetService<ISaveProduct_UseCase>();
            useCase.Execute(productView);
        }
        protected override void DeleteEntityAction(IServiceScope serviceScope, ProductView entity)
        {
            var useCase = serviceScope.ServiceProvider.GetService<IDeleteProduct_UseCase>();
            useCase.Execute(entity);
        }
    }
}
