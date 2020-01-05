using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Graph.Sort
{
    public static class GraphSortExtensions
    {
        public static Optional<IEnumerable<T>> Sort<T>(this IGraph<T> graph,
            Func<SortOptions<T>, SortOptions<T>> optionsFactory)
        {
            var options = optionsFactory(SortOptions<T>.Default());
            return options.Strategy.Sort(graph);
        }
    }
}