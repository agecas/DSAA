using System.Collections.Generic;

namespace DSAA.Tree
{
    public interface ITree<TKey, TValue, TNode> where TNode : INode<TKey, TValue, TNode>
    {
        TNode Root { get; }
        ITree<TKey, TValue, TNode> Add(TKey key, TValue value);
        ITree<TKey, TValue, TNode> Delete(TKey key);
        IReadOnlyList<TValue> Find(TKey key);
        bool IsEmpty { get;  }
    }
}