namespace BalansirApp.Core.Common.DataAccess
{
    public class BaseQueryParam
    {
        public int PageSize { get; }
        public int PageNumber { get; }

        // CTOR
        public BaseQueryParam(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
        public BaseQueryParam()
        {
        }
    }
}
