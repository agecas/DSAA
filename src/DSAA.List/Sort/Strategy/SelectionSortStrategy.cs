using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.List.Sort.Strategy
{
    public sealed class SelectionSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public SelectionSortStrategy(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            for (var currentIndex = 0; currentIndex < collection.Count - 1; currentIndex++)
            {
                var minIndex = FindIndexForSmallestValue(currentIndex, collection);
                if (minIndex != currentIndex)
                    collection = collection.Swap<T, IList<T>>(currentIndex, minIndex);
            }

            return collection;
        }

        private int FindIndexForSmallestValue(int currentIndex, IList<T> collection)
        {
            var minIndex = currentIndex;

            for (var index = currentIndex + 1; index < collection.Count; index++)
                if (_comparer.Compare(collection[index], collection[minIndex]) < 0)
                    minIndex = index;

            return minIndex;
        }
    }
}