using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common;

namespace BalansirApp.Core.Acts
{
    public interface IActsService : IEntityService<ActView, ActsQueryParam>
    {
    }
}