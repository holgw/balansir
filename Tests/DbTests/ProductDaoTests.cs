using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Products;
using BalansirApp.Core.Products.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using SQLite;

namespace Tests.DbTests
{
    [TestClass]
    public class ProductDaoTests : CrudDaoTests<Product, Product, ProductsQueryParam>
    {
        protected override IDAO<Product, ProductsQueryParam> GetDAO(SQLiteConnection db)
        {
            return new ProductDAO(db);
        }

        protected override void CheckMapping(Product source, Product target)
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

        protected override Product GetRecordForCreate()
        {
            return new Product { Name = "N1", Code = "C1", Units = "U1", Description = "D1" };
        }

        protected override void GetRecordForUpdate(Product createdRecord)
        {
            createdRecord.Name = "N2";
            createdRecord.Code = "C2";
            createdRecord.Units = "U2";
            createdRecord.Description = "D2";
        }
    }
}
