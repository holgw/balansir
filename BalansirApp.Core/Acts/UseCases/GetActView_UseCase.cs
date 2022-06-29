using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;

namespace BalansirApp.Core.Acts.UseCases
{
    class GetActView_UseCase :
        AbstractGetView_UseCase<Act, ActView, ActsQueryParam>
    {
        private readonly IProductDAO _productDao;

        // CTOR
        public GetActView_UseCase(IDAO<Act, ActsQueryParam> dao, IProductDAO productDao) :
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
