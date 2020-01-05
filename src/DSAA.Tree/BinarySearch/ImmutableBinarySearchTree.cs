using System.Collections.Generic;

namespace DSAA.Tree.BinarySearch
{
    public sealed class ImmutableBinarySearchTree<TKey, TValue> : BinaryTree<TKey, TValue, Node<TKey, TValue>>
    {
        public ImmutableBinarySearchTree(IComparer<TKey> comparer) : base(comparer, Node.Leaf)
        {
        }

        public override ITree<TKey, TValue, Node<TKey, TValue>> Add(TKey key, TValue value)
        {
            return new ImmutableBinarySearchTree<TKey, TValue>(Comparer)
            {
                Root = Insert(key, value, Root)
            };
        }

        public override ITree<TKey, TValue, Node<TKey, TValue>> Delete(TKey key)
        {
            if (IsEmpty)
                return this;

            return new ImmutableBinarySearchTree<TKey, TValue>(Comparer)
            {
                Root = Delete(key, Root)
            };
        }
    }
}