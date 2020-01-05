using System;
using System.Collections.Generic;

namespace DSAA.Tree.Traverse.Fluent
{
    public sealed class TraverseStrategyBuilder<TKey, TValue, TNode> : ISelectTraverseStrategy<TValue>,
        ISelectDepthStrategyOrder<TValue>, IBuildTraverseStrategy<TValue>
        where TNode : INode<TKey, TValue, TNode>
    {
        private readonly ITree<TKey, TValue, TNode> _tree;
        private IEnumerable<TValue> _strategy;

        public TraverseStrategyBuilder(ITree<TKey, TValue, TNode> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        public IEnumerable<TValue> Build()
        {
            return _strategy;
        }

        public IBuildTraverseStrategy<TValue> PreOrder()
        {
            _strategy = new DepthFirstPreOrderTraversal<TKey, TValue, TNode>(_tree);
            return this;
        }

        public IBuildTraverseStrategy<TValue> InOrder()
        {
            _strategy = new DepthFirstInOrderTraversal<TKey, TValue, TNode>(_tree);
            return this;
        }

        public IBuildTraverseStrategy<TValue> PostOrder()
        {
            _strategy = new DepthFirstPostOrderTraversal<TKey, TValue, TNode>(_tree);
            return this;
        }

        public IBuildTraverseStrategy<TValue> BreadthFirst()
        {
            _strategy = new BreadthTraversal<TKey, TValue, TNode>(_tree);
            return this;
        }

        public ISelectDepthStrategyOrder<TValue> DepthFirst()
        {
            return this;
        }
    }
}