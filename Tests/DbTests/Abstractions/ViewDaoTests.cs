using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Products.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DbTests
{
    public abstract class ViewDaoTests<TRecord, TView, TParam> : DaoTests<TRecord, TView, TParam>
        where TRecord : DbRecord, new()
        where TParam : BaseQueryParam
    {
        protected abstract IDAO<TRecord, TParam> ViewDAO { get; }
        protected abstract TRecord[] Records { get; }

        [TestInitialize]
        public override void Startup()
        {
            base.Startup();

            //_appFilesLocator.ExecuteDbConnection(db =>
            //{
            //    var actDAO = new ActDAO(db);
            //    var productDAO = new ProductDAO(db);
            //    Data.Fill(productDAO, actDAO);
            //});
        }

        // ===

        [TestMethod]
        public void GetAll_Test()
        {
            // ACT
            var items = ViewDAO.GetAll(null);

            // ASSERT
            Assert.AreEqual(items.Length, Records.Length);
        }

        [TestMethod]
        public void Materialize_Test()
        {
            // ACT
            var items = ViewDAO.GetAll(null);

            // ASSERT
            foreach (var item in items)
            {
                CheckViewMapping(item);
            }
        }

        public virtual void GetPage_Test(int pageSize, int pageNumber, int totalPagesCount, int pageItemsCount)
        {
            // ARRANGE
            var qParam = GetParam(pageSize, pageNumber);

            // ACT
            var page = ViewDAO.GetPage(qParam);

            // ASSERT
            Assert.AreEqual(page.TotalItemsCount, Records.Length);
            Assert.AreEqual(page.TotalPagesCount, totalPagesCount);
            Assert.AreEqual(page.Items.Length, pageItemsCount);
            Assert.AreEqual(page.QueryParam, qParam);
        }

        // ===

        public abstract TParam GetParam(int pageSize, int pageNumber);
        public abstract void CheckViewMapping(TRecord view);
    }
}
