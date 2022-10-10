namespace BalansirApp.Core.Migrations.Tools.Interfaces
{
    interface IDataDefinitionBase
    {
        IDataDefinitionTable<TTable> AddTable<TTable>();
    }
}