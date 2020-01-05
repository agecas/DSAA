using Xunit;

namespace DSAA.Tree.UnitTests.Helpers
{
    internal static class NodeExtensions
    {
        public static void AssertEqual<TKey, TValue, TNode>(this TNode node, TKey expectedKey,
            params TValue[] expectedValues) where TNode : INode<TKey, TValue, TNode>
        {
            Assert.Equal(expectedKey, node.Key);
            Assert.Equal(expectedValues, node.Values);
        }  
   
        public static void AssertRootEqual<TKey, TValue, TNode>(this ITree<TKey, TValue, TNode> tree, TKey expectedKey,
            params TValue[] expectedValues) where TNode : class, INode<TKey, TValue, TNode>
        {
            Assert.NotNull(tree.Root);
            tree.Root.AssertEqual(expectedKey, expectedValues);
        }  
        
        public static void AssertLeafEqual<TKey, TValue, TNode>(this TNode node, TKey expectedKey,
            params TValue[] expectedValues) where TNode : INode<TKey, TValue, TNode>
        {
            node.AssertEqual(expectedKey, expectedValues);
            Assert.Null(node.Left);
            Assert.Null(node.Right);
        }

        public static void AssertBranchEqual<TKey, TValue, TNode>(this TNode node, TKey expectedKey,
            TValue expectedValue) where TNode : INode<TKey, TValue, TNode>
        {
            node.AssertEqual(expectedKey, expectedValue);
            Assert.True(node.Left != null || node.Right != null);
        }
    }
}