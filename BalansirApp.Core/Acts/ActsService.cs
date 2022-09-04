using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Acts.UseCases;
using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.Core.Products.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace BalansirApp.Core.Acts
{
    class ActsService : AbstractEntityService<ActView, ActsQueryParam>, IActsService
    {
        // CTOR
        public ActsService(IAppFilesLocator appFilesLocator) :
            base(appFilesLocator)
        {
        }

        // METHODS: Protected
        protected override ItemsPageQueryResult<ActView, ActsQueryParam> GetEntityListViewAction(IServiceScope serviceScope, ActsQueryParam queryParam)
        {
            ItemsPageQueryResult<ActView, ActsQueryParam> result = null;

            serviceScope.ServiceProvider.GetService<>
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var actDAO = new ActDAO(db);
                var productDAO = new ProductDAO(db);
                var useCase = new GetActsListView_UseCase(actDAO, productDAO);
                result = useCase.Execute(queryParam);
            });

            return result;
        }
        public override ActView GetEntityView(int id)
        {
            ActView actView = null;

            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var actDAO = new ActDAO(db);
                var productDAO = new ProductDAO(db);
                var useCase = new GetActView_UseCase(actDAO, productDAO);
                actView = useCase.Execute(id);
            });

            return actView;
        }
        public override void SaveEntity(ActView actView)
        {
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var actDAO = new ActDAO(db);
                var productDAO = new ProductDAO(db);
                var useCase = new SaveAct_UseCase(actDAO, productDAO);
                useCase.Execute(actView);
            });
        }
        public override void DeleteEntity(ActView entity)
        {
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                var actDAO = new ActDAO(db);
                var productDAO = new ProductDAO(db);
                var useCase = new DeleteAct_UseCase(actDAO, productDAO);
                useCase.Execute(entity);
            });
        }
    }
}
