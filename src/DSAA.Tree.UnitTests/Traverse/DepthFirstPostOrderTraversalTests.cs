using System.Collections.Generic;
using System.Linq;
using DSAA.Tree.BinarySearch;
using Xunit;

namespace DSAA.Tree.UnitTests.Traverse
{
    public sealed class DepthFirstPostOrderTraversalTests
    {
        [Fact]
        public void Given_BinaryTree_When_HasNodes_Then_TraverseInExpectedOrder()
        {
            // Arrange
            //       4 
            //     /   \
            //    2      5
            //   / \
            //  1   3

            var tree = new List<int> { 4, 5, 2, 1, 3 }.ToDictionary(k => k, v => v).ToImmutableBinarySearchTree();

            // Act
            var result = tree.Traverse(o => o.DepthFirst().PostOrder());

            // Assert
            Assert.Equal(new List<int> { 1, 3, 2, 5, 4 }, result);
        }   
        
        [Fact]
        public void Given_BinaryTree_When_HasNoNodes_Then_ReturnEmpty()
        {
            // Arrange
            var tree = new Dictionary<int, int>().ToImmutableBinarySearchTree();

            // Act
            var result = tree.Traverse(o => o.DepthFirst().PostOrder());

            // Assert
            Assert.Empty(result);
        }
    }
}
