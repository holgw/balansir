using BalansirApp.Core.Common.DataAccess;
using System;

namespace BalansirApp.Core.Acts.DataAccess
{
    public class ActsQueryParam : BaseQueryParam
    {
        public int? ProductId { get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }

        // CTOR
        public ActsQueryParam(
            int pageSize,
            int pageNumber) : base(pageSize, pageNumber)
        {
        }
        public ActsQueryParam(
            int pageSize,
            int pageNumber,
            int? productId,
            DateTime? startTime,
            DateTime? endTime) : base(pageSize, pageNumber)
        {
            ProductId = productId;
            StartTime = startTime;
            EndTime = endTime;
        }
        public ActsQueryParam(
            int? productId = null,
            DateTime? startTime = null,
            DateTime? endTime = null) : base()
        {
            ProductId = productId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
