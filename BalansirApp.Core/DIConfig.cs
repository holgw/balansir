using BalansirApp.Core.Acts;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Migrations.Tools;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using BalansirApp.Core.Products;
using Microsoft.Extensions.DependencyInjection;

namespace BalansirApp.Core
{
    public static class DIConfig
    {
        public static void SetupCore(this ServiceCollection services)
        {
            services.AddSingleton<IDbMaintainService, DbMaintainService>();
            services.AddSingleton<IActsService, ActsService>();
            services.AddSingleton<IProductsService, ProductsService>();
        }
    }
}