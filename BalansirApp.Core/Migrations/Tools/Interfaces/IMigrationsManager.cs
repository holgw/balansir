namespace BalansirApp.Core.Migrations.Tools.Interfaces
{
    public interface IDbMigrationsManager
    {
        void CheckAndApplyMigrations();
    }
}