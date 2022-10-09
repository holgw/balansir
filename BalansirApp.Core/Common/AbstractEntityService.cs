using BalansirApp.Core.Common.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BalansirApp.Core.Common
{
    abstract class AbstractEntityService<TView, TQueryParam> : IEntityService<TView, TQueryParam>
        where TView : class
        where TQueryParam : BaseQueryParam
    {
        private readonly IServiceProvider _serviceProvider;

        // CTOR
        public AbstractEntityService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        // METHODS: Public
        public void DeleteEntity(TView entityView)
        {
            this.ExecuteInScope(scope => this.DeleteEntityAction(scope, entityView));
        }
        public ItemsPageQueryResult<TView, TQueryParam> GetEntityListView(TQueryParam queryParam)
        {
            ItemsPageQueryResult<TView, TQueryParam> result = null;
            this.ExecuteInScope(scope => result = this.GetEntityListViewAction(scope, queryParam));
            return result;
        }
        public TView GetEntityView(int id)
        {
            TView result = null;
            this.ExecuteInScope(scope => result = this.GetEntityViewAction(scope, id));
            return result;
        }
        public void SaveEntity(TView entityView)
        {
            this.ExecuteInScope(scope => this.SaveEntityAction(scope, entityView));
        }

        // METHODS: Protected
        protected abstract void DeleteEntityAction(IServiceScope serviceScope, TView entityView);
        protected abstract ItemsPageQueryResult<TView, TQueryParam> GetEntityListViewAction(IServiceScope serviceScope, TQueryParam queryParam);
        protected abstract TView GetEntityViewAction(IServiceScope serviceScope, int id);
        protected abstract void SaveEntityAction(IServiceScope serviceScope, TView entityView);
        protected virtual void ExecuteInScope(Action<IServiceScope> action)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<SQLiteConnection>())
                {
                    db.BeginTransaction();
                    try
                    {
                        action(scope);
                        db.CommitTransaction();
                    }
                    catch
                    {
                        db.RollbackTransaction();
                        throw;
                    }
                }                
            }
        }
    }
}
