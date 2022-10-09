using BalansirApp.Core.Acts;
using BalansirApp.Core.Common;
using BalansirApp.Core.Products;
using BalansirApp.Core.Products.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Tests.ServicesTests.Abstractions;

namespace Tests.ServicesTests
{
    [TestClass]
    public class ProductsServiceTests : AbstractEntityServiceTests<Product, ProductView, ProductsQueryParam>
    {
        static IProductsService _productsService;
        static IActsService _actsService;

        [TestInitialize]
        public override void Startup()
        {
            base.Startup();

            _productsService = ServiceProvider.GetService<IProductsService>();
            _actsService = ServiceProvider.GetService<IActsService>();
        }

        // ---

        [TestMethod, Description("Простое создание нового продукта")]
        public void CreateNewProduct_Test1()
        {
            // ARRANGE
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };

            // ACT
            _productsService.SaveEntity(productView);

            // ASSERT
            Assert.AreNotSame(productView.Id, 0);
            var productViewLoaded = _productsService.GetEntityView(productView.Id);
            Assert.IsNotNull(productViewLoaded);
            Assert.AreEqual(productView.Name, productViewLoaded.Name);
        }

        [TestMethod, Description("Попытка создания двух продуктов с одинаковыми наименованиями")]
        public void CreateNewProduct_Test2()
        {
            // ARRANGE
            var productView1 = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            var productView2 = new ProductView() { Name = "N1", Code = "C2", Units = "U2", Description = "D2" };

            // ACT
            _productsService.SaveEntity(productView1);

            // ASSERT
            Assert.ThrowsException<SqliteException>(() => _productsService.SaveEntity(productView2));
        }

        [TestMethod, Description("Попытка создания двух продуктов с одинаковыми кодами")]
        public void CreateNewProduct_Test3()
        {
            // ARRANGE
            var productView1 = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            var productView2 = new ProductView() { Name = "N2", Code = "C1", Units = "U2", Description = "D2" };

            // ACT
            _productsService.SaveEntity(productView1);

            // ASSERT
            Assert.ThrowsException<SqliteException>(() => _productsService.SaveEntity(productView2));
        }

        [TestMethod, Description("Удаление изделия и всех прикрепленных к нему актов")]
        public void DeleteProduct_Test()
        {
            // ARRANGE
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            _productsService.SaveEntity(productView);

            var actView = new ActView() { TimeStamp = DateTime.Now, ProductId = productView.Id, Delta = +15 };
            _actsService.SaveEntity(actView);

            // ACT
            _productsService.DeleteEntity(productView);

            // ASSERT
            Assert.IsNull(_actsService.GetEntityView(actView.Id));
            Assert.IsNull(_productsService.GetEntityView(productView.Id));
        }

        // METHODS: Protected
        protected override ProductView GetItemForCreate()
        {
            return new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
        }
        protected override void GetItemForUpdate(ProductView entityView)
        {
            entityView.Name = "N2";
            entityView.Code = "C2";
            entityView.Units = "U2";
            entityView.Description = "D2";
        }
        protected override void CheckMapping(ProductView source, ProductView target)
        {
            var checks = new bool[]
            {
                source.Id == target.Id,
                source.Name == target.Name,
                source.Code == target.Code,
                source.Units == target.Units,
                source.Description == target.Description,
                source.Balance == target.Balance,
            };

            if (checks.Any(x => x == false))
                throw new Exception("Mapping is broken!");
        }
        protected override IEntityService<ProductView, ProductsQueryParam> GetService()
        {
            return _productsService;
        }
    }

    //[TestClass]
    //public class TestDbUpdate
    //{
    //    string _dbPath = Path.Combine(Environment.CurrentDirectory, "dbMainTest.db");
    //    protected IAppFilesLocator _appFilesLocator;
    //    protected IDbMaintainService _dbMaintainService;

    //    [TestInitialize]
    //    public virtual void Startup()
    //    {
    //        var appFilesLocatorMock = new Mock<IAppFilesLocator>();
    //        appFilesLocatorMock.Setup(x => x.GetDatabasePath()).Returns(_dbPath);
    //        _appFilesLocator = appFilesLocatorMock.Object;

    //        // Создадим новый файл БД со структурой данных
    //        _dbMaintainService = new DbMaintainService(_appFilesLocator);
    //        _dbMaintainService.InitializeDatabase();
    //    }

    //    [TestMethod]
    //    public void Test1()
    //    {

    //    }
    //}
}
