namespace DSAA.Graph.SpanningTree.Fluent
{
    public interface ISelectSpanningTreeStrategy<T>
    {
        IBuildSpanningTreeStrategy<T> UsePrims();
        IBuildSpanningTreeStrategy<T> UseKruskals();
    }
}