using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;

namespace DSAA.Graph
{
    public sealed class MetadataProvider
    {
        public Metadata GetMetadata<T>(IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (graph.Count == 0)
                return Metadata.Empty(graph.Type);

            var verticesCount = graph.Count;
            var (weighted, hasNegativeWeights, edgeCount) = GetEdgeMetadata(graph);
            var connected = IsGraphConnected(graph);

            return new Metadata(graph.Type, verticesCount, edgeCount, connected, weighted, hasNegativeWeights);
        }

        private static (bool Weighted, bool HasNegativeWeights, int EdgeCount) GetEdgeMetadata<T>(IGraph<T> graph)
        {
            var weighted = false;
            var hasNegativeWeights = false;
            var lastWeight = (int?) null;
            var edges = graph.Type == GraphType.Directed
                ? new HashSet<WeightedEdge<T>>(WeightedEdge<T>.DirectionalComparer(graph.Comparer))
                : new HashSet<WeightedEdge<T>>(WeightedEdge<T>.NonDirectionalComparer(graph.Comparer));


            foreach (var vertex in graph)
            foreach (var edge in graph.GetAdjacentVertices(vertex))
            {
                edges.Add(new WeightedEdge<T>(vertex, edge.Value, edge.Weight));

                if (hasNegativeWeights == false && edge.Weight < 0)
                    hasNegativeWeights = true;

                if (lastWeight.HasValue && lastWeight != edge.Weight)
                    weighted = true;
                else
                    lastWeight = edge.Weight;
            }

            return (weighted, hasNegativeWeights, edges.Count);
        }

        private bool IsGraphConnected<T>(IGraph<T> graph)
        {
            var vertices = graph.Traverse(o => o.BreadthFirst(), graph.First());
            return graph.Count == vertices.Count();
        }
    }
}