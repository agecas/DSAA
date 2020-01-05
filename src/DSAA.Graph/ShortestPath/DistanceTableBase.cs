using System.Collections.Generic;
using DSAA.Graph.ShortestPath.Fluent;

namespace DSAA.Graph.ShortestPath
{
    public abstract class DistanceTableBase<T>
    {
        protected Dictionary<T, DistanceInfo<T>> InitializeDistanceTable(IGraph<T> graph, T source)
        {
            var distanceTable = new Dictionary<T, DistanceInfo<T>>(graph.Comparer);

            foreach (var vertex in graph)
                distanceTable.Add(vertex, new DistanceInfo<T>());

            distanceTable[source] = DistanceInfo<T>.Zero(source);

            return distanceTable;
        }
    }
}