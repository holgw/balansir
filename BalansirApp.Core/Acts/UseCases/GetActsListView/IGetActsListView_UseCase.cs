using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common.UseCases.Interfaces;

namespace BalansirApp.Core.Acts.UseCases.GetActsListView
{
    public interface IGetActsListView_UseCase : IGetListView_UseCase<ActView, ActsQueryParam>
    {
    }
}
