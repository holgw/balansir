using BalansirApp.Core.Common.UseCases.Interfaces;
using BalansirApp.Core.Domains.Acts;

namespace BalansirApp.Core.Acts.UseCases.DeleteAct
{
    public interface IDeleteAct_UseCase : IDeleteEntity_UseCase<ActView>
    {
    }
}
