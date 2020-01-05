using System;
using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Graph.SpanningTree.Fluent;
using DSAA.Graph.Traverse.Fluent;
using DSAA.Shared;

namespace DSAA.Graph
{
    public static class GraphExtensions
    {
        public static IEnumerable<T> Traverse<T>(this IGraph<T> graph,
            Func<ISelectTraverseStrategy<T>, IBuildTraverseStrategy<T>> traversalOptions, T startingVertex)
        {
            if (traversalOptions == null) throw new ArgumentNullException(nameof(traversalOptions));

            var builder = new TraverseStrategyBuilder<T>(graph, startingVertex);
            return traversalOptions(builder).Build();
        }

        public static Optional<GraphPath<T>> FindShortestPath<T>(this IGraph<T> graph,
            T startingVertex, T destinationVertex,
            Func<ISelectDistanceTableStrategy<T>, IBuildPathStrategy<T>> traversalOptions)
        {
            if (traversalOptions == null) throw new ArgumentNullException(nameof(traversalOptions));

            var builder = new ShortestPathBuilder<T>();
            return traversalOptions(builder).Build(graph, startingVertex, destinationVertex);
        }

        public static Optional<GraphPath<T>> FindShortestPath<T>(this IGraph<T> graph,
            T startingVertex, T destinationVertex)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var metadata = new MetadataProvider().GetMetadata(graph);

            if (metadata.Unweighted && metadata.AllPositiveWeights)
                return graph.FindShortestPath(startingVertex, destinationVertex, o => o.UseUnweightedGraph());

            if (metadata.Weighted && metadata.AllPositiveWeights)
                return graph.FindShortestPath(startingVertex, destinationVertex, o => o.UseDijkstra());

            if (metadata.HasNegativeWeights && metadata.Type == GraphType.Directed)
                return graph.FindShortestPath(startingVertex, destinationVertex, o => o.UseBellmanFord());

            // if (metadata.HasNegativeWeights && metadata.Type == GraphType.Undirected)
            // Using Edmonds' Minimum Weight Perfect Matching Algorithm to solve shortest path problems for undirected graph with negative-weight edges
            // TODO: implement shortest path algorithm for Undirected graph with negative weights
            throw GraphException<T>.WhenNoShortestPathNotSupported(graph, metadata);
        }

        public static Optional<IEnumerable<WeightedEdge<T>>> FindSpanningTree<T>(this IGraph<T> graph,
            Func<ISelectSpanningTreeStrategy<T>, IBuildSpanningTreeStrategy<T>> traversalOptions)
        {
            if (traversalOptions == null) throw new ArgumentNullException(nameof(traversalOptions));

            var builder = new SpanningTreeBuilder<T>();
            return traversalOptions(builder).Build(graph);
        }

        public static Optional<IEnumerable<WeightedEdge<T>>> FindSpanningTree<T>(this IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var metadata = new MetadataProvider().GetMetadata(graph);

            if (metadata.Type == GraphType.Directed)
            {
                if (metadata.Disconnected)
                    return graph.FindSpanningTree(o => o.UseKruskals());

                return graph.FindSpanningTree(o => o.UsePrims());
            }

            // TODO: implement Spanning Tree algorithm for Undirected Connected & Disconnected (Forests) graphs
            // For directed graphs, the equivalent notion of a spanning tree is spanning arborescence. A minimum weight spanning arborescence can be found using Edmonds' algorithm. => https://brainly.in/question/3337585
            throw GraphException<T>.WhenSpanningTreeNotSupported(graph, metadata);
        }
    }
}