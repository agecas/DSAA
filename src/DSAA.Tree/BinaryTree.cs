using System;
using System.Collections.Generic;

namespace DSAA.Tree
{
    public abstract class BinaryTree<TKey, TValue, TNode> : ITree<TKey, TValue, TNode> where TNode : class,
        INode<TKey, TValue, TNode>,
        INodeBuilder<TKey, TValue, TNode>
    {
        protected IComparer<TKey> Comparer { get; }
        private readonly Func<TKey, TValue, IComparer<TKey>, TNode> _nodeFactory;

        protected BinaryTree(IComparer<TKey> comparer, Func<TKey, TValue, IComparer<TKey>, TNode> nodeFactory)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _nodeFactory = nodeFactory ?? throw new ArgumentNullException(nameof(nodeFactory));
        }

        public TNode Root { get; protected set; }
        public bool IsEmpty => Root == null;

        public abstract ITree<TKey, TValue, TNode> Add(TKey key, TValue value);

        public abstract ITree<TKey, TValue, TNode> Delete(TKey key);

        public IReadOnlyList<TValue> Find(TKey key)
        {
            var nodeToCheck = Root;

            while (nodeToCheck != null)
            {
                if (nodeToCheck.KeyEqualsTo(key))
                    return nodeToCheck.Values;

                nodeToCheck = nodeToCheck.KeyIsGreaterThan(key) ? nodeToCheck.Left : nodeToCheck.Right;
            }

            return new List<TValue>();
        }

        protected TNode Insert(TKey key, TValue value, TNode node, Func<TNode, TNode> afterInsert = null)
        {
            if (node == null)
                return _nodeFactory(key, value, Comparer);

            if (node.KeyEqualsTo(key))
                return node.WithValue(value);

            var right = node.Right;
            var left = node.Left;

            if (node.KeyIsLessThan(key))
                right = Insert(key, value, right, afterInsert);
            else if (node.KeyIsGreaterThan(key)) left = Insert(key, value, left, afterInsert);

            var affectedNode = node.WithLeft(left).WithRight(right);

            return afterInsert == null ? affectedNode : afterInsert(affectedNode);
        }

        protected TNode Delete(TKey key, TNode node, Func<TNode, TNode> afterDelete = null)
        {
            var nodeToProcess = node;

            if (nodeToProcess.KeyIsLessThan(key) && nodeToProcess.Right != null)
            {
                nodeToProcess = nodeToProcess.WithRight(Delete(key, nodeToProcess.Right, afterDelete));
            }
            else if (nodeToProcess.KeyIsGreaterThan(key) && nodeToProcess.Left != null)
            {
                nodeToProcess = nodeToProcess.WithLeft(Delete(key, nodeToProcess.Left, afterDelete));
            }
            else if (nodeToProcess.KeyEqualsTo(key))
            {
                if (nodeToProcess.Leaf)
                {
                    nodeToProcess = null;
                }
                else if (nodeToProcess.Right == null)
                {
                    nodeToProcess = nodeToProcess.Left;
                }
                else if (nodeToProcess.Right.Left == null)
                {
                    var left = nodeToProcess.Left;
                    nodeToProcess = nodeToProcess.Right.WithLeft(left);
                }
                else
                {
                    var (leftMost, right) = GetLeftMostNodeAndAdjustedRight(nodeToProcess.Right);

                    nodeToProcess = leftMost
                        .WithLeft(nodeToProcess.Left)
                        .WithRight(right);
                }
            }

            return afterDelete == null ? nodeToProcess : afterDelete(nodeToProcess);
        }

        private (TNode Left, TNode Right) GetLeftMostNodeAndAdjustedRight(TNode node)
        {
            var parents = new Stack<TNode>(new[] {node});

            while (parents.Peek().Left != null) parents.Push(parents.Peek().Left);

            var leftMost = parents.Pop();
            var right = parents.Count > 0 ? parents.Pop().WithLeft(leftMost.Right) : null;

            while (parents.Count > 0)
            {
                var parent = parents.Pop();
                right = parent.WithLeft(right);
            }

            return (leftMost, right);
        }
    }
}