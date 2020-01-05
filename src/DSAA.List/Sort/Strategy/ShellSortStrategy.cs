using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.List.Sort.Strategy
{
    public sealed class ShellSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly Func<IList<T>, int> _incrementFactory;

        public ShellSortStrategy(IComparer<T> comparer, Func<IList<T>, int> incrementFactory)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _incrementFactory = incrementFactory ?? throw new ArgumentNullException(nameof(incrementFactory));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var increment = CalculateIncrement(collection);

            while (increment >= 1)
            {
                for (var startIndex = 0; startIndex < increment; startIndex++)
                    collection = InsertionSort(collection, startIndex, increment);

                increment = increment / 2;
            }

            return collection;
        }

        private int CalculateIncrement(IList<T> collection)
        {
            var increment = _incrementFactory(collection);

            if (increment < 1 || increment > collection.Count)
                increment = 1;

            return increment;
        }

        private IList<T> InsertionSort(IList<T> collection, int startIndex, int increment)
        {
            for (var i = startIndex; i < collection.Count; i += increment)
            for (var j = Math.Min(i + increment, collection.Count - 1); j - increment >= 0; j -= increment)
                if (_comparer.Compare(collection[j], collection[j - increment]) < 0)
                    collection = collection.Swap<T, IList<T>>(j, j - increment);
                else
                    break;

            return collection;
        }
    }
}