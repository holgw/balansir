using BalansirApp.Core.Acts;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Products;

namespace BalansirApp.Core.Common.DataAccess
{
    /// <summary>
    /// Инструмент для инициализации БД --
    /// создания файла БД, таблиц и т.д.
    /// </summary>
    internal class DbMaintainService : IDbMaintainService
    {
        private readonly IAppFilesLocator _appFilesLocator;

        // CTOR
        public DbMaintainService(IAppFilesLocator appFilesLocator)
        {
            _appFilesLocator = appFilesLocator ?? throw new System.ArgumentNullException(nameof(appFilesLocator));
        }

        // METHODS: Public
        public void InitializeDatabase()
        {
            // Если файла БД по указанному пути не существует,
            // то файл будет создан автоматически
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                // Создадим таблицы в БД, если их не существует
                db.CreateTable<Product>();
                db.CreateTable<Act>();
            });
        }
    }
}
