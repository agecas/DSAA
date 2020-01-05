using System;
using System.Collections;
using System.Collections.Generic;

namespace DSAA.Tree.Traverse
{
    public sealed class BreadthTraversal<TKey, TValue, TNode> : IEnumerable<TValue> where TNode : INode<TKey, TValue, TNode>
    {
        private readonly ITree<TKey, TValue, TNode> _tree;

        public BreadthTraversal(ITree<TKey, TValue, TNode> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (_tree.IsEmpty)
            {
                yield break;
            }

            var queue = new Queue<TNode>(new []
            {
                _tree.Root
            });
            
            while (queue.Count > 0) {
                var node = queue.Dequeue();

                foreach (var value in node.Values)
                {
                    yield return value;
                }

                if (node.Left != null) {
                    queue.Enqueue(node.Left);
                }
                if (node.Right != null) {
                    queue.Enqueue(node.Right);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
