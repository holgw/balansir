using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.DataAccess;
using LinqToDB;
using System.Linq;

namespace BalansirApp.Core.Acts.DataAccess
{
    public class ActDAO : AbstractDAO<Act, ActsQueryParam>, IActDAO
    {
        protected override ITable<Act> Table => _db.Acts;

        // CTOR
        public ActDAO(MySQLiteConnection db) : base(db)
        {
        }

        // METHODS: Protected
        protected override IQueryable<Act> Query(ActsQueryParam queryParam)
        {
            var q = base.Query(queryParam);

            if (queryParam != null)
            {
                if (queryParam.ProductId.HasValue && queryParam.ProductId > 0)
                {
                    q = q.Where(x => x.ProductId == queryParam.ProductId);
                }

                if (queryParam.StartTime.HasValue)
                {
                    q = q.Where(x => x.TimeStamp >= queryParam.StartTime);
                }

                if (queryParam.EndTime.HasValue)
                {
                    q = q.Where(x => x.TimeStamp <= queryParam.EndTime);
                }
            }

            return q.OrderByDescending(x => x.TimeStamp);
        }
    }
}
