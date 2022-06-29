using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Products.UseCases
{
    class DeleteProduct_UseCase
    {
        private readonly IProductDAO _productDAO;
        private readonly IActDAO _actDAO;

        // CTOR
        public DeleteProduct_UseCase(IProductDAO productDAO, IActDAO actDAO)
        {
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
            _actDAO = actDAO ?? throw new ArgumentNullException(nameof(actDAO));
        }

        public void Execute(ProductView entity)
        {
            _productDAO.Delete(entity.Id);

            // Удалим все акты для выбранного продукта
            {
                var actsQueryParam = new ActsQueryParam(entity.Id);
                var acts = _actDAO.GetAll(actsQueryParam);
                _actDAO.DeleteAll(acts);
            }
        }
    }
}
