using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DbTests
{
    public abstract class AbstractEntityServiceTests<TRecord, TView, TParam> : DaoTests<TRecord, TView, TParam>
        where TRecord : DbRecord, new ()
        where TView : IIdentifier
        where TParam : BaseQueryParam
    {
        [TestMethod]
        public void Test()
        {
            // CREATE: ARRANGE + ACT + ASSERT
            var service = this.GetService();
            var record = this.GetItemForCreate();
            service.SaveEntity(record);

            // READ: ARRANGE + ACT + ASSERT
            var entity = service.GetEntityView(record.Id);
            this.CheckMapping(record, entity);

            // UPDATE: ARRANGE + ACT + ASSERT
            this.GetItemForUpdate(record);
            service.SaveEntity(record);
            Assert.AreEqual(record.Id, 1);
            entity = service.GetEntityView(record.Id);
            this.CheckMapping(record, entity);

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
