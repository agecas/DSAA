using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Shared;

namespace DSAA.List.Search
{
    public static class ListSearchExtensions
    {
        public static int FindIndex<T>(this IList<T> collection, T valueToFind,
            Func<SearchOptions<T>, SearchOptions<T>> optionsFactory)
        {
            var indexes = collection.FindAllIndexes(valueToFind, optionsFactory);
            return indexes.Any() ? indexes.First() : -1;
        }

        public static IReadOnlyList<int> FindAllIndexes<T>(this IList<T> collection, T valueToFind,
            Func<SearchOptions<T>, SearchOptions<T>> optionsFactory)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var options = optionsFactory(SearchOptions<T>.Default());
            return options.Strategy.FindAllIndexes(collection, valueToFind).ToList();
        }

        public static Optional<T> Find<T>(this IList<T> collection, T valueToFind,
            Func<SearchOptions<T>, SearchOptions<T>> optionsFactory)
        {
            var index = collection.FindIndex(valueToFind, optionsFactory);
            return index > -1 ? Optional<T>.Some(collection[index]) : Optional<T>.None();
        }

        public static IReadOnlyList<T> FindAll<T>(this IList<T> collection, T valueToFind,
            Func<SearchOptions<T>, SearchOptions<T>> optionsFactory)
        {
            var indexes = collection.FindAllIndexes(valueToFind, optionsFactory);
            var values = indexes.Select(i => collection[i]).ToList();
            return values;
        }
    }
}