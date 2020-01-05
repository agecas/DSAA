using System;
using DSAA.Shared;

namespace DSAA.Graph.Domain
{
    public sealed class GraphException<T> : DssaException
    {
        private GraphException(IGraph<T> graph, string message) : base(message)
        {
            Graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        public IGraph<T> Graph { get; }

        public static GraphException<T> WhenNoMinimumSpanningTree(IGraph<T> graph)
        {
            return new GraphException<T>(graph, "No minimum spanning tree found for given graph.");
        }

        public static GraphException<T> WhenNoShortestPathNotSupported(IGraph<T> graph, Metadata metadata)
        {
            var message = $"None of the available algorithms supports finding Shortest Path for graph with metadata: {metadata}";
            return new GraphException<T>(graph, message);
        }

        public static Exception WhenSpanningTreeNotSupported(IGraph<T> graph, Metadata metadata)
        {
            var message = $"None of the available algorithms supports finding Spanning Tree for graph with metadata: {metadata}";
            return new GraphException<T>(graph, message);
        }
    }
}