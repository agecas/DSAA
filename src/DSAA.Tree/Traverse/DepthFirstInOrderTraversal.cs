using System;
using System.Collections;
using System.Collections.Generic;

namespace DSAA.Tree.Traverse
{
    public sealed class DepthFirstInOrderTraversal<TKey, TValue, TNode> : IEnumerable<TValue> where TNode : INode<TKey, TValue, TNode>
    {
        private readonly ITree<TKey, TValue, TNode> _tree;

        public DepthFirstInOrderTraversal(ITree<TKey, TValue, TNode> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (_tree.IsEmpty) yield break;

            var nodes = new Stack<TNode>();
            var current = _tree.Root;

            while (nodes.Count > 0 || current != null)
                if (current != null)
                {
                    nodes.Push(current);
                    current = current.Left;
                }
                else
                {
                    var node = nodes.Pop();

                    foreach (var value in node.Values) yield return value;

                    current = node.Right;
                }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}