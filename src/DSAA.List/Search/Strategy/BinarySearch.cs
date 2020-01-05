using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class BinarySearch<T> : ISearchStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public BinarySearch(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            return FindAllIndexes(collection, valueToFind, 0, collection.Count - 1);
        }

        public IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind, int startAt, int endAt)
        {
            if (collection.GivenRangeIsNotValid(startAt, endAt))
                return new List<int>();

            if (collection.GivenValueIsOutOfRange(valueToFind, _comparer, startAt, endAt))
                return new List<int>();

            var middleIndex = (startAt + endAt) / 2;
            var candidateValue = collection[middleIndex];

            if (_comparer.Compare(candidateValue, valueToFind) == 0)
            {
                return collection.GetIndexesForMatchingValue(valueToFind, _comparer, middleIndex);
            }

            if (_comparer.Compare(valueToFind, candidateValue) < 0)
                return FindAllIndexes(collection, valueToFind, startAt, middleIndex - 1);

            if (_comparer.Compare(valueToFind, candidateValue) > 0)
                return FindAllIndexes(collection, valueToFind, middleIndex + 1, endAt);

            return new List<int>();
        }
    }
}