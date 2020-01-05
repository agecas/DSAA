using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.ShortestPath.Fluent;

namespace DSAA.Graph.ShortestPath.Strategy
{
    public sealed class UnweightedGraphStrategy<T> : DistanceTableBase<T>, IDistanceTableStrategy<T>
    {
        public IDictionary<T, DistanceInfo<T>> BuildDistanceTable(IGraph<T> graph, T source)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var distanceTable = InitializeDistanceTable(graph, source);
            var verticesToProcess = new Queue<T>(new []{ source });
            
            while (verticesToProcess.Count > 0)
            {
                var lastVertex = verticesToProcess.Dequeue();

                foreach (var neighbor in graph.GetAdjacentVertices(lastVertex))
                {
                    var distance = distanceTable[neighbor];
                    if (distance.Empty)
                    {
                        distanceTable[neighbor] = distanceTable[lastVertex].AddStep(lastVertex);

                        if (graph.GetAdjacentVertices(neighbor).Any())
                        {
                            verticesToProcess.Enqueue(neighbor);
                        }
                    }
                }
            }

            return distanceTable;
        }
    }
}