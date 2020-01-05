using System.Collections.Generic;

namespace DSAA.Tree
{
    public interface INodeBuilder<TKey, TValue, TNode> 
        where TNode : INode<TKey, TValue, TNode>, 
            INodeBuilder<TKey, TValue, TNode>
    {
        IComparer<TKey> Comparer { get; }
        TNode WithLeft(TNode left);
        TNode WithRight(TNode right);
        TNode WithValue(TValue value);
        TNode NoLeft();
        TNode WithNoRight();

        bool KeyEqualsTo(TKey otherKey);
        bool KeyIsLessOrEqualTo(TKey otherKey);
        bool KeyIsLessThan(TKey otherKey);
        bool KeyIsGreaterOrEqualTo(TKey otherKey);
        bool KeyIsGreaterThan(TKey otherKey);
    }
}