using BalansirApp.Core.Acts;
using BalansirApp.Core.Common;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.Core.Products;
using BalansirApp.Core.Products.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using System;
using System.Linq;

namespace Tests.DbTests
{
    [TestClass]
    public class ProductsServiceTests : AbstractEntityServiceTests<Product, ProductView, ProductsQueryParam>
    {
        [TestMethod, Description("Простое создание нового продукта")]
        public void CreateNewProduct_Test1()
        {
            // ARRANGE
            var productsService = new ProductsService(_appFilesLocator);
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };

            // ACT
            productsService.SaveEntity(productView);

            // ASSERT
            Assert.AreNotSame(productView.Id, 0);
            var productViewLoaded = productsService.GetEntityView(productView.Id);
            Assert.IsNotNull(productViewLoaded);
            Assert.AreEqual(productView.Name, productViewLoaded.Name);
        }

        [TestMethod, Description("Попытка создания двух продуктов с одинаковыми наименованиями")]
        public void CreateNewProduct_Test2()
        {
            // ARRANGE
            var productsService = new ProductsService(_appFilesLocator);
            var productView1 = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            var productView2 = new ProductView() { Name = "N1", Code = "C2", Units = "U2", Description = "D2" };

            // ACT
            productsService.SaveEntity(productView1);

            // ASSERT
            Assert.ThrowsException<SQLiteException>(() => productsService.SaveEntity(productView2));
        }

        [TestMethod, Description("Попытка создания двух продуктов с одинаковыми кодами")]
        public void CreateNewProduct_Test3()
        {
            // ARRANGE
            var productsService = new ProductsService(_appFilesLocator);
            var productView1 = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            var productView2 = new ProductView() { Name = "N2", Code = "C1", Units = "U2", Description = "D2" };

            // ACT
            productsService.SaveEntity(productView1);

            // ASSERT
            Assert.ThrowsException<SQLiteException>(() => productsService.SaveEntity(productView2));
        }

        [TestMethod, Description("Удаление изделия и всех прикрепленных к нему актов")]
        public void DeleteProduct_Test()
        {
            // ARRANGE
            var productsService = new ProductsService(_appFilesLocator);
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            productsService.SaveEntity(productView);

            var actsService = new ActsService(_appFilesLocator);
            var actView = new ActView() { TimeStamp = DateTime.Now, ProductId = productView.Id, Delta = +15 };
            actsService.SaveEntity(actView);

            // ACT
            productsService.DeleteEntity(productView);

            // ASSERT
            Assert.IsNull(actsService.GetEntityView(actView.Id));
            Assert.IsNull(productsService.GetEntityView(productView.Id));
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
            return new ProductsService(_appFilesLocator);
        }
    }
}
