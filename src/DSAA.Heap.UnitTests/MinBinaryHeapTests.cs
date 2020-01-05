using System.Collections.Generic;
using System.Linq;
using DSAA.Shared;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Heap.UnitTests
{
    public sealed class MinBinaryHeapTests
    {
        [Theory]
        [InlineData(32)]
        [InlineData(56)]
        [InlineData(72)]
        public void Given_Heap_When_HeapContainsValue_Then_ReturnTrue(int value)
        {
            // Arrange
            var sut = new[] { 1, 20, 32, 56, 5, 3, 10, 100, 72 }.ToMinBinaryHeap();

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
            var sut = new[] { 1, 20, 32, 56, 5, 3, 10, 100, 72 }.ToMinBinaryHeap();

            // Act
            // Assert
            Assert.False(sut.Contains(value));
        }

        [Fact]
        public void Given_EmptyHeap_When_ValueExistenceChecked_Then_ReturnFalse()
        {
            // Arrange
            var sut = new int[] { }.ToMinBinaryHeap();

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
            var sut = input.ToMinBinaryHeap();

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
            var sut = input.ToMinBinaryHeap();

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
            var sut = input.ToMinBinaryHeap();

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
            var sut = new[] {202}.ToMinBinaryHeap();

            // Act
            var dequeuedValue = sut.Pop();

            // Assert
            Assert.Empty(sut);
            Assert.Equal(Optional<int>.None(), sut.Peek());
            Assert.Equal(202, dequeuedValue.Single());
        }

        #region SUT Setup / Data

        private MinBinaryHeap<int> CreateSut()
        {
            return new MinBinaryHeap<int>(new IntComparer());
        }

        public static IEnumerable<object[]> InsertTestCases =>
            new List<object[]>
            {
                new object[] {new[] {150}, new[] {150}, 150},
                new object[] {new[] {150, 70}, new[] {70, 150}, 70},
                new object[] {new[] {150, 70, 202}, new[] {70, 150, 202}, 70},
                new object[] {new[] {150, 70, 202, 34}, new[] {34, 70, 202, 150}, 34},
                new object[] {new[] {150, 70, 202, 34, 42}, new[] {34, 42, 202, 150, 70}, 34},
                new object[] {new[] {150, 70, 202, 34, 42, 1}, new[] {1, 42, 34, 150, 70, 202}, 1},
                new object[] {new[] {150, 70, 202, 34, 42, 1, 3}, new[] {1, 42, 3, 150, 70, 202, 34}, 1},
                new object[] {new[] {150, 70, 202, 34, 42, 1, 3, 10}, new[] {1, 10, 3, 42, 70, 202, 34, 150}, 1},
                new object[] {new[] {150, 70, 202, 34, 42, 1, 3, 10, 21}, new[] {1, 10, 3, 21, 70, 202, 34, 150, 42}, 1}
            };

        public static IEnumerable<object[]> DeleteTopPriorityValueTestCases =>
            new List<object[]>
            {
                new object[]
                    {new[] {1, 10, 3, 21, 42, 202, 150, 34, 70}, new[] {3, 10, 70, 21, 42, 202, 150, 34}, 1, 3},
                new object[] {new[] {3, 10, 70, 21, 42, 202, 150, 34}, new[] {10, 21, 70, 34, 42, 202, 150}, 3, 10},
                new object[] {new[] {10, 21, 70, 34, 42, 202, 150}, new[] {21, 34, 70, 150, 42, 202}, 10, 21},
                new object[] {new[] {21, 34, 70, 150, 42, 202}, new[] {34, 42, 70, 150, 202}, 21, 34},
                new object[] {new[] {34, 42, 70, 150, 202}, new[] {42, 150, 70, 202}, 34, 42},
                new object[] {new[] {42, 150, 70, 202}, new[] {70, 150, 202}, 42, 70},
                new object[] {new[] {70, 150, 202}, new[] {150, 202}, 70, 150},
                new object[] {new[] {150, 202}, new[] {202}, 150, 202}
            };

        public static IEnumerable<object[]> DeleteAnyValueTestCases =>
            new List<object[]>
            {
                // Removing NON existent value
                new object[] {new[] { 1, 5, 6, 9, 11, 8, 15, 17, 21 }, new[] { 1, 5, 6, 9, 11, 8, 15, 17, 21 }, 999, 1},
                new object[] {new[] { 1, 5, 6, 9, 11, 8, 15, 17, 21 }, new[] { 1, 9, 6, 17, 11, 8, 15, 21 }, 5, 1},
                new object[] {new[] { 1, 9, 22, 17, 11, 33, 27, 21, 19 }, new[] { 1, 9, 19, 17, 11, 22, 27, 21 }, 33, 1},

                // Removing TOP value
                new object[]
                    {new[] {1, 10, 3, 21, 42, 202, 150, 34, 70}, new[] {3, 10, 70, 21, 42, 202, 150, 34}, 1, 3},
                new object[] {new[] {3, 10, 70, 21, 42, 202, 150, 34}, new[] {10, 21, 70, 34, 42, 202, 150}, 3, 10},
                new object[] {new[] {10, 21, 70, 34, 42, 202, 150}, new[] {21, 34, 70, 150, 42, 202}, 10, 21},
                new object[] {new[] {21, 34, 70, 150, 42, 202}, new[] {34, 42, 70, 150, 202}, 21, 34},
                new object[] {new[] {34, 42, 70, 150, 202}, new[] {42, 150, 70, 202}, 34, 42},
                new object[] {new[] {42, 150, 70, 202}, new[] {70, 150, 202}, 42, 70},
                new object[] {new[] {70, 150, 202}, new[] {150, 202}, 70, 150},
                new object[] {new[] {150, 202}, new[] {202}, 150, 202},

                // Removing LAST Value
                new object[] {new[] { 1, 5, 6, 9, 11, 8, 15, 17, 21 }, new[] { 1, 5, 6, 9, 11, 8, 15, 17 }, 21, 1},
            };

        #endregion

    }
}