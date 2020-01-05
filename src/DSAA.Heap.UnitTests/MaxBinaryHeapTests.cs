using System.Collections.Generic;
using System.Linq;
using DSAA.Shared;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Heap.UnitTests
{
    public sealed class MaxBinaryHeapTests
    {
        [Theory]
        [InlineData(32)]
        [InlineData(56)]
        [InlineData(72)]
        public void Given_Heap_When_HeapContainsValue_Then_ReturnTrue(int value)
        {
            // Arrange
            var sut = new[] {1, 20, 32, 56, 5, 3, 10, 100, 72}.ToMaxBinaryHeap();

            // Act
            // Assert
            Assert.True(sut.Contains(value));
        } 

        [Theory]
        [InlineData(7)]
        [InlineData(-5)]
        [InlineData(999)]
        public void Given_Heap_When_HeapDoesNotContainValue_Then_ReturnFalse(int value)
        {
            // Arrange
            var sut = new[] {1, 20, 32, 56, 5, 3, 10, 100, 72}.ToMaxBinaryHeap();

            // Act
            // Assert
            Assert.False(sut.Contains(value));
        } 
        
        [Fact]
        public void Given_EmptyHeap_When_ValueExistenceChecked_Then_ReturnFalse()
        {
            // Arrange
            var sut = new int[] {}.ToMaxBinaryHeap();

            // Act
            // Assert
            Assert.False(sut.Contains(100));
        }

        [Theory]
        [MemberData(nameof(InsertTestCases))]
        public void Given_Heap_When_MultipleValuesAdded_Then_CorrectHeapShouldBeBuilt(int[] input, int[] expected,
            int expectedTopValue)
        {
            // Arrange
            var sut = input.ToMaxBinaryHeap();

            // Act
            // Assert
            Assert.Equal(expected, sut);
            Assert.Equal(expectedTopValue, sut.Peek().Single());
        }

        [Theory]
        [MemberData(nameof(DeleteTopPriorityValueTestCases))]
        public void Given_Heap_When_TopValueRemoved_Then_HeapShouldBeRearranged(int[] input, int[] expected,
            int expectedDequeuedValue, int expectedTopValue)
        {
            // Arrange
            var sut = input.ToMaxBinaryHeap();

            // Act
            var dequeuedValue = sut.Pop();

            // Assert
            Assert.Equal(expected, sut);
            Assert.Equal(expectedTopValue, sut.Peek().Single());
            Assert.Equal(expectedDequeuedValue, dequeuedValue.Single());
        }

        [Theory]
        [MemberData(nameof(DeleteAnyValueTestCases))]
        public void Given_Heap_When_ValueRemoved_Then_HeapShouldBeRearranged(int[] input, int[] expected, int valueToDelete, int expectedTopValue)
        {
            // Arrange
            var sut = input.ToMaxBinaryHeap();

            // Act
            sut.Remove(valueToDelete);

            // Assert
            Assert.Equal(expected, sut);
            Assert.Equal(expectedTopValue, sut.Peek().Single());
        }

        [Fact]
        public void Given_EmptyHeap_When_Peeked_Then_ReturnNone()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            // Assert
            Assert.False(sut.Peek().HasData);
        }

        [Fact]
        public void Given_EmptyHeap_When_Popped_Then_ReturnNone()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            // Assert
            Assert.False(sut.Pop().HasData);
        }

        [Fact]
        public void Given_EmptyHeap_When_ValueAdded_Then_ValueBecomesHeapRoot()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            sut.Push(5);

            // Assert
            Assert.Equal(5, sut.Peek().Single());
        }

        [Fact]
        public void Given_HeapWithSingleValue_When_LastValueRemoved_Then_HeapShouldBeEmpty()
        {
            // Arrange
            var sut = new[] {202}.ToMaxBinaryHeap();

            // Act
            var dequeuedValue = sut.Pop();

            // Assert
            Assert.Empty(sut);
            Assert.Equal(Optional<int>.None(), sut.Peek());
            Assert.Equal(202, dequeuedValue.Single());
        }

        #region SUT Setup / Data

        private MaxBinaryHeap<int> CreateSut()
        {
            return new MaxBinaryHeap<int>(new IntComparer());
        }

        public static IEnumerable<object[]> InsertTestCases =>
            new List<object[]>
            {
                new object[] {new[] { 1 }, new[] { 1 }, 1 },
                new object[] {new[] { 1, 20 }, new[] { 20, 1 }, 20 },
                new object[] {new[] { 1, 20, 32 }, new[] { 32, 1, 20 }, 32 },
                new object[] {new[] { 1, 20, 32, 56 }, new[] { 56, 32, 20, 1 }, 56 },
                new object[] {new[] { 1, 20, 32, 56, 5 }, new[] { 56, 32, 20, 1, 5 }, 56 },
                new object[] {new[] { 1, 20, 32, 56, 5, 3 }, new[] { 56, 32, 20, 1, 5, 3 }, 56 },
                new object[] {new[] { 1, 20, 32, 56, 5, 3, 10 }, new[] { 56, 32, 20, 1, 5, 3, 10 }, 56 },
                new object[] {new[] { 1, 20, 32, 56, 5, 3, 10, 100 }, new[] { 100, 56, 20, 32, 5, 3, 10, 1 }, 100 },
                new object[] {new[] { 1, 20, 32, 56, 5, 3, 10, 100, 72 }, new[] { 100, 72, 20, 56, 5, 3, 10, 1, 32 }, 100 }
            };

        public static IEnumerable<object[]> DeleteTopPriorityValueTestCases =>
            new List<object[]>
            {
                new object[] {new[] { 100, 72, 32, 56, 5, 3, 10, 1, 20 }, new[] { 72, 56, 32, 20, 5, 3, 10, 1}, 100, 72 },
                new object[] {new[] { 72, 56, 32, 20, 5, 3, 10, 1 }, new[] {56, 20, 32, 1, 5, 3, 10}, 72, 56 },
                new object[] {new[] { 56, 20, 32, 1, 5, 3, 10 }, new[] { 32, 20, 10, 1, 5, 3 }, 56, 32 },
                new object[] {new[] { 32, 20, 10, 1, 5, 3 }, new[] { 20, 5, 10, 1, 3 }, 32, 20 },
                new object[] {new[] { 20, 5, 10, 1, 3 }, new[] { 10, 5, 3, 1 }, 20, 10 },
                new object[] {new[] { 10, 5, 3, 1 }, new[] { 5, 1, 3 }, 10, 5 },
                new object[] {new[] { 5, 1, 3 }, new[] { 3, 1 }, 5, 3 },
                new object[] {new[] { 3, 1 }, new[] { 1 }, 3, 1 },
            };

        public static IEnumerable<object[]> DeleteAnyValueTestCases =>
            new List<object[]>
            {
                // Removing NON existent value
                new object[] {new[] { 100, 72, 32, 56, 5, 3, 10, 1, 20 }, new[] { 100, 72, 32, 56, 5, 3, 10, 1, 20 }, 999, 100 },

                //// Removing Arbitrary value from Left/Right side's
                new object[] {new[] { 100, 72, 32, 56, 5, 3, 10, 1, 40 }, new[] { 100, 72, 40, 56, 5, 32, 10, 1 }, 3, 100 },
                new object[] {new[] { 100, 72, 32, 56, 5, 3, 10, 1, 20 }, new[] { 100, 56, 32, 20, 5, 3, 10, 1 }, 72, 100 },

                // Removing TOP value
                new object[] {new[] { 100, 72, 32, 56, 5, 3, 10, 1, 20 }, new[] { 72, 56, 32, 20, 5, 3, 10, 1}, 100, 72 },
                new object[] {new[] { 72, 56, 32, 20, 5, 3, 10, 1 }, new[] {56, 20, 32, 1, 5, 3, 10}, 72, 56 },
                new object[] {new[] { 56, 20, 32, 1, 5, 3, 10 }, new[] { 32, 20, 10, 1, 5, 3 }, 56, 32 },
                new object[] {new[] { 32, 20, 10, 1, 5, 3 }, new[] { 20, 5, 10, 1, 3 }, 32, 20 },
                new object[] {new[] { 20, 5, 10, 1, 3 }, new[] { 10, 5, 3, 1 }, 20, 10 },
                new object[] {new[] { 10, 5, 3, 1 }, new[] { 5, 1, 3 }, 10, 5 },
                new object[] {new[] { 5, 1, 3 }, new[] { 3, 1 }, 5, 3 },
                new object[] {new[] { 3, 1 }, new[] { 1 }, 3, 1 },

                // Removing LAST Value
                new object[] {new[] { 100, 72, 32, 56, 5, 3, 10, 1, 20 }, new[] { 72, 56, 32, 20, 5, 3, 10, 1}, 100, 72 },
            };

        #endregion

    }
}