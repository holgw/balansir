namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    public interface ISaveEntity_UseCase<TEntity>
    {
        void Execute(TEntity entity);
    }
}
