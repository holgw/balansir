using System.Collections.Generic;

namespace BalansirApp.Utility
{
    public static class Extensions
    {
        public static HashSet<int> ToHashSet(this IEnumerable<int> values)
        {
            var result = new HashSet<int>();

            foreach (var val in values)
            {
                result.Add(val);
            }

            return result;
        }
    }
}
