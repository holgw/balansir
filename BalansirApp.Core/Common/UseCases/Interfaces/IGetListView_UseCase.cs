using BalansirApp.Core.Common.DataAccess;

namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    public interface IGetListView_UseCase<TView, TParam> where TParam : BaseQueryParam
    {
        ItemsPageQueryResult<TView, TParam> Execute(TParam queryParam);
    }
}
