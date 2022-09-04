using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common.UseCases.Interfaces;
using BalansirApp.Core.Domains.Acts;

namespace BalansirApp.Core.Acts.UseCases.GetActsListView
{
    public interface IGetActsListView_UseCase : IGetListView_UseCase<ActView, ActsQueryParam>
    {
    }
}
