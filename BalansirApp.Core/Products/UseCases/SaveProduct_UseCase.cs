using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Products.UseCases
{
    class SaveProduct_UseCase
    {
        private readonly IProductDAO _productDAO;

        // CTOR
        public SaveProduct_UseCase(IProductDAO productDAO)
        {
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
        }

        public void Execute(ProductView view)
        {
            var entity = new Product();

            if (view.Id > 0)
            {
                entity = _productDAO.TryGet(view.Id);

                if (entity == null)
                {
                    string errMsg = "Unknown Product.Id";
                    throw new InvalidOperationException(errMsg);
                }
            }

            RefillItem(view, entity);
            _productDAO.Save(entity);
            view.Id = entity.Id;
        }

        void RefillItem(ProductView source, Product target)
        {
            target.Name = source.Name;
            target.Code = source.Code;
            target.Units = source.Units;
            target.Description = source.Description;
        }
    }
}
