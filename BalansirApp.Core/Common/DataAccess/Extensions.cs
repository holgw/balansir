using BalansirApp.Core.Common.DataAccess.Interfaces;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalansirApp.Core.Common.DataAccess
{
    public static class Extensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> q, int pageSize, int page)
        {
            return q.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static void DeleteAll<T, P>(this IDAO<T, P> dao, IEnumerable<T> records)
            where T : DbRecord, new()
            where P : BaseQueryParam
        {
            foreach (var record in  records)
            {
                dao.Delete(record.Id);
            }
        }
    }
}
