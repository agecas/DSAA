using System;
using System.Collections.Generic;
using Xunit;

namespace DSAA.Core.UnitTests
{
    public class LinearSearchTests
    {
        public LinearSearchTests()
        {
            Data = new List<int> {25, 1, 3, 25, 27, 120, 90, 1, 90, 90, 10, 14, -8, 0};
        }

        private List<int> Data { get; }

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
            var result = Data.FindIndex(n => n == value, opt => opt.UseLinearSearch());

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
            var result = Data.FindIndex(n => n == value, opt => opt.UseLinearSearch());

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Given_List_When_Empty_Then_Return_NegativeIndex()
        {
            // Arrange
            var data = new List<int>();

            // Act
            var result = data.FindIndex(n => n > 3, opt => opt.UseLinearSearch());

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
            Assert.Throws<ArgumentNullException>(() => data.FindIndex(n => n > 3, opt => opt.UseLinearSearch()));
        }










        //[Fact]
        //public void Given_List_When_ValueWithinTheList_Then_Return_IndexesOfAllOccurances()
        //{
        //    // Arrange
        //    // Act
        //    var result = Data.FindAllIndexes(n => n == value, opt => opt.UseLinearSearch());

        //    // Assert
        //    Assert.Equal(expectedIndex, result);
        //}

        //[Theory]
        //[InlineData(4)]
        //[InlineData(99)]
        //[InlineData(-5)]
        //public void Given_List_When_ValueIsNotInTheList_Then_Return_Empty(int value)
        //{
        //    // Arrange
        //    // Act
        //    var result = Data.FindAllIndexes(n => n == value, opt => opt.UseLinearSearch());

        //    // Assert
        //    Assert.Equal(-1, result);
        //}

        //[Fact]
        //public void Given_List_When_Empty_Then_Return_Empty()
        //{
        //    // Arrange
        //    var data = new List<int>();

        //    // Act
        //    var result = data.FindAllIndexes(n => n > 3, opt => opt.UseLinearSearch());

        //    // Assert
        //    Assert.Equal(-1, result);
        //}

        //[Fact]
        //public void Given_List_When_Null_Then_Throw()
        //{
        //    // Arrange
        //    List<int> data = null;

        //    // Act
        //    // Assert
        //    Assert.Throws<ArgumentNullException>(() => data.FindAllIndexes(n => n > 3, opt => opt.UseLinearSearch()));
        //}
    }
}