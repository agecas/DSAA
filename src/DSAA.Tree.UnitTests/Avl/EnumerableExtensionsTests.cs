using System.Collections.Generic;
using DSAA.Tree.Avl;
using DSAA.Tree.UnitTests.Helpers;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Tree.UnitTests.Avl
{
    public sealed class EnumerableExtensionsTests
    {
        [Fact]
        public void Given_Dictionary_When_Empty_Then_ReturnEmptyTree()
        {
            // Arrange
            // Act
            var result = new Dictionary<int, string>().ToImmutableAvlTree<int, string, IntComparer>();

            // Assert
            Assert.True(result.IsEmpty);
        }

        [Fact]
        public void Given_Dictionary_When_ContainsData_And_UsingComparerImplementation_Then_ReturnTree()
        {
            // Arrange
            var data = new Dictionary<int, string>
            {
                {4, "D"},
                {2, "B"},
                {1, "A"},
                {3, "C"},
                {6, "F"},
                {5, "E"},
                {7, "G"}
            };

            // Act
            var result = data.ToImmutableAvlTree<int, string, IntComparer>();

            // Assert
            result.AssertRootEqual(4, "D");
            result.Root.Left.AssertBranchEqual(2, "B");
            result.Root.Left.Left.AssertLeafEqual(1, "A");
            result.Root.Left.Right.AssertLeafEqual(3, "C");
            result.Root.Right.AssertBranchEqual(6, "F");
            result.Root.Right.Left.AssertLeafEqual(5, "E");
            result.Root.Right.Right.AssertLeafEqual(7, "G");
        }

        [Fact]
        public void Given_Dictionary_When_ContainsData_And_UsingLambdaComparer_Then_ReturnTree()
        {
            // Arrange
            var data = new Dictionary<int, string>
            {
                {4, "D"},
                {2, "B"},
                {1, "A"},
                {3, "C"},
                {6, "F"},
                {5, "E"},
                {7, "G"}
            };

            // Act
            var result = data.ToImmutableAvlTree((x, y) => x.CompareTo(y));

            // Assert
            result.AssertRootEqual(4, "D");
            result.Root.Left.AssertBranchEqual(2, "B");
            result.Root.Left.Left.AssertLeafEqual(1, "A");
            result.Root.Left.Right.AssertLeafEqual(3, "C");
            result.Root.Right.AssertBranchEqual(6, "F");
            result.Root.Right.Left.AssertLeafEqual(5, "E");
            result.Root.Right.Right.AssertLeafEqual(7, "G");
        }

        [Fact]
        public void Given_Dictionary_When_ContainsData_And_UsingImplicitComparerForComparableTypes_Then_ReturnTree()
        {
            // Arrange
            var data = new Dictionary<int, string>
            {
                {4, "D"},
                {2, "B"},
                {1, "A"},
                {3, "C"},
                {6, "F"},
                {5, "E"},
                {7, "G"}
            };

            // Act
            var result = data.ToImmutableAvlTree();

            // Assert
            result.AssertRootEqual(4, "D");
            result.Root.Left.AssertBranchEqual(2, "B");
            result.Root.Left.Left.AssertLeafEqual(1, "A");
            result.Root.Left.Right.AssertLeafEqual(3, "C");
            result.Root.Right.AssertBranchEqual(6, "F");
            result.Root.Right.Left.AssertLeafEqual(5, "E");
            result.Root.Right.Right.AssertLeafEqual(7, "G");
        }
    }
}