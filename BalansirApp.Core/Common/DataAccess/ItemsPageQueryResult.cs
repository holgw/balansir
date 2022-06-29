using System;
using System.Linq;

namespace BalansirApp.Core.Common.DataAccess
{
    public class ItemsPageQueryResult<T, P> where P : BaseQueryParam
    {
        public P QueryParam { get; }
        public T[] Items { get; }
        public int TotalItemsCount { get; }
        public int TotalPagesCount { get; }

        // CTOR
        public ItemsPageQueryResult(P queryParam, T[] items, int totalItemsCount, int totalPagesCount)
        {
            QueryParam = queryParam ?? throw new ArgumentNullException(nameof(queryParam));
            Items = items ?? throw new ArgumentNullException(nameof(items));
            TotalItemsCount = totalItemsCount;
            TotalPagesCount = totalPagesCount;
        }

        // METHODS: Public
        public ItemsPageQueryResult<TOther, P> CopyWithRefill<TOther>(Func<T, TOther> mapFunc)
        {
            var otherItems = Items.Select(x => mapFunc(x)).ToArray();
            return new ItemsPageQueryResult<TOther, P>(
                QueryParam,
                otherItems,
                TotalItemsCount,
                TotalPagesCount
            );
        }
    }
}
