using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Acts.UseCases.GetActView
{
    class GetActView_UseCase :
        AbstractGetView_UseCase<Act, ActView, ActsQueryParam>,
        IGetActView_UseCase
    {
        private readonly IProductDAO _productDao;

        // CTOR
        public GetActView_UseCase(IActDAO dao, IProductDAO productDao) :
            base(dao)
        {
            _productDao = productDao ?? throw new ArgumentNullException(nameof(productDao));
        }

        // METHODS: Protected
        protected override ActView BuildView(Act record)
        {
            var product = _productDao.TryGet(record?.ProductId ?? 0);
            var view = ActView.Map(record, product);
            return view;
        }
    }
}
