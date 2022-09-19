using BalansirApp.Core;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace Tests.DbTests
{
    public abstract class DaoTests<TRecord, TView, TParam>
        where TParam : BaseQueryParam
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        [TestInitialize]
        public virtual void Startup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IAppFilesLocator, AppFilesLocator_Test>(); 
            services.SetupCore();
            ServiceProvider = services.BuildServiceProvider();

            File.Delete(ServiceProvider.GetService<IAppFilesLocator>().DbPath);

            using (var scope = ServiceProvider.CreateScope())
            {
                var migrationsManager = scope.ServiceProvider.GetService<IDbMigrationsManager>();
                migrationsManager.CheckAndApplyMigrations();
            }
        }
    }

    class AppFilesLocator_Test : BaseAppFilesLocator
    {
        public override string DbFolder => Environment.CurrentDirectory;
    }

    [TestClass]
    public class ConnectionTests
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        [TestInitialize]
        public virtual void Startup()
        {
            if (ServiceProvider != null)
            {
                ServiceProvider = null;
                GC.Collect();
            }

            var services = new ServiceCollection();
            services.AddSingleton<IAppFilesLocator, AppFilesLocator_Test>();
            services.AddScoped<SQLiteConnection>();
            ServiceProvider = services.BuildServiceProvider();

            File.Delete(ServiceProvider.GetService<IAppFilesLocator>().DbPath);
        }

        [TestMethod]
        public void Test1()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<SQLiteConnection>())
                {
                    db.BeginTransaction();
                    try
                    {
                        Thread.Sleep(10);
                        db.CommitTransaction();
                    }
                    catch
                    {
                        db.RollbackTransaction();
                        throw;
                    }
                }
            }
        }

        [TestMethod]
        public void Test2()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<SQLiteConnection>())
                {
                    db.BeginTransaction();
                    try
                    {
                        Thread.Sleep(10);
                        db.CommitTransaction();
                    }
                    catch
                    {
                        db.RollbackTransaction();
                        throw;
                    }
                }
            }
        }

        [TestMethod]
        public void Test4()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<SQLiteConnection>())
                {
                    db.BeginTransaction();
                    try
                    {
                        Thread.Sleep(10);
                        db.CommitTransaction();
                    }
                    catch
                    {
                        db.RollbackTransaction();
                        throw;
                    }
                }
            }
        }
    }
}
