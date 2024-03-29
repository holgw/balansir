﻿using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Products.DataAccess;
using BalansirApp.Core.Products.DataAccess.Interfaces;

namespace BalansirApp.Core.Products.UseCases.GetProductView
{
    class GetProductView_UseCase :
        AbstractGetView_UseCase<Product, ProductView, ProductsQueryParam>,
        IGetProductView_UseCase
    {
        // CTOR
        public GetProductView_UseCase(IProductDAO dao) : base(dao)
        {
        }

        // METHODS: Protected
        protected override ProductView BuildView(Product record)
        {
            return ProductView.Map(record);
        }
    }
}
