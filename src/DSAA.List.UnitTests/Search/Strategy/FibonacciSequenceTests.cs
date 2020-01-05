using DSAA.List.Search.Strategy;
using Xunit;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public sealed class FibonacciSequenceTests
    {
        public FibonacciSequenceTests()
        {
            Sut = FibonacciSequence.First();
        }

        private FibonacciSequence Sut { get; }

        [Fact]
        public void Given_Sequence_When_Initialized_Then_Return_1()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal(1, Sut.N);
        }

        [Fact]
        public void Given_Sequence_When_1StepTaken_Then_Return_2()
        {
            // Arrange
            // Act
            var result = Sut.Next();
            // Assert
            Assert.Equal(2, result.N);
        }

        [Fact]
        public void Given_Sequence_When_8StepsTaken_Then_Return_34()
        {
            // Arrange
            // Act
            var result = Sut
                .Next()
                .Next()
                .Next()
                .Next()
                .Next()
                .Next()
                .Next();
            // Assert
            Assert.Equal(34, result.N);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 0, 3)]
        [InlineData(3, 0, 5)]
        [InlineData(4, 1, 5)]
        [InlineData(29, 1, 832040)]
        [InlineData(7, 3, 8)]
        public void Given_Sequence_When_SequenceOfStepsTaken_Then_ReturnExpectedFibonacciValue(int stepsForward, int stepsBackward, int expectedValue)
        {
            // Arrange
            var result = Sut;

            // Act
            for (int i = 0; i < stepsForward; i++)
            {
                result = result.Next();
            }

            for (int i = 0; i < stepsBackward; i++)
            {
                result = result.Previous();
            }

            // Assert
            Assert.Equal(expectedValue, result.N);
        }
    }
}
