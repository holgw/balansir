using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Acts.UseCases
{
    class GetActsListView_UseCase :
        AbstractGetListView_UseCase<Act, ActView, ActsQueryParam>
    {
        private readonly IProductDAO _productDao;

        // CTOR
        public GetActsListView_UseCase(IActDAO dao, IProductDAO productDao) :
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
