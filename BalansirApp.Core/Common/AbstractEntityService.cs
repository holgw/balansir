using BalansirApp.Core.Common.DataAccess;
using System;

namespace BalansirApp.Core.Common
{
    abstract class AbstractEntityService<TView, TQueryParam> : IEntityService<TView, TQueryParam>
        where TQueryParam : BaseQueryParam
    {
        protected readonly IAppFilesLocator _appFilesLocator;

        // CTOR
        public AbstractEntityService(IAppFilesLocator appFilesLocator)
        {
            _appFilesLocator = appFilesLocator ?? throw new ArgumentNullException(nameof(appFilesLocator));
        }

        // METHODS: Public
        public abstract void DeleteEntity(TView entityView);
        public abstract ItemsPageQueryResult<TView, TQueryParam> GetEntityListView(TQueryParam queryParam);
        public abstract TView GetEntityView(int id);
        public abstract void SaveEntity(TView entityView);
    }
}
