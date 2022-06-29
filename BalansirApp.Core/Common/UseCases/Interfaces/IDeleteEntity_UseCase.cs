namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    interface IDeleteEntity_UseCase<TEntity>
    {
        void Execute(TEntity entity);
    }
}
