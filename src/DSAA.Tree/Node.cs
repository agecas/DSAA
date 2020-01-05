using System;
using System.Collections;
using System.Collections.Generic;

namespace DSAA.Tree
{
    public sealed class Node<TKey, TValue> : INode<TKey, TValue, Node<TKey, TValue>>,
        INodeBuilder<TKey, TValue, Node<TKey, TValue>>
    {
        internal Node(TKey key, IReadOnlyList<TValue> values, Node<TKey, TValue> left,
            Node<TKey, TValue> right, IComparer<TKey> comparer)
        {
            Key = key;
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Left = left;
            Right = right;
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public bool Leaf => Left == null && Right == null;

        public TKey Key { get; }
        public IReadOnlyList<TValue> Values { get; }
        public Node<TKey, TValue> Left { get; }
        public Node<TKey, TValue> Right { get; }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IComparer<TKey> Comparer { get; }

        public Node<TKey, TValue> WithLeft(Node<TKey, TValue> left)
        {
            return new Node<TKey, TValue>(Key, Values, left, Right, Comparer);
        }

        public Node<TKey, TValue> WithRight(Node<TKey, TValue> right)
        {
            return new Node<TKey, TValue>(Key, Values, Left, right, Comparer);
        }

        public Node<TKey, TValue> WithValue(TValue value)
        {
            return new Node<TKey, TValue>(Key, new List<TValue>(Values) {value}, Left, Right, Comparer);
        }

        public Node<TKey, TValue> NoLeft()
        {
            return new Node<TKey, TValue>(Key, Values, null, Right, Comparer);
        }

        public Node<TKey, TValue> WithNoRight()
        {
            return new Node<TKey, TValue>(Key, Values, Left, null, Comparer);
        }

        public override string ToString()
        {
            return $"Key: {Key} : {string.Join(", ", Values)}";
        }

        #region Key Comparisons

        public bool KeyEqualsTo(TKey otherKey) => Comparer.Compare(Key, otherKey) == 0;
        public bool KeyIsLessOrEqualTo(TKey otherKey) => Comparer.Compare(Key, otherKey) <= 0;
        public bool KeyIsLessThan(TKey otherKey) => Comparer.Compare(Key, otherKey) < 0;
        public bool KeyIsGreaterOrEqualTo(TKey otherKey) => Comparer.Compare(Key, otherKey) >= 0;
        public bool KeyIsGreaterThan(TKey otherKey) => Comparer.Compare(Key, otherKey) > 0;

        #endregion
    }

    public static class Node
    {
        public static Node<TKey, TValue> Leaf<TKey, TValue>(TKey key, TValue value,
            IComparer<TKey> comparer)
        {
            return new Node<TKey, TValue>(key, new[] {value}, null, null, comparer);
        }
    }
}