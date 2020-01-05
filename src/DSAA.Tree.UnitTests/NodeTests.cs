using System.Collections.Generic;
using DSAA.Shared;
using DSAA.Tree.UnitTests.Helpers;
using Xunit;

namespace DSAA.Tree.UnitTests
{
    public sealed class NodeTests
    {
        private IComparer<int> Comparer { get; }

        public NodeTests()
        {
            Comparer = new LambdaComparer<int>((left, right) => left.CompareTo(right));
        }

        [Theory]
        [InlineData(1, "A")]
        [InlineData(2, "B")]
        [InlineData(3, "C")]
        public void Given_Key_And_Value_When_NodeCreated_Then_MustHaveCorrectKey_And_Value(int key, string value)
        {
            // Arrange
            // Act
            var sut = Node.Leaf(key, value, Comparer);

            // Assert
            sut.AssertLeafEqual(key, value);
        }

        [Fact]
        public void Given_Node_When_LeftNodeAdded_Then_NodeMustNotBeMutated()
        {
            // Arrange
            var node = Node.Leaf(2, "B", Comparer);
            var left = Node.Leaf(1, "A", Comparer);

            // Act
            var sut = node.WithLeft(left);

            // Assert
            node.AssertLeafEqual(2, "B");
            sut.AssertBranchEqual(2, "B");
            sut.Left.AssertLeafEqual(1, "A");
            Assert.Same(sut.Left, left);
            Assert.NotSame(node, sut);
        }

        [Fact]
        public void Given_Node_When_RightNodeAdded_Then_NodeMustNotBeMutated()
        {
            // Arrange
            var node = Node.Leaf(2, "B", Comparer);
            var right = Node.Leaf(3, "C", Comparer);

            // Act
            var sut = node.WithRight(right);

            // Assert
            node.AssertLeafEqual(2, "B");
            sut.AssertBranchEqual(2, "B");
            sut.Right.AssertLeafEqual(3, "C");
            Assert.Same(sut.Right, right);
            Assert.NotSame(node, sut);
        }

        [Theory]
        [InlineData("B1")]
        [InlineData("B2")]
        public void Given_Node_When_ValueAdded_Then_NodeMustNotBeMutated(string value)
        {
            // Arrange
            var node = Node.Leaf(2, "B", Comparer);

            // Act
            var sut = node.WithValue(value);

            // Assert
            node.AssertLeafEqual(2, "B");
            sut.AssertLeafEqual(2, "B", value);
            Assert.NotSame(node, sut);
        } 
        
        [Fact]
        public void Given_Node_When_MultipleValuesAdded_Then_AllValuesShouldBeYielded()
        {
            // Arrange
            var node = Node.Leaf(1, "A", Comparer);

            // Act
            var sut = node.WithValue("B").WithValue("C");

            // Assert
            Assert.Equal(new [] {"A", "B", "C"}, sut);
        }  
        
        [Fact]
        public void Given_Node_When_ComparingSameKey_Then_KeysShouldBeEqual()
        {
            // Arrange
            var sut = Node.Leaf(1, "A", Comparer);

            // Act
            // Assert
            Assert.True(sut.KeyEqualsTo(1));
        } 
        
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Given_Node_When_ComparingDifferentKey_Then_KeysShouldNotBeEqual(int key)
        {
            // Arrange
            var sut = Node.Leaf(1, "A", Comparer);

            // Act
            // Assert
            Assert.False(sut.KeyEqualsTo(key));
        }
        
        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        public void Given_Node_When_KeyIsGreaterOrEqual_Then_ReturnTrue(int key, bool expected)
        {
            // Arrange
            var sut = Node.Leaf(4, "A", Comparer);

            // Act
            // Assert
            Assert.Equal(expected, sut.KeyIsGreaterOrEqualTo(key));
        }
        
        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(5, false)]
        public void Given_Node_When_KeyIsGreater_Then_ReturnTrue(int key, bool expected)
        {
            // Arrange
            var sut = Node.Leaf(4, "A", Comparer);

            // Act
            // Assert
            Assert.Equal(expected, sut.KeyIsGreaterThan(key));
        }
        
        [Theory]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        public void Given_Node_When_KeyIsLessOrEqual_Then_ReturnTrue(int key, bool expected)
        {
            // Arrange
            var sut = Node.Leaf(3, "A", Comparer);

            // Act
            // Assert
            Assert.Equal(expected, sut.KeyIsLessOrEqualTo(key));
        }
        
        [Theory]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        public void Given_Node_When_KeyIsLess_Then_ReturnTrue(int key, bool expected)
        {
            // Arrange
            var sut = Node.Leaf(3, "A", Comparer);

            // Act
            // Assert
            Assert.Equal(expected, sut.KeyIsLessThan(key));
        }  
        
        [Fact]
        public void Given_Node_When_HasNoChildren_Then_NodeIsLeaf()
        {
            // Arrange
            var sut = Node.Leaf(3, "A", Comparer);

            // Act
            // Assert
            Assert.True(sut.Leaf);
        }  

        [Fact]
        public void Given_Node_When_HasBothChildren_Then_NodeIsNotLeaf()
        {
            // Arrange
            var left = Node.Leaf(1, "A", Comparer);
            var right = Node.Leaf(3, "C", Comparer);
            var sut = Node.Leaf(2, "B", Comparer)
                .WithLeft(left)
                .WithRight(right);

            // Act
            // Assert
            Assert.False(sut.Leaf);
        }  

        [Fact]
        public void Given_Node_When_HasLeftChild_Then_NodeIsNotLeaf()
        {
            // Arrange
            var left = Node.Leaf(1, "A", Comparer);
            var sut = Node.Leaf(2, "B", Comparer)
                .WithLeft(left);

            // Act
            // Assert
            Assert.False(sut.Leaf);
        } 
        
        [Fact]
        public void Given_Node_When_HasRightChild_Then_NodeIsNotLeaf()
        {
            // Arrange
            var right = Node.Leaf(3, "C", Comparer);
            var sut = Node.Leaf(2, "B", Comparer)
                .WithRight(right);

            // Act
            // Assert
            Assert.False(sut.Leaf);
        }

        [Fact]
        public void Given_Node_When_HasBothChildren_And_LeftRemoved_Then_NewNodeHasNoLeftChild()
        {
            // Arrange
            var left = Node.Leaf(1, "A", Comparer);
            var right = Node.Leaf(3, "C", Comparer);
            var sut = Node.Leaf(2, "B", Comparer)
                .WithLeft(left)
                .WithRight(right);

            // Act
            sut = sut.NoLeft();

            // Assert
            sut.AssertBranchEqual(2, "B");
            Assert.Null(sut.Left);
            sut.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_Node_When_HasBothChildren_And_RightRemoved_Then_NewNodeHasNoRightChild()
        {
            // Arrange
            var left = Node.Leaf(1, "A", Comparer);
            var right = Node.Leaf(3, "C", Comparer);
            var sut = Node.Leaf(2, "B", Comparer)
                .WithLeft(left)
                .WithRight(right);

            // Act
            sut = sut.WithNoRight();

            // Assert
            sut.AssertBranchEqual(2, "B");
            Assert.Null(sut.Right);
            sut.Left.AssertLeafEqual(1, "A");
        }
    }
}
