using BalansirApp.Core.Products;
using System.Linq;

namespace Tests.ServicesTests.Extensions
{
    public static class ComparisonExtensions
    {
        public static bool IsEqualRecord(this Product source, Product target)
        {
            return
                source.Name == target.Name &&
                source.Code == target.Code &&
                source.Units == target.Units &&
                source.Description == target.Description &&
                source.Balance == target.Balance;
        }
        public static bool IsEqualRecords(this Product[] sources, Product[] targets)
        {
            foreach (var source in sources)
            {
                var target = targets.First(x => x.Id == source.Id);
                if (!source.IsEqualRecord(target))
                    return false;
            }

            return true;
        }
    }
}
