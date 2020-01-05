using System.Collections.Generic;
using DSAA.Graph.ShortestPath.Fluent;

namespace DSAA.Graph.ShortestPath.Strategy
{
    public interface IDistanceTableStrategy<T>
    {
        IDictionary<T, DistanceInfo<T>> BuildDistanceTable(IGraph<T> graph, T source);
    }
}