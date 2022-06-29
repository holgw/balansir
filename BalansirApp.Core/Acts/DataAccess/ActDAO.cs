using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.DataAccess;
using SQLite;

namespace BalansirApp.Core.Acts.DataAccess
{
    public class ActDAO : AbstractDAO<Act, ActsQueryParam>, IActDAO
    {
        // CTOR
        public ActDAO(SQLiteConnection db) : base(db)
        {
        }

        // METHODS: Protected
        protected override TableQuery<Act> Query(ActsQueryParam queryParam)
        {
            var q = base.Query(queryParam).OrderByDescending(x => x.TimeStamp);

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

            return q;
        }
    }
}
