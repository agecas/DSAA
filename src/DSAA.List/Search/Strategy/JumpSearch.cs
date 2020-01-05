using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class JumpSearch<T> : ISearchStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public JumpSearch(IComparer<T> comparer)
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

            var step = Convert.ToInt32(Math.Floor(Math.Sqrt(collection.Count)));
            var index = 0;

            while (_comparer.Compare(collection[index], valueToFind) < 0)
            {
                index = index + step;

                if (index >= collection.Count)
                {
                    index = collection.Count - 1;
                    break;
                }
            }

            var candidateValue = collection[index];
            if (_comparer.Compare(candidateValue, valueToFind) == 0)
            {
                return collection.GetIndexesForMatchingValue(valueToFind, _comparer, index);
            }

            index = index - step + 1;
            if (index < 0)
                return new List<int>();

            return collection.GetIndexesForMatchingValue(valueToFind, _comparer, index, i => ++i);
        }
    }
}