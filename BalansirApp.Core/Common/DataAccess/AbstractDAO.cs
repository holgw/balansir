using BalansirApp.Core.Common.DataAccess.Interfaces;
using LinqToDB;
using System;
using System.Linq;

namespace BalansirApp.Core.Common.DataAccess
{
    /// <summary>
    /// Абстрактный класс для доступа к таблице к БД 
    /// (create\read\update\delete)
    /// </summary>
    public abstract class AbstractDAO<T, P> : IDAO<T, P>
        where T : DbRecord, new()
        where P : BaseQueryParam
    {
        protected readonly MySQLiteConnection _db;

        protected abstract ITable<T> Table { get; }

        // CTOR
        public AbstractDAO(MySQLiteConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // METHODS: Public
        public T[] GetAll(P queryParam)
        {
            return Query(queryParam).ToArray();
        }
        public ItemsPageQueryResult<T, P> GetPage(P queryParam)
        {
            int pageNo = queryParam?.PageNumber ?? 1;
            int pageSize = queryParam?.PageSize ?? int.MaxValue;

            var q = Query(queryParam);
            int totalItemsCount = q.Count();
            int totalPagesCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
            var pageItems = q.GetPage(pageSize, pageNo).ToArray();

            return new ItemsPageQueryResult<T, P>(
                queryParam,
                pageItems,
                totalItemsCount,
                totalPagesCount
            );
        }
        public T TryGet(int id)
        {
            return this.Table.FirstOrDefault(x => x.Id == id);
        }
        public int Delete(int id)
        {
            return this.Table.Delete(x => x.Id == id);
        }
        public int Save(T item, bool insertStrict = false)
        {
            if (item.Id != 0 && !insertStrict)
            {
                _db.Update(item);
                return item.Id;
            }
            else
            {
                item.Id = _db.InsertWithInt32Identity(item);
                return item.Id;
            }
        }

        // METHODS: Protected
        protected virtual IQueryable<T> Query(P queryParam)
        {
            return this.Table;
        }
    }
}
