using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases.Interfaces;
using System;

namespace BalansirApp.Core.Common.UseCases
{
    public abstract class AbstractGetListView_UseCase<TRecord, TView, TParam> : IGetListView_UseCase<TView, TParam>
        where TRecord : DbRecord, new()
        where TView : class
        where TParam : BaseQueryParam
    {
        private readonly IDAO<TRecord, TParam> _dao;

        // CTOR
        protected AbstractGetListView_UseCase(IDAO<TRecord, TParam> dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        // METHODS: Public
        public ItemsPageQueryResult<TView, TParam> Execute(TParam queryParam)
        {
            return _dao.GetPage(queryParam)
                .CopyWithRefill(record => BuildView(record));
        }

        // METHODS: Protected
        protected abstract TView BuildView(TRecord record);
    }
}
