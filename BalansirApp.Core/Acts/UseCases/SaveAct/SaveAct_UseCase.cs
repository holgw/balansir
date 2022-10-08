using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Acts.UseCases.SaveAct
{
    /// <summary>
    /// Создание нового акта с кешированием остатков продукции на складе
    /// </summary>
    class SaveAct_UseCase : ISaveAct_UseCase
    {
        private readonly IActDAO _actDAO;
        private readonly IProductDAO _productDAO;

        // CTOR
        public SaveAct_UseCase(IActDAO actDAO, IProductDAO productDAO)
        {
            _actDAO = actDAO ?? throw new ArgumentNullException(nameof(actDAO));
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
        }

        public void Execute(ActView view)
        {
            var entity = new Act();
            var newProduct = _productDAO.TryGet(view.ProductId);

            if (newProduct == null)
            {
                string errMsg = "Undefined Act.ProductId";
                throw new InvalidOperationException(errMsg);
            }

            decimal newProductDelta = view.Delta;

            if (view.Id > 0)
            {
                var oldAct = _actDAO.TryGet(view.Id);
                entity = oldAct;

                if (view == null)
                {
                    string errMsg = "Unknown Act.Id";
                    throw new InvalidOperationException(errMsg);
                }

                if (oldAct.ProductId != view.ProductId)
                {
                    var oldProduct = _productDAO.TryGet(oldAct.ProductId);
                    if (oldProduct != null)
                    {
                        oldProduct.Balance -= oldAct.Delta;
                        _productDAO.Save(oldProduct);
                    }
                }
                else
                {
                    newProductDelta -= oldAct.Delta;
                }
            }

            newProduct.Balance += newProductDelta;
            _productDAO.Save(newProduct);

            RefillItem(entity, view);
            _actDAO.Save(entity);
            view.Id = entity.Id;
        }

        void RefillItem(Act act, ActView actView)
        {
            act.Id = actView.Id;
            act.Delta = actView.Delta;
            act.ProductId = actView.ProductId;
            act.TimeStamp = actView.TimeStamp;
            act.Comment = actView.Comment;
        }
    }
}
