using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace Tests.DbTests
{
    public abstract class CrudDaoTests<TRecord, TView, TParam> : DaoTests<TRecord, TView, TParam>
        where TRecord : DbRecord, new()
        where TParam : BaseQueryParam
    {
        [TestMethod]
        public void Test()
        {
            _appFilesLocator.ExecuteDbConnection(db =>
            {
                // CREATE: ARRANGE + ACT + ASSERT
                var dao = this.GetDAO(db);                
                var record = this.GetRecordForCreate();
                dao.Save(record);
                Assert.AreEqual(record.Id, 1);

                // READ: ARRANGE + ACT + ASSERT
                var dbRecord = dao.TryGet(record.Id);
                this.CheckMapping(record, dbRecord);

                // UPDATE: ARRANGE + ACT + ASSERT
                this.GetRecordForUpdate(record);
                dao.Save(record);
                Assert.AreEqual(record.Id, 1);
                dbRecord = dao.TryGet(record.Id);
                this.CheckMapping(record, dbRecord);

                // DELETE: ARRANGE + ACT + ASSERT
                dao.Delete(record.Id);
                dbRecord = dao.TryGet(record.Id);
                Assert.AreEqual(record.Id, 1);
                Assert.AreEqual(dbRecord, null);
            });
        }

        protected abstract TRecord GetRecordForCreate();
        protected abstract void GetRecordForUpdate(TRecord createdRecord);
        protected abstract void CheckMapping(TRecord source, TRecord target);
        protected abstract IDAO<TRecord, TParam> GetDAO(SQLiteConnection db);
    }
}
