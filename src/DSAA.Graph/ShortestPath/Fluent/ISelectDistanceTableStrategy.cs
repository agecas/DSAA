namespace DSAA.Graph.ShortestPath.Fluent
{
    public interface ISelectDistanceTableStrategy<T>
    {
        IBuildPathStrategy<T> UseUnweightedGraph();
        IBuildPathStrategy<T> UseDijkstra();
        IBuildPathStrategy<T> UseBellmanFord();
    }
}