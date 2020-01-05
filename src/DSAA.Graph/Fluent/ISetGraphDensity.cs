namespace DSAA.Graph.Fluent
{
    public interface ISetGraphDensity<T>
    {
        ISetComparer<T> WellConnected();
        ISetComparer<T> Sparse();
    }
}