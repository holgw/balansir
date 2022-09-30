using BalansirApp.Core;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using BalansirApp.Utility;
using BalansirApp.ViewModels.Acts;
using BalansirApp.ViewModels.Common;
using BalansirApp.ViewModels.ItemReferences;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace BalansirApp
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        // CTOR
        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            services.SetupCore();
            services.SetupServices();
            ServiceProvider = services.BuildServiceProvider();

            var appFilesLoc = ServiceProvider.GetService<IAppFilesLocator>();
            LinqToDB.DataProvider.SQLite.SQLiteTools.CreateDatabase(appFilesLoc.DbPath);

            using (var scope = ServiceProvider.CreateScope())
            {               
                var db = new MySQLiteConnection(appFilesLoc);

                var migrationsManager = scope.ServiceProvider.GetService<IDbMigrationsManager>();
                migrationsManager.CheckAndApplyMigrations();
            }

            this.MainPage = new AppShell();
        }

        public static TViewModel GetViewModel<TViewModel>()
            where TViewModel : BaseViewModel
        {
            return ServiceProvider.GetService<TViewModel>();
        }

        public static TViewModel GetDependency<TViewModel>()
        {
            return ServiceProvider.GetService<TViewModel>();
        }
    }

    internal static class DiExtensions
    {
        public static void SetupServices(this ServiceCollection services)
        {
            services.AddSingleton<ISettingsProvider, Settings>();
            services.AddSingleton(x => DependencyService.Resolve<IAppFilesLocator>());
            services.AddSingleton(x => DependencyService.Resolve<IAppVersionProvider>());

            services.AddTransient<Shell_ViewModel>();
            services.AddTransient<ActEdit_ViewModel>();
            services.AddTransient<ActsList_ViewModel>();
            services.AddTransient<ProductEdit_ViewModel>();
            services.AddTransient<ProductsList_ViewModel>();
            services.AddTransient<SettingsEdit_ViewModel>();
        }
    }
}
