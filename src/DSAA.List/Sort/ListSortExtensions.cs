using System;
using System.Collections.Generic;

namespace DSAA.List.Sort
{
    public static class ListSortExtensions
    {
        public static IList<T> Sort<T>(this IList<T> collection,
            Func<SortOptions<T>, SortOptions<T>> optionsFactory)
        {
            var options = optionsFactory(SortOptions<T>.Default());
            return options.Strategy.Sort(collection);
        }
    }
}