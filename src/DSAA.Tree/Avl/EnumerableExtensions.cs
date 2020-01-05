using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Tree.Avl
{
    public static class EnumerableExtensions
    {
        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableAvlTree<TKey, TValue, TComparer>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
            where TComparer : IComparer<TKey>, new()
        {
            return source.ToImmutableAvlTree(new TComparer());
        }

        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableAvlTree<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
            where TKey : IComparable<TKey>
        {
            return source.ToImmutableAvlTree((x, y) => x.CompareTo(y));
        }

        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableAvlTree<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source,
            Func<TKey, TKey, int> comparer)
        {
            return source.ToImmutableAvlTree(new LambdaComparer<TKey>(comparer));
        }

        public static ITree<TKey, TValue, Node<TKey, TValue>> ToImmutableAvlTree<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source,
            IComparer<TKey> comparer)
        {
            ITree<TKey, TValue, Node<TKey, TValue>> tree = new ImmutableAvlTree<TKey, TValue>(comparer);

            foreach (var pair in source) tree = tree.Add(pair.Key, pair.Value);

            return tree;
        }
    }
}