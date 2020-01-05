using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class FibonacciSearch<T> : ISearchStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public FibonacciSearch(IComparer<T> comparer)
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
            
            var sequence = GetFibonacciSequence(collection);
            var startIndex = -1;

            while (sequence.N > 1)
            {
                var index = Math.Min(startIndex + sequence.N2, collection.Count - 1);

                if (_comparer.Compare(valueToFind, collection[index]) < 0)
                {
                    sequence = sequence.Previous().Previous();
                }
                else if (_comparer.Compare(valueToFind, collection[index]) > 0)
                {
                    sequence = sequence.Previous();
                    startIndex = index;
                }
                else
                {
                    return collection.GetIndexesForMatchingValue(valueToFind, _comparer, index);
                }
            }

            if (LastElementMatch(collection, valueToFind, sequence, startIndex))
                return collection.GetIndexesForMatchingValue(valueToFind, _comparer, startIndex + 1);

            return new List<int>();
        }

        private bool LastElementMatch(IList<T> collection, T valueToFind, FibonacciSequence sequence, int startIndex)
        {
            return sequence.N1 == 1 && startIndex + 1 < collection.Count &&
                   _comparer.Compare(valueToFind, collection[startIndex + 1]) == 0;
        }

        private FibonacciSequence GetFibonacciSequence(IList<T> collection)
        {
            var sequence = FibonacciSequence.First();
            while (sequence.N < collection.Count) sequence = sequence.Next();
            return sequence;
        }
    }
}