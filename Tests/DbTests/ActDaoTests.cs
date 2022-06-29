using BalansirApp.Core.Acts;
using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Domains.Acts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using System;
using System.Linq;

namespace Tests.DbTests
{
    [TestClass]
    public class ActDaoTests : CrudDaoTests<Act, ActView, ActsQueryParam>
    {
        protected override IDAO<Act, ActsQueryParam> GetDAO(SQLiteConnection db)
        {
            return new ActDAO(db);
        }

        protected override void CheckMapping(Act source, Act target)
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

        protected override Act GetRecordForCreate()
        {
            return new Act { TimeStamp = DateTime.Now.AddHours(-0), ProductId = 1, Delta = +15 };
        }

        protected override void GetRecordForUpdate(Act createdRecord)
        {
            createdRecord.TimeStamp = DateTime.Now.AddHours(-1);
            createdRecord.ProductId = 1;
            createdRecord.Delta = +02;
        }
    }
}
