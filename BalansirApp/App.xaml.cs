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
        public App(ServiceCollection services)
        {
            InitializeComponent();
            this.SetupServices(services);

            using (var scope = ServiceProvider.CreateScope())
            {
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

        public void SetupServices(ServiceCollection services)
        {
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

            //services.AddSingleton<IServiceProvider>(x => App.ServiceProvider);
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
