using DSAA.Shared;

namespace DSAA.Graph.ShortestPath.Fluent
{
    public interface IBuildPathStrategy<T>
    {
        Optional<GraphPath<T>> Build(IGraph<T> graph, T source, T destination);
    }
}