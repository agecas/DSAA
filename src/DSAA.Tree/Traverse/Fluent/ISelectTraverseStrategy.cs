namespace DSAA.Tree.Traverse.Fluent
{
    public interface ISelectTraverseStrategy<out TValue>
    {
        IBuildTraverseStrategy<TValue> BreadthFirst();
        ISelectDepthStrategyOrder<TValue> DepthFirst();
    }
}