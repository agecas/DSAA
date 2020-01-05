using System;
using DSAA.Graph.Sort.Strategy;

namespace DSAA.Graph.Sort
{
    public sealed class SortOptions<T>
    {
        public SortOptions(ISortStrategy<T> strategy)
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public ISortStrategy<T> Strategy { get; }

        public static SortOptions<T> Default()
        {
            return new SortOptions<T>(new KahnTopologicalSort<T>());
        }
    }
}