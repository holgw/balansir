using BalansirApp.Core.Acts;
using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Products;
using BalansirApp.Core.Products.DataAccess.Interfaces;
using System;
using System.Linq;

namespace Tests.DbTests
{
    internal static class Data
    {
        public static Product[] Products = new Product[]
        {
            new Product { Name = "N1", Code = "C1", Units = "U1", Description = "D1" },
            new Product { Name = "N2", Code = "C2", Units = "U2", Description = "D2" },
            new Product { Name = "N3", Code = "C3", Units = "U3", Description = "D3" },
            new Product { Name = "N4", Code = "C4", Units = "U4", Description = "D4" },
            new Product { Name = "N5", Code = "C5", Units = "U5", Description = "D5" },
            new Product { Name = "N6", Code = "C6", Units = "U6", Description = "D6" },
            new Product { Name = "N7", Code = "C7", Units = "U7", Description = "D7" },
        };

        public static Act[] Acts = new Act[]
{
            new Act { TimeStamp = DateTime.Now.AddHours(-0), ProductId = 1, Delta = +15 },
            new Act { TimeStamp = DateTime.Now.AddHours(-1), ProductId = 1, Delta = +02 },
            new Act { TimeStamp = DateTime.Now.AddHours(-2), ProductId = 1, Delta = +70 },
            new Act { TimeStamp = DateTime.Now.AddHours(-0), ProductId = 3, Delta = +05 },
            new Act { TimeStamp = DateTime.Now.AddHours(-1), ProductId = 3, Delta = -23 },
        };

        public static void Fill(IProductDAO productDAO, IActDAO actDAO)
        {
            productDAO.Fill(Products);
            actDAO.Fill(Acts);
        }
        public static void Fill<T, P>(this IDAO<T, P> crudDAO, T[] data)
            where T : DbRecord, new()
            where P : BaseQueryParam
        {
            foreach (var item in data)
            {
                crudDAO.Save(item, true);
            }
        }

        /// <summary>
        /// Синхронное выполнение операций над двумя массивами записей БД
        /// </summary>
        public static void CorrelateArrays<T>(this T[] sourceArr, T[] targetArr, Action<T, T> action)
            where T : DbRecord
        {
            foreach (var source in sourceArr)
            {
                var target = targetArr.Single(x => x.Id == source.Id);
                action(source, target);
            }
        }

        public static bool SameMinute(this DateTime dt)
        {
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            return dt == now;
        }
    }
}
