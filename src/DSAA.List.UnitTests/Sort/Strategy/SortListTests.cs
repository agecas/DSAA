using System;
using System.Collections.Generic;
using DSAA.List.Sort;
using Xunit;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public abstract class SortListTests
    {
        protected SortListTests(Func<SortOptions<int>, SortOptions<int>> optionsFactory)
        {
            OptionsFactory = optionsFactory ?? throw new ArgumentNullException(nameof(optionsFactory));
        }

        private Func<SortOptions<int>, SortOptions<int>> OptionsFactory { get; }

        [Fact]
        public void Given_Array_When_Empty_Then_ReturnEmpty()
        {
            // Arrange
            var list = new List<int>();

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Given_Array_When_ArrayHasSingleValue_Then_Return()
        {
            // Arrange
            var list = new List<int> { 100 };

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(100, v)
            );
        }

        [Fact]
        public void Given_Array_When_ArrayIsUnsorted_And_ValuesAreDistinct_Then_ReturnSortedInAscendingOrder()
        {
            // Arrange
            var list = new List<int> {100, 2, 3, 1, 56, 78, 209, 46, 21, 10, 12, 15, 51};

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(1, v),
                v => Assert.Equal(2, v),
                v => Assert.Equal(3, v),
                v => Assert.Equal(10, v),
                v => Assert.Equal(12, v),
                v => Assert.Equal(15, v),
                v => Assert.Equal(21, v),
                v => Assert.Equal(46, v),
                v => Assert.Equal(51, v),
                v => Assert.Equal(56, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(100, v),
                v => Assert.Equal(209, v)
            );
        }

        [Fact]
        public void Given_Array_When_ArrayIsUnsorted_And_HasDuplicates_Then_ReturnSortedInAscendingOrder()
        {
            // Arrange
            var list = new List<int> { 100, 2, 3, 1, 56, 78, 209, 21, 46, 78, 10, 12, 1, 51, 15 };

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(1, v),
                v => Assert.Equal(1, v),
                v => Assert.Equal(2, v),
                v => Assert.Equal(3, v),
                v => Assert.Equal(10, v),
                v => Assert.Equal(12, v),
                v => Assert.Equal(15, v),
                v => Assert.Equal(21, v),
                v => Assert.Equal(46, v),
                v => Assert.Equal(51, v),
                v => Assert.Equal(56, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(100, v),
                v => Assert.Equal(209, v)
            );
        }

        [Fact]
        public void Given_Array_When_ArraySorted_And_HasDistinctValues_Then_ReturnSortedInAscendingOrder()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 10, 12, 15, 21, 46, 51, 56, 78, 100, 209 };

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(1, v),
                v => Assert.Equal(2, v),
                v => Assert.Equal(3, v),
                v => Assert.Equal(10, v),
                v => Assert.Equal(12, v),
                v => Assert.Equal(15, v),
                v => Assert.Equal(21, v),
                v => Assert.Equal(46, v),
                v => Assert.Equal(51, v),
                v => Assert.Equal(56, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(100, v),
                v => Assert.Equal(209, v)
            );
        }

        [Fact]
        public void Given_Array_When_ArraySorted_And_HasDuplicates_Then_ReturnSortedInAscendingOrder()
        {
            // Arrange
            var list = new List<int> { 1, 1, 2, 3, 10, 12, 15, 21, 46, 51, 56, 78, 78, 100, 209 };

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(1, v),
                v => Assert.Equal(1, v),
                v => Assert.Equal(2, v),
                v => Assert.Equal(3, v),
                v => Assert.Equal(10, v),
                v => Assert.Equal(12, v),
                v => Assert.Equal(15, v),
                v => Assert.Equal(21, v),
                v => Assert.Equal(46, v),
                v => Assert.Equal(51, v),
                v => Assert.Equal(56, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(100, v),
                v => Assert.Equal(209, v)
            );
        }

        [Fact]
        public void Given_Array_When_ArraySortedInDescendingOrder_And_HasDistinctValues_Then_ReturnSortedInAscendingOrder()
        {
            // Arrange
            var list = new List<int> { 209, 100, 78, 56, 51, 46, 21, 15, 12, 10, 3, 2, 1 };

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(1, v),
                v => Assert.Equal(2, v),
                v => Assert.Equal(3, v),
                v => Assert.Equal(10, v),
                v => Assert.Equal(12, v),
                v => Assert.Equal(15, v),
                v => Assert.Equal(21, v),
                v => Assert.Equal(46, v),
                v => Assert.Equal(51, v),
                v => Assert.Equal(56, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(100, v),
                v => Assert.Equal(209, v)
            );
        }

        [Fact]
        public void Given_Array_When_ArraySortedInDescendingOrder_And_HasDuplicates_Then_ReturnSortedInAscendingOrder()
        {
            // Arrange
            var list = new List<int> { 209, 100, 78, 78, 56, 51, 46, 21, 15, 12, 10, 3, 2, 1, 1 };

            // Act
            var result = list.Sort(OptionsFactory);

            // Assert
            Assert.Collection(result,
                v => Assert.Equal(1, v),
                v => Assert.Equal(1, v),
                v => Assert.Equal(2, v),
                v => Assert.Equal(3, v),
                v => Assert.Equal(10, v),
                v => Assert.Equal(12, v),
                v => Assert.Equal(15, v),
                v => Assert.Equal(21, v),
                v => Assert.Equal(46, v),
                v => Assert.Equal(51, v),
                v => Assert.Equal(56, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(78, v),
                v => Assert.Equal(100, v),
                v => Assert.Equal(209, v)
            );
        }
    }
}