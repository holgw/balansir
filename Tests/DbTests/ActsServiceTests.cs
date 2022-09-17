using BalansirApp.Core.Acts;
using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.Core.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.DbTests
{
    [TestClass]
    public class ActsServiceTests : AbstractEntityServiceTests<Act, ActView, ActsQueryParam>
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

        [TestMethod]
        public void CreateNewAct_Test()
        {                        
            // ARRANGE
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };

            // ACT
            var actView = new ActView() { TimeStamp = DateTime.Now, ProductId = productView.Id, Delta = +15 };
            _actsService.SaveEntity(actView);

            // ASSERT
            productView = _productsService.GetEntityView(productView.Id);
            Assert.AreEqual(productView.Balance, 15);
        }

        [TestMethod]
        public void DeleteAct_Test()
        {
            // ARRANGE
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            _productsService.SaveEntity(productView);

            var actView = new ActView() { TimeStamp = DateTime.Now, ProductId = productView.Id, Delta = +15 };
            _actsService.SaveEntity(actView);

            // ACT
            _actsService.DeleteEntity(actView);

            // ASSERT
            productView = _productsService.GetEntityView(productView.Id);
            Assert.AreEqual(productView.Balance, 0);
            actView = _actsService.GetEntityView(actView.Id);
            Assert.IsNull(actView);
        }

        // METHODS: Protected
        protected override ActView GetItemForCreate()
        {
            var productView = new ProductView() { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
            _productsService.SaveEntity(productView);

            return new ActView { TimeStamp = DateTime.Now, ProductId = 1, Delta = +15 };
        }
        protected override void GetItemForUpdate(ActView entityView)
        {
            entityView.TimeStamp = DateTime.Now.AddHours(-1);
            entityView.ProductId = 1;
            entityView.Delta = +02;
        }
        protected override void CheckMapping(ActView source, ActView target)
        {
            var checks = new bool[]
            {
                source.Id == target.Id,
                source.TimeStamp == target.TimeStamp,
                source.ProductId == target.ProductId,
                source.Delta == target.Delta,
            };

            if (checks.Any(x => x == false))
                throw new Exception("Mapping is broken!");
        }
        protected override IEntityService<ActView, ActsQueryParam> GetService()
        {
            return _actsService;
        }
    }
}
