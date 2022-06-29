namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    interface IGetView_UseCase<TView>
    {
        TView Execute(int id);
    }
}
