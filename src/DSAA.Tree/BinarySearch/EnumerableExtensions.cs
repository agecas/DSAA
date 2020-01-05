using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Tree.BinarySearch
{
    public static class EnumerableExtensions
    {
        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableBinarySearchTree<TKey, TValue, TComparer>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
            where TComparer : IComparer<TKey>, new()
        {
            return source.ToImmutableBinarySearchTree(new TComparer());
        }

        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableBinarySearchTree<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
            where TKey : IComparable<TKey>
        {
            return source.ToImmutableBinarySearchTree((x, y) => x.CompareTo(y));
        }

        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableBinarySearchTree<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source,
            Func<TKey, TKey, int> comparer)
        {
            return source.ToImmutableBinarySearchTree(new LambdaComparer<TKey>(comparer));
        }

        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableBinarySearchTree<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source,
            IComparer<TKey> comparer)
        {
            ITree<TKey, TValue, Node<TKey, TValue>> tree = new ImmutableBinarySearchTree<TKey, TValue>(comparer);

            foreach (var pair in source) tree = tree.Add(pair.Key, pair.Value);

            return tree;
        }
    }
}