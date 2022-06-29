namespace BalansirApp.Core.Common.UseCases.Interfaces
{
    interface ISaveEntity_UseCase<TEntity>
    {
        void Execute(TEntity entity);
    }
}
