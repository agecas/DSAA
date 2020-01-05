using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.List.Search;
using Xunit;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public abstract class UnsortedListSearchTests
    {
        private Func<SearchOptions<int>, SearchOptions<int>> OptionsFactory { get; }

        protected UnsortedListSearchTests(Func<SearchOptions<int>, SearchOptions<int>> optionsFactory)
        {
            OptionsFactory = optionsFactory ?? throw new ArgumentNullException(nameof(optionsFactory));
            Data = new List<int> {25, 1, 3, 25, 27, 120, 90, 1, 90, 90, 10, 14, -8, 0};
        }

        private List<int> Data { get; }

        #region FindIndex
        
        [Theory]
        [InlineData(25, 0)]
        [InlineData(1, 1)]
        [InlineData(120, 5)]
        [InlineData(14, 11)]
        [InlineData(-8, 12)]
        [InlineData(0, 13)]
        public void Given_List_When_ValueWithinTheList_Then_Return_IndexOfFirstOccurrence(int value, int expectedIndex)
        {
            // Arrange
            // Act
            var result = Data.FindIndex(value, OptionsFactory);

            // Assert
            Assert.Equal(expectedIndex, result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(99)]
        [InlineData(-5)]
        public void Given_List_When_ValueIsNotInTheList_Then_Return_NegativeIndex(int value)
        {
            // Arrange
            // Act
            var result = Data.FindIndex(value, OptionsFactory);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Given_List_When_Empty_Then_Return_NegativeIndex()
        {
            // Arrange
            var data = new List<int>();

            // Act
            var result = data.FindIndex(3, OptionsFactory);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Given_List_When_Null_Then_Throw()
        {
            // Arrange
            List<int> data = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => data.FindIndex(3, OptionsFactory));
        }

        #endregion

        #region FindAllIndexes

        [Fact]
        public void Given_List_When_ValueWithinTheList_Then_Return_IndexesOfAllOccurrences()
        {
            // Arrange
            // Act
            var result = Data.FindAllIndexes(120, OptionsFactory);

            // Assert
            Assert.Collection(result, i => Assert.Equal(5, i));
        }

        [Fact]
        public void Given_List_When_ValueWithinTheListMultipleTimes_Then_Return_IndexesOfAllOccurrences()
        {
            // Arrange
            // Act
            var result = Data.FindAllIndexes(90, OptionsFactory);

            // Assert
            Assert.Collection(result, 
                i => Assert.Equal(6, i),
                i => Assert.Equal(8, i),
                i => Assert.Equal(9, i));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(99)]
        [InlineData(-5)]
        public void Given_List_When_ValueIsNotInTheList_Then_Return_Empty(int value)
        {
            // Arrange
            // Act
            var result = Data.FindAllIndexes(value, OptionsFactory);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Given_List_When_Empty_Then_Return_Empty()
        {
            // Arrange
            var data = new List<int>();

            // Act
            var result = data.FindAllIndexes(3, OptionsFactory);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Given_List_When_SearchingForMultipleValues_But_ListIsNull_Then_Throw()
        {
            // Arrange
            List<int> data = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => data.FindAllIndexes(3, OptionsFactory));
        }

        #endregion

        #region Find

        [Theory]
        [InlineData(25)]
        [InlineData(1)]
        [InlineData(120)]
        [InlineData(14)]
        [InlineData(-8)]
        [InlineData(0)]
        public void Given_List_When_ValueWithinTheList_Then_Return_FirstOccurenceOfTheItem(int value)
        {
            // Arrange
            // Act
            int result = Data.Find(value, OptionsFactory);

            // Assert
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(99)]
        [InlineData(-5)]
        public void Given_List_When_ValueIsNotInTheList_Then_Return_EmptyValueResult(int value)
        {
            // Arrange
            // Act
            var result = Data.Find(value, OptionsFactory);

            // Assert
            Assert.False(result.Any());
        }

        [Fact]
        public void Given_List_When_Empty_Then_Return_EmptyValueResult()
        {
            // Arrange
            var data = new List<int>();

            // Act
            var result = data.Find(3, OptionsFactory);

            // Assert
            Assert.False(result.Any());
        }

        #endregion

        #region FindAll

        [Fact]
        public void Given_List_When_ValueWithinTheList_Then_Return_AllOccurrences()
        {
            // Arrange
            // Act
            var result = Data.FindAll(120, OptionsFactory);

            // Assert
            Assert.Collection(result, i => Assert.Equal(120, i));
        }

        [Fact]
        public void Given_List_When_ValueWithinTheListMultipleTimes_Then_Return_AllOccurrences()
        {
            // Arrange
            // Act
            var result = Data.FindAll(90, OptionsFactory);

            // Assert
            Assert.Collection(result,
                i => Assert.Equal(90, i),
                i => Assert.Equal(90, i),
                i => Assert.Equal(90, i));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(99)]
        [InlineData(-5)]
        public void Given_List_When_ValueIsNotInTheList_Then_Return_EmptyValuesResult(int value)
        {
            // Arrange
            // Act
            var result = Data.FindAll(value, OptionsFactory);

            // Assert
            Assert.False(result.Any());
        }

        [Fact]
        public void Given_List_When_Empty_Then_Return_EmptyValuesResult()
        {
            // Arrange
            var data = new List<int>();

            // Act
            var result = data.FindAll(3, OptionsFactory);

            // Assert
            Assert.False(result.Any());
        }

        #endregion
    }
}