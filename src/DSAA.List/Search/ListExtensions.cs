using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAA.List.Search
{
    internal static class ListExtensions
    {
        internal static bool GivenRangeIsNotValid<T>(this IList<T> collection, int startAt, int endAt)
        {
            return startAt < 0 || collection.Count <= endAt || endAt < startAt;
        }

        internal static bool GivenValueIsOutOfRange<T>(this IList<T> collection, T valueToFind,
            IComparer<T> comparer, int startAt, int endAt)
        {
            return comparer.Compare(valueToFind, collection[startAt]) < 0 || comparer.Compare(valueToFind, collection[endAt]) > 0;
        }

        internal static IReadOnlyList<int> GetIndexesForMatchingValue<T>(this IList<T> collection, T valueToFind,
            IComparer<T> comparer, int matchingIndex)
        {
            var results = new List<int>();

            results.AddRange(GetIndexesForMatchingValue(collection, valueToFind, comparer, matchingIndex - 1,
                index => --index));

            results.Add(matchingIndex);

            results.AddRange(GetIndexesForMatchingValue(collection, valueToFind, comparer, matchingIndex + 1,
                index => ++index));

            return results;
        }

        internal static IEnumerable<int> GetIndexesForMatchingValue<T>(this IList<T> collection, T valueToFind,
            IComparer<T> comparer, int startAt,
            Func<int, int> indexStepper)
        {
            var results = new List<int>();
            var index = startAt;

            while (index >= 0 && index < collection.Count)
            {
                var candidateValue = collection[index];
                if (comparer.Compare(valueToFind, candidateValue) == 0)
                    results.Add(index);
                else
                    break;

                index = indexStepper(index);
            }

            return results.OrderBy(i => i);
        }
    }
}