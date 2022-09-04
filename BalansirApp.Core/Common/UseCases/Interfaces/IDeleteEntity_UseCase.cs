namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    public interface IDeleteEntity_UseCase<TEntity>
    {
        void Execute(TEntity entity);
    }
}
