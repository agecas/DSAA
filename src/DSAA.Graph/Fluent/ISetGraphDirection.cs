namespace DSAA.Graph.Fluent
{
    public interface ISetGraphDirection<T>
    {
        ISetGraphDensity<T> Directed();
        ISetGraphDensity<T> Undirected();
    }
}