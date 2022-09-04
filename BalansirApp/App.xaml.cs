using BalansirApp.Core;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
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
            this.SetupServices();

            var migrationsManager = ServiceProvider.GetService<IDbMigrationsManager>();
            migrationsManager.CheckAndApplyMigrations();

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

        void SetupServices()
        {
            var services = new ServiceCollection();

            services.SetupCore();

            services.AddSingleton<ISettingsProvider, Settings>();
            services.AddSingleton(x => DependencyService.Resolve<IAppFilesLocator>());
            services.AddSingleton(x => DependencyService.Resolve<IAppVersionProvider>());

            services.AddTransient<Shell_ViewModel>();
            services.AddTransient<ActEdit_ViewModel>();
            services.AddTransient<ActsList_ViewModel>();
            services.AddTransient<ProductEdit_ViewModel>();
            services.AddTransient<ProductsList_ViewModel>();
            services.AddTransient<SettingsEdit_ViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
