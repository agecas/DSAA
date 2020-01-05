using System;
using System.Collections;
using System.Collections.Generic;

namespace DSAA.Tree.Traverse
{
    public sealed class DepthFirstPostOrderTraversal<TKey, TValue, TNode> : IEnumerable<TValue> where TNode : INode<TKey, TValue, TNode>
    {
        private readonly ITree<TKey, TValue, TNode> _tree;

        public DepthFirstPostOrderTraversal(ITree<TKey, TValue, TNode> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (_tree.IsEmpty) yield break;

            var nodes = new Stack<TNode>(new[] {_tree.Root});
            TNode previous = default;

            while (nodes.Count > 0)
            {
                var current = nodes.Peek();

                if (previous == null || ReferenceEquals(previous.Left, current) ||
                    ReferenceEquals(previous.Right, current))
                {
                    if (current.Left != null)
                    {
                        nodes.Push(current.Left);
                    }
                    else if (current.Right != null)
                    {
                        nodes.Push(current.Right);
                    }
                    else
                    {
                        nodes.Pop();
                        foreach (var value in current.Values) yield return value;
                    }
                }
                else if (ReferenceEquals(current.Left, previous))
                {
                    if (current.Right != null)
                    {
                        nodes.Push(current.Right);
                    }
                    else
                    {
                        nodes.Pop();
                        foreach (var value in current.Values) yield return value;
                    }
                }
                else if (ReferenceEquals(current.Right, previous))
                {
                    nodes.Pop();
                    foreach (var value in current.Values) yield return value;
                }

                previous = current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}