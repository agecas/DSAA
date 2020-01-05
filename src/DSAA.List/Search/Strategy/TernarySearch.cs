using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class TernarySearch<T> : ISearchStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public TernarySearch(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
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

            var firstThirdIndex = startAt + (endAt - startAt) * 1 / 3;
            var secondThirdIndex = startAt + (endAt - startAt) * 2 / 3;

            var firstThirdValue = collection[firstThirdIndex];
            var secondThirdValue = collection[secondThirdIndex];

            if (_comparer.Compare(firstThirdValue, valueToFind) == 0)
            {
                return collection.GetIndexesForMatchingValue(valueToFind, _comparer, firstThirdIndex);
            }

            if (_comparer.Compare(secondThirdValue, valueToFind) == 0)
            {
                return collection.GetIndexesForMatchingValue(valueToFind, _comparer, secondThirdIndex);
            }

            if (_comparer.Compare(valueToFind, firstThirdValue) < 0)
                return FindAllIndexes(collection, valueToFind, startAt, firstThirdIndex - 1);

            if (_comparer.Compare(valueToFind, firstThirdValue) > 0 && _comparer.Compare(valueToFind, secondThirdValue) < 0)
                return FindAllIndexes(collection, valueToFind, firstThirdIndex + 1, secondThirdIndex - 1);

            if (_comparer.Compare(valueToFind, secondThirdValue) > 0)
                return FindAllIndexes(collection, valueToFind, secondThirdIndex + 1, endAt);

            return new List<int>();
        }
    }
}