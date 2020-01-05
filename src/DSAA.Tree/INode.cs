using System.Collections.Generic;

namespace DSAA.Tree
{
    public interface INode<out TKey, out TValue, out TNode> : IEnumerable<TValue> where TNode : INode<TKey, TValue, TNode>
    {
        TKey Key { get; }
        IReadOnlyList<TValue> Values { get; }
        TNode Left { get; }
        TNode Right { get; }
        bool Leaf { get; }
    }
}