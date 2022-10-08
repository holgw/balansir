using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Acts.UseCases.DeleteAct;
using BalansirApp.Core.Acts.UseCases.GetActsListView;
using BalansirApp.Core.Acts.UseCases.GetActView;
using BalansirApp.Core.Acts.UseCases.SaveAct;
using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BalansirApp.Core.Acts
{
    class ActsService : AbstractEntityService<ActView, ActsQueryParam>, IActsService
    {
        // CTOR
        public ActsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        // METHODS: Protected
        protected override ItemsPageQueryResult<ActView, ActsQueryParam> GetEntityListViewAction(IServiceScope serviceScope, ActsQueryParam queryParam)
        {
            var useCase = serviceScope.ServiceProvider.GetService<IGetActsListView_UseCase>();
            return useCase.Execute(queryParam);
        }
        protected override ActView GetEntityViewAction(IServiceScope serviceScope, int id)
        {
            var useCase = serviceScope.ServiceProvider.GetService<IGetActView_UseCase>();
            return useCase.Execute(id);
        }
        protected override void SaveEntityAction(IServiceScope serviceScope, ActView actView)
        {
            var useCase = serviceScope.ServiceProvider.GetService<ISaveAct_UseCase>();
            useCase.Execute(actView);
        }
        protected override void DeleteEntityAction(IServiceScope serviceScope, ActView entity)
        {
            var useCase = serviceScope.ServiceProvider.GetService<IDeleteAct_UseCase>();
            useCase.Execute(entity);
        }
    }
}
