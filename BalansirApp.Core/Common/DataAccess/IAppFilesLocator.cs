namespace BalansirApp.Core.Common.DataAccess
{
    /// <summary>
    /// Интерфейс доступа к путям файлов, используемых в  приложении, 
    /// на устройстве пользователя . Реализация класса зависит от платформы устройства.
    /// Развруливаются зависимости через DependencyService
    /// </summary>
    public interface IAppFilesLocator
    {
        string GetDatabasePath();
    }
}
