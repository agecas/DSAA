using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class ExponentialSearch<T> : ISearchStrategy<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly BinarySearch<T> _binarySearch;

        public ExponentialSearch(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _binarySearch = new BinarySearch<T>(comparer);
        }

        public IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            return FindAllIndexes(collection, valueToFind, 0, collection.Count - 1);
        }

        private IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind, int startAt, int endAt)
        {
            if (collection.GivenRangeIsNotValid(startAt, endAt))
                return new List<int>();

            if (collection.GivenValueIsOutOfRange(valueToFind, _comparer, startAt, endAt))
                return new List<int>();

            var index = 0;
            while (index < collection.Count && _comparer.Compare(collection[index], valueToFind) < 0)
            {
                if (index == 0)
                    index++;
                else
                    index *= 2;
            }

            if (index >= collection.Count)
            {
                index = collection.Count - 1;
            }

            if (_comparer.Compare(collection[index], valueToFind) == 0)
            {
                return collection.GetIndexesForMatchingValue(valueToFind, _comparer, index);
            }

            return _binarySearch.FindAllIndexes(collection, valueToFind, index / 2 + 1, index - 1);
        }
    }
}