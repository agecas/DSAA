using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Graph.Sort.Strategy
{
    public interface ISortStrategy<T>
    {
        Optional<IEnumerable<T>> Sort(IGraph<T> graph);
    }
}