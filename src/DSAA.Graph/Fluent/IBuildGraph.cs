namespace DSAA.Graph.Fluent
{
    public interface IBuildGraph<T>
    {
        IGraph<T> Build();
    }
}