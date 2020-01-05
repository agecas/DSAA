using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;
using DSAA.Heap;
using DSAA.Shared;

namespace DSAA.Graph.SpanningTree.Strategy
{
    public sealed class KruskalsSpanningTreeStrategy<T> : ISpanningTreeStrategy<T>
    {
        public Optional<IEnumerable<WeightedEdge<T>>> FindSpanningTreeEdges(IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (graph.Count == 0)
                return Optional<IEnumerable<WeightedEdge<T>>>.None();

            var edgesToProcess = CreatePriorityQueueAndEnqueueUniqueEdges(graph);
            var processedVertices = new HashSet<T>(graph.Comparer);
            var edgeLookup = CreateEdgeLookupByVertex(graph);
            var spanningTree = new List<WeightedEdge<T>>();

            while (edgesToProcess.Count > 0 && spanningTree.Count < graph.Count - 1)
            {
                var edge = edgesToProcess.Pop().Single();

                if (HasCycle(edgeLookup, edge))
                    continue;

                edgeLookup[edge.Source].Add(edge.Destination);
                spanningTree.Add(edge);
                processedVertices = MarkEdgeAsProcessed(edge, processedVertices);
            }

            if (processedVertices.Count != graph.Count)
                throw GraphException<T>.WhenNoMinimumSpanningTree(graph);

            return spanningTree;
        }

        private static Dictionary<T, HashSet<T>> CreateEdgeLookupByVertex(IGraph<T> graph)
        {
            var edgeMap = new Dictionary<T, HashSet<T>>(graph.Comparer);

            foreach (var vertex in graph)
                edgeMap.Add(vertex, new HashSet<T>(graph.Comparer));

            return edgeMap;
        }

        private static MinBinaryHeap<WeightedEdge<T>> CreatePriorityQueueAndEnqueueUniqueEdges(IGraph<T> graph)
        {
            var edgesToProcess = new MinBinaryHeap<WeightedEdge<T>>(new LambdaComparer<WeightedEdge<T>>(
                (left, right) => left.Weight.CompareTo(right.Weight)));

            var edgeSet = new HashSet<WeightedEdge<T>>(WeightedEdge<T>.NonDirectionalComparer(graph.Comparer));

            foreach (var vertex in graph)
            foreach (var neighbor in graph.GetAdjacentVertices(vertex))
            {
                var edge = new WeightedEdge<T>(vertex, neighbor.Value, neighbor.Weight);
                if (edgeSet.Contains(edge) == false)
                {
                    edgeSet.Add(edge);
                    edgesToProcess.Push(edge);
                }
            }

            return edgesToProcess;
        }

        private static bool HasCycle(Dictionary<T, HashSet<T>> edgeLookup, WeightedEdge<T> candidateEdge)
        {
            foreach (var sourceVertex in edgeLookup.Keys)
            {
                var queue = new Queue<T>(new[] {sourceVertex});
                var processedVertices = new HashSet<T>(edgeLookup.Comparer);

                while (queue.Count > 0)
                {
                    var currentVertex = queue.Dequeue();

                    if (processedVertices.Contains(currentVertex)) return true;

                    processedVertices.Add(currentVertex);

                    foreach (var neighbor in edgeLookup[currentVertex]) queue.Enqueue(neighbor);

                    if (edgeLookup.Comparer.Equals(currentVertex, candidateEdge.Source))
                        queue.Enqueue(candidateEdge.Destination);
                }
            }

            return false;
        }

        private static HashSet<T> MarkEdgeAsProcessed(WeightedEdge<T> edge, HashSet<T> processedVertices)
        {
            processedVertices.Add(edge.Source);
            processedVertices.Add(edge.Destination);

            return processedVertices;
        }
    }
}