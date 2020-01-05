using System;
using System.Collections.Generic;
using DSAA.Graph.ShortestPath.Fluent;

namespace DSAA.Graph.ShortestPath.Strategy
{
    public sealed class BellmanFordsStrategy<T> : DistanceTableBase<T>, IDistanceTableStrategy<T>
    {
        public IDictionary<T, DistanceInfo<T>> BuildDistanceTable(IGraph<T> graph, T source)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var distances = InitializeDistanceTable(graph, source);

            for (var iteration = 0; iteration < graph.Count; iteration++)
                foreach (var sourceVertex in graph)
                foreach (var destinationVertex in graph.GetAdjacentVertices(sourceVertex))
                {
                    var step = StepMetadata<T>.ForPath(sourceVertex, destinationVertex, distances);

                    if (step.FoundShorterDistance()
                        || step.SameDistanceButLessEdges())
                    {
                        if (CheckingForCycles(graph, iteration))
                            return InitializeDistanceTable(graph, source);

                        distances[destinationVertex] = step.ToSource();
                    }
                }

            return distances;
        }

        private static bool CheckingForCycles(IGraph<T> graph, int iteration)
        {
            return iteration == graph.Count - 1;
        }
    }
}