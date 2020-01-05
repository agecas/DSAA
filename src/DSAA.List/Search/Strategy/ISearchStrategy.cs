using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public interface ISearchStrategy<T>
    {
        IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind);
    }
}