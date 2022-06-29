using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common;
using BalansirApp.Core.Domains.Acts;

namespace BalansirApp.Core.Acts
{
    public interface IActsService : IEntityService<ActView, ActsQueryParam>
    {
    }
}