using System;
using System.Collections.Generic;
using DSAA.List.Sort.Strategy;

namespace DSAA.List.Sort
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
            return new SortOptions<T>(new BubbleSortStrategy<T>(Comparer<T>.Default));
        }
    }
}