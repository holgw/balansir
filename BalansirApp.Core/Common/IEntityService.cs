using BalansirApp.Core.Common.DataAccess;

namespace BalansirApp.Core.Common
{
    public interface IEntityService<TView, TQueryParam>
        where TQueryParam : BaseQueryParam
    {
        void DeleteEntity(TView entityView);
        ItemsPageQueryResult<TView, TQueryParam> GetEntityListView(TQueryParam queryParam);
        TView GetEntityView(int id);
        void SaveEntity(TView entityView);
    }
}
