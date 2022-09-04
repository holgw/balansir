using BalansirApp.Core.Acts;
using BalansirApp.Core.Acts.UseCases.DeleteAct;
using BalansirApp.Core.Acts.UseCases.GetActsListView;
using BalansirApp.Core.Acts.UseCases.GetActView;
using BalansirApp.Core.Acts.UseCases.SaveAct;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Migrations.Tools;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using BalansirApp.Core.Products;
using BalansirApp.Core.Products.UseCases;
using BalansirApp.Core.Products.UseCases.DeleteProduct;
using BalansirApp.Core.Products.UseCases.GetProductsListView;
using BalansirApp.Core.Products.UseCases.SaveProduct;
using Microsoft.Extensions.DependencyInjection;

namespace BalansirApp.Core
{
    public static class DIConfig
    {
        public static void SetupCore(this ServiceCollection services)
        {
            services.AddSingleton<IDbBackupManager, DbBackupManager>();
            services.AddSingleton<IDbMigrationsManager, DbMigrationsManager>();           
            
            // Register Products Service
            services.AddTransient<IDeleteProduct_UseCase, DeleteProduct_UseCase>();
            services.AddTransient<IGetProductsListView_UseCase, GetProductsListView_UseCase>();
            services.AddTransient<IGetProductView_UseCase, GetProductView_UseCase>();
            services.AddTransient<ISaveProduct_UseCase, SaveProduct_UseCase>();
            services.AddSingleton<IProductsService, ProductsService>();

            // Register Acts Service
            services.AddTransient<IDeleteAct_UseCase, DeleteAct_UseCase>();
            services.AddTransient<IGetActsListView_UseCase, GetActsListView_UseCase>();
            services.AddTransient<IGetActView_UseCase, GetActView_UseCase>();
            services.AddTransient<ISaveAct_UseCase, SaveAct_UseCase>();
            services.AddSingleton<IActsService, ActsService>();
        }
    }
}