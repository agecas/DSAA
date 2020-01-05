using System;
using System.Collections.Generic;
using DSAA.Tree.Traverse.Fluent;

namespace DSAA.Tree
{
    public static class TreeExtensions
    {
        public static IEnumerable<TValue> Traverse<TKey, TValue, TNode>(this ITree<TKey, TValue, TNode> tree,
            Func<ISelectTraverseStrategy<TValue>, IBuildTraverseStrategy<TValue>> traversalOptions)
            where TNode : INode<TKey, TValue, TNode>
        {
            if (traversalOptions == null) throw new ArgumentNullException(nameof(traversalOptions));

            var builder = new TraverseStrategyBuilder<TKey, TValue, TNode>(tree);
            return traversalOptions(builder).Build();
        }
    }
}