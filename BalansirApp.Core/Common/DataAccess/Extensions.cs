using BalansirApp.Core.Common.DataAccess.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;

namespace BalansirApp.Core.Common.DataAccess
{
    public static class Extensions
    {
        public static TableQuery<T> GetPage<T>(this TableQuery<T> q, int pageSize, int page)
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

        public static void ExecuteDbConnection(this IAppFilesLocator appFilesLocator, Action<SQLiteConnection> action)
        {
            string dbPath = appFilesLocator.GetDatabasePath();
            using (var db = new SQLiteConnection(dbPath))
            {
                db.RunInTransaction(() => action(db));
            }
        }
    }
}
