using System;
using DSAA.List.Search.Strategy;

namespace DSAA.List.Search
{
    public sealed class SearchOptions<T>
    {
        public SearchOptions(ISearchStrategy<T> strategy)
        {
            Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public ISearchStrategy<T> Strategy { get; }
        
        public static SearchOptions<T> Default() => new SearchOptions<T>(new LinearSearch<T>());
    }
}