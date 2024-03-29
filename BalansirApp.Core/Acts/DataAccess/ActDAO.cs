﻿using BalansirApp.Core.Acts.DataAccess.Interfaces;
using BalansirApp.Core.Common.DataAccess;
using SQLite;
using System.Linq;

namespace BalansirApp.Core.Acts.DataAccess
{
    public class ActDAO : AbstractDAO<Act, ActsQueryParam>, IActDAO
    {
        protected override TableQuery<Act> Table => _db.Table<Act>();

        // CTOR
        public ActDAO(SQLiteConnection db) : base(db)
        {
        }

        // METHODS: Protected
        protected override TableQuery<Act> Query(ActsQueryParam queryParam)
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
