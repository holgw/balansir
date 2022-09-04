namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    public interface IGetView_UseCase<TView>
    {
        TView Execute(int id);
    }
}
