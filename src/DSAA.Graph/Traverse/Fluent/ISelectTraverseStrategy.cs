namespace DSAA.Graph.Traverse.Fluent
{
    public interface ISelectTraverseStrategy<out TValue>
    {
        IBuildTraverseStrategy<TValue> BreadthFirst();
        IBuildTraverseStrategy<TValue> DepthFirst();
    }
}