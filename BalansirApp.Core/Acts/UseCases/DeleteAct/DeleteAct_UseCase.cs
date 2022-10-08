using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Acts.UseCases.DeleteAct
{
    /// <summary>
    /// Создание нового акта с кешированием остатков продукции на складе
    /// </summary>
    class DeleteAct_UseCase : IDeleteAct_UseCase
    {
        private readonly IActDAO _actDAO;
        private readonly IProductDAO _productDAO;

        // CTOR
        public DeleteAct_UseCase(IActDAO actDAO, IProductDAO productDAO)
        {
            _actDAO = actDAO ?? throw new ArgumentNullException(nameof(actDAO));
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
        }

        public void Execute(ActView actView)
        {
            actView.Id.Validate(nameof(actView.Id));

            var act = _actDAO.TryGet(actView.Id);
            if (act == null)
            {
                string errMsg = $"Unknown {nameof(Act)}.{nameof(Act.Id)}";
                throw new InvalidOperationException(errMsg);
            }

            // Откатим баланс продукта
            {
                var product = _productDAO.TryGet(actView.ProductId);
                if (product != null)
                {
                    product.Balance -= actView.Delta;
                    _productDAO.Save(product);
                }
            }

            _actDAO.Delete(act.Id);
        }
    }
}
