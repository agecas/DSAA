namespace DSAA.Tree.Traverse.Fluent
{
    public interface ISelectDepthStrategyOrder<out TValue>
    {
        IBuildTraverseStrategy<TValue> PreOrder();
        IBuildTraverseStrategy<TValue> InOrder();
        IBuildTraverseStrategy<TValue> PostOrder();
    }
}