using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Heap;

namespace DSAA.Graph.ShortestPath.Strategy
{
    public sealed class DijkstraStrategy<T> : DistanceTableBase<T>, IDistanceTableStrategy<T>
    {
        public IDictionary<T, DistanceInfo<T>> BuildDistanceTable(IGraph<T> graph, T source)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var distances = InitializeDistanceTable(graph, source);
            var verticesToProcess = new MinBinaryHeap<DistanceInfo<T>>(DistanceInfo<T>.DistanceAndEdgeComparer);
            var processedVertices = new HashSet<T>(graph.Comparer);

            verticesToProcess.Push(DistanceInfo<T>.Zero(source));

            while (verticesToProcess.Count > 0)
            {
                var sourceVertex = verticesToProcess.Pop().Single().PreviousVertex;

                processedVertices.Add(sourceVertex);

                foreach (var destinationVertex in graph.GetAdjacentVertices(sourceVertex))
                {
                    if (processedVertices.Contains(destinationVertex)) 
                        continue;

                    var step = StepMetadata<T>.ForPath(sourceVertex, destinationVertex, distances);

                    if (step.FoundShorterDistance()
                        || step.SameDistanceButLessEdges())
                    {
                        distances[destinationVertex] = step.ToSource();
                        verticesToProcess.Push(step.ToDestination());
                    }
                }
            }

            return distances;
        }
    }
}