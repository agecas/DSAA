using System;
using System.Collections.Generic;

namespace DSAA.Tree.Avl
{
    public sealed class ImmutableAvlTree<TKey, TValue> : BinaryTree<TKey, TValue, Node<TKey, TValue>>
    {
        public ImmutableAvlTree(IComparer<TKey> comparer) : base(comparer, Node.Leaf)
        {
        }

        public override ITree<TKey, TValue, Node<TKey, TValue>> Add(TKey key, TValue value)
        {
            return new ImmutableAvlTree<TKey, TValue>(Comparer)
            {
                Root = Insert(key, value, Root, Balance)
            };
        }

        public override ITree<TKey, TValue, Node<TKey, TValue>> Delete(TKey key)
        {
            if (IsEmpty)
                return this;

            return new ImmutableAvlTree<TKey, TValue>(Comparer)
            {
                Root = Delete(key, Root, Balance)
            };
        }

        #region Balancing Algorithm

        private int LeftHeight(Node<TKey, TValue> node) => CalculateHeight(node.Left);

        private int RightHeight(Node<TKey, TValue> node) => CalculateHeight(node.Right);

        private int CalculateHeight(Node<TKey, TValue> node)
        {
            if (node == null)
                return 0;

            return 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
        }

        private int BalanceFactor(Node<TKey, TValue> node) => RightHeight(node) - LeftHeight(node);

        private Node<TKey, TValue> Balance(Node<TKey, TValue> node)
        {
            if (node == null)
                return null;

            var state = GetBalanceState(node);

            if (state == TreeState.RightHeavy)
            {
                if (node.Right != null && BalanceFactor(node.Right) < 0)
                {
                    var right = RotateRight(node.Right);
                    node = node.WithRight(right);
                    node = RotateLeft(node);
                }
                else
                {
                    node = RotateLeft(node);
                }
            }

            if (state == TreeState.LeftHeavy)
            {
                if (node.Left != null && BalanceFactor(node.Left) > 0)
                {
                    var left = RotateLeft(node.Left);
                    node = node.WithLeft(left);
                    node = RotateRight(node);
                }
                else
                {
                    node = RotateRight(node);
                }
            }

            return node;
        }

        private TreeState GetBalanceState(Node<TKey, TValue> node)
        {
            if (LeftHeight(node) - RightHeight(node) > 1)
                return TreeState.LeftHeavy;

            if (RightHeight(node) - LeftHeight(node) > 1)
                return TreeState.RightHeavy;

            return TreeState.Balanced;
        }

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> node)
        {
            var left = node.Left.Left;
            var right = node.WithLeft(node.Left.Right).WithRight(node.Right);
            return node.Left.WithLeft(left).WithRight(right);
        }

        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> node)
        {
            var left = node.WithLeft(node.Left).WithRight(node.Right.Left);
            var right = node.Right.Right;
            return node.Right.WithLeft(left).WithRight(right);
        }

        private enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy
        }

        #endregion
    }
}
