using System.Collections.Generic;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Heap.UnitTests
{
    public sealed class EnumerableExtensionsTests
    {
        [Fact]
        public void Given_Collection_When_Empty_Then_ReturnEmptyMinBinaryHeap()
        {
            // Arrange
            // Act
            var result = new List<int>().ToMinBinaryHeap();

            // Assert
            Assert.True(result.Empty);
        }

        [Fact]
        public void Given_Collection_When_ContainsData_And_UsingComparerImplementation_Then_ReturnMinBinaryHeap()
        {
            // Arrange
            var data = new List<int> { 150, 70, 202, 34 };

            // Act
            var result = data.ToMinBinaryHeap<int, IntComparer>();

            // Assert
            Assert.Equal(new[] { 34, 70, 202, 150 }, result);
        }

        [Fact]
        public void Given_Collection_When_ContainsData_And_UsingLambdaComparer_Then_ReturnMinBinaryHeap()
        {
            // Arrange
            var data = new List<int> { 150, 70, 202, 34 };

            // Act
            var result = data.ToMinBinaryHeap((x, y) => x.CompareTo(y));

            // Assert
            Assert.Equal(new[] { 34, 70, 202, 150 }, result);
        }

        [Fact]
        public void Given_Collection_When_ContainsData_And_UsingImplicitComparerForComparableTypes_Then_ReturnMinBinaryHeap()
        {
            // Arrange
            var data = new List<int> { 150, 70, 202, 34 };

            // Act
            var result = data.ToMinBinaryHeap();

            // Assert
            Assert.Equal(new[] { 34, 70, 202, 150 }, result);
        }

        [Fact]
        public void Given_Collection_When_Empty_Then_ReturnEmptyMaxBinaryHeap()
        {
            // Arrange
            // Act
            var result = new List<int>().ToMaxBinaryHeap();

            // Assert
            Assert.True(result.Empty);
        }

        [Fact]
        public void Given_Collection_When_ContainsData_And_UsingComparerImplementation_Then_ReturnMaxBinaryHeap()
        {
            // Arrange
            var data = new List<int> { 1, 20, 32, 56, 5 };

            // Act
            var result = data.ToMaxBinaryHeap<int, IntComparer>();

            // Assert
            Assert.Equal(new[] { 56, 32, 20, 1, 5 }, result);
        }

        [Fact]
        public void Given_Collection_When_ContainsData_And_UsingLambdaComparer_Then_ReturnMaxBinaryHeap()
        {
            // Arrange
            var data = new List<int> { 1, 20, 32, 56, 5 };

            // Act
            var result = data.ToMaxBinaryHeap((x, y) => x.CompareTo(y));

            // Assert
            Assert.Equal(new[] { 56, 32, 20, 1, 5 }, result);
        }

        [Fact]
        public void Given_Collection_When_ContainsData_And_UsingImplicitComparerForComparableTypes_Then_ReturnMaxBinaryHeap()
        {
            // Arrange
            var data = new List<int> { 1, 20, 32, 56, 5 };

            // Act
            var result = data.ToMaxBinaryHeap();

            // Assert
            Assert.Equal(new[] { 56, 32, 20, 1, 5 }, result);
        }
    }
}