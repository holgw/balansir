using BalansirApp.Core;
using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Tests.ServicesTests.TestDataSets;

namespace Tests.ServicesTests.Abstractions
{
    public abstract class AbstractEntityServiceTests<TRecord, TView, TParam>
        where TRecord : DbRecord, new()
        where TView : IIdentifier
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

        [TestMethod]
        public void Test()
        {
            // CREATE: ARRANGE + ACT + ASSERT
            var service = GetService();
            var record = GetItemForCreate();
            service.SaveEntity(record);

            // READ: ARRANGE + ACT + ASSERT
            var entity = service.GetEntityView(record.Id);
            CheckMapping(record, entity);

            // UPDATE: ARRANGE + ACT + ASSERT
            GetItemForUpdate(record);
            service.SaveEntity(record);
            Assert.AreEqual(record.Id, 1);
            entity = service.GetEntityView(record.Id);
            CheckMapping(record, entity);

            // DELETE: ARRANGE + ACT + ASSERT
            service.DeleteEntity(record);
            entity = service.GetEntityView(record.Id);
            Assert.AreEqual(record.Id, 1);
            Assert.AreEqual(entity, null);
        }

        // METHODS: Protected
        protected abstract TView GetItemForCreate();
        protected abstract void GetItemForUpdate(TView entityView);
        protected abstract void CheckMapping(TView source, TView target);
        protected abstract IEntityService<TView, TParam> GetService();
    }
}
