using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JstorCrawl.Domain
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> RestOfList<T>(this IEnumerable<T> queue) where T : class
        {
            bool first = false;
            foreach (var t in queue)
            {
                if (!first) yield return t;
                first = true;
            }
        }
    }
}
