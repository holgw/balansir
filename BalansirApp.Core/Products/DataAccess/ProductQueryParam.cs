using BalansirApp.Core.Common.DataAccess;

namespace BalansirApp.Core.Products.DataAccess
{
    public class ProductsQueryParam : BaseQueryParam
    {
        /// <summary>
        /// Часть имени референса
        /// </summary>
        public string ProductName { get; }

        // CTOR
        public ProductsQueryParam(
            int pageSize,
            int pageNumber,
            string itemReferenceName) : base(pageSize, pageNumber)
        {
            ProductName = itemReferenceName;
        }
        public ProductsQueryParam(string itemReferenceName) : base(0, 0)
        {
            ProductName = itemReferenceName;
        }
    }
}
