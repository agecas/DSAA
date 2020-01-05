namespace DSAA.Graph.UnitTests.Helpers
{
    public static class GraphExtensions
    {
        internal static IGraph<T> AddToGraph<T>(this IGraph<T> graph, EdgeDescriptor<T>[] edges)
        {
            foreach (var edge in edges)
            {
                if (edge.Vertices.Length == 0)
                    graph.AddVertex(edge.Edge);
                else
                    foreach (var vertex in edge.Vertices)
                    {
                        graph.AddEdge(edge.Edge, vertex.Value, vertex.Weight);
                    }
            }

            return graph;
        }
    }
}