using System;
using System.Collections;
using System.Collections.Generic;

namespace DSAA.Tree.Traverse
{
    public sealed class DepthFirstPreOrderTraversal<TKey, TValue, TNode> : IEnumerable<TValue> where TNode : INode<TKey, TValue, TNode>
    {
        private readonly ITree<TKey, TValue, TNode> _tree;

        public DepthFirstPreOrderTraversal(ITree<TKey, TValue, TNode> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (_tree.IsEmpty) yield break;

            var nodes = new Stack<TNode>(new[] {_tree.Root});

            while (nodes.Count > 0)
            {
                var current = nodes.Pop();
                foreach (var value in current.Values) yield return value;

                if (current.Right != null)
                {
                    nodes.Push(current.Right);
                }
                if (current.Left != null)
                {
                    nodes.Push(current.Left);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}