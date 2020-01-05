using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Graph.ShortestPath.Strategy;
using DSAA.Heap;
using DSAA.Shared;

namespace DSAA.Graph.SpanningTree.Strategy
{
    public sealed class PrimsSpanningTreeStrategy<T> : DistanceTableBase<T>, ISpanningTreeStrategy<T>
    {
        private readonly Func<IGraph<T>, T> _sourceNodeProvider;

        public PrimsSpanningTreeStrategy(Func<IGraph<T>, T> sourceNodeProvider)
        {
            _sourceNodeProvider = sourceNodeProvider ?? throw new ArgumentNullException(nameof(sourceNodeProvider));
        }

        public Optional<IEnumerable<WeightedEdge<T>>> FindSpanningTreeEdges(IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            if (graph.Count == 0)
                return Optional<IEnumerable<WeightedEdge<T>>>.None();

            var source = _sourceNodeProvider(graph);
            var distances = InitializeDistanceTable(graph, source);
            var verticesToProcess = new MinBinaryHeap<DistanceInfo<T>>(DistanceInfo<T>.DistanceComparer);
            var processedVertices = new HashSet<T>(graph.Comparer);
            var spanningTree = new HashSet<WeightedEdge<T>>(WeightedEdge<T>.NonDirectionalComparer(graph.Comparer));

            verticesToProcess.Push(DistanceInfo<T>.Zero(source));

            while (verticesToProcess.Count > 0)
            {
                var currentVertex = verticesToProcess.Pop().Single().PreviousVertex;
                var sourceDistance = distances[currentVertex];

                processedVertices.Add(currentVertex);

                if (graph.Comparer.Equals(source, currentVertex) == false)
                {
                    var edge = new WeightedEdge<T>(sourceDistance.PreviousVertex, currentVertex, sourceDistance.Distance);
                    if (spanningTree.Contains(edge) == false)
                    {
                        spanningTree.Add(edge);
                    }
                }

                foreach (var destinationVertex in graph.GetAdjacentVertices(currentVertex))
                {
                    if (processedVertices.Contains(destinationVertex))
                        continue;

                    var step = StepMetadata<T>.ForSpanningTree(currentVertex, destinationVertex, distances);

                    if (step.FoundShorterDistance())
                    {
                        distances[destinationVertex] = step.ToSource();
                        verticesToProcess.Push(step.ToDestination());
                    }
                }
            }

            return spanningTree;
        }
    }
}