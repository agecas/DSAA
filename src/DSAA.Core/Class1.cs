using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAA.Core
{
    public class Class1
    {
        public void DoWork()
        {
            var data = new[] {2, 5, 9, 1, 3, 7};
            var data1 = new List<int>(data);
            var data2 = data1 as IList<int>;
            var x = data2[0];

            data1.Find(x1 => x1 > 1, o => o.UseLinearSearch());
            data1.Find(x1 => x1 > 1);
        }
            
    }

    // Linear Search
    public static class ListExtensions
    {
        public static int FindIndex<T>(this IList<T> collection, Predicate<T> predicate, Func<SearchOptions<T>, SearchOptions<T>> options)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            var strategy = options(SearchOptions<T>.Default());

            return strategy.Strategy.FindIndex(collection, predicate);
        }

        public static IReadOnlyList<int> FindAllIndexes<T>(this IList<T> collection, Predicate<T> predicate, Action<SearchOptions<T>> options)
        {


            return new List<int>();
        }

        public static T Find<T>(this IList<T> collection, Predicate<T> predicate, Action<SearchOptions<T>> options)
        {


            return default(T);
        }

        public static IReadOnlyList<T> FindAll<T>(this IList<T> collection, Predicate<T> predicate, Action<SearchOptions<T>> options)
        {


            return new List<T>();
        }
    }

    public sealed class SearchOptions<T>
    {
        private SearchOptions(ISearchStrategy<T> strategy)
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public ISearchStrategy<T> Strategy { get; }

        public SearchOptions<T> Use(ISearchStrategy<T> strategy)
        {
            return new SearchOptions<T>(strategy);
        }

        public static SearchOptions<T> Default() => new SearchOptions<T>(new LinearSearch<T>());

    }

    public interface ISearchStrategy<T>
    {
        int FindIndex(IList<T> collection, Predicate<T> predicate);
    }

    public sealed class LinearSearch<T> : ISearchStrategy<T>
    {
        public int FindIndex(IList<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < collection.Count; i++)
            {
                if (predicate(collection[i]))
                {
                    return i;
                }   
            }

            return -1;
        }
    }

    public static class SearchOptionsExtensions
    {
        public static SearchOptions<T> UseLinearSearch<T>(this SearchOptions<T> options)
        {
            return SearchOptions<T>.Default();
        }
    }
}
