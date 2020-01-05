using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Shared;
using Xunit;

namespace DSAA.Graph.UnitTests.ShortestPath
{
    public sealed class DistanceTableToPathConverterTests
    {
        public DistanceTableToPathConverterTests()
        {
            Sut = new DistanceTableToPathConverter<string>(Comparer);
        }

        private DistanceTableToPathConverter<string> Sut { get; }

        private static LambdaEqualityComparer<string> Comparer { get; } =
            new LambdaEqualityComparer<string>(string.Equals);

        public static IEnumerable<object[]> ReachableDestinationTests =>
            new List<object[]>
            {
                new object[]
                {
                    "A", "D", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>(2, 2, "B")},
                        {"E", new DistanceInfo<string>(2, 2, "C")}
                    },
                    new GraphPath<string>(new[] {"A", "B", "D"}, 2, Comparer)
                },
                new object[]
                {
                    "A", "D", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>(3, 3, "E")},
                        {"E", new DistanceInfo<string>(2, 2, "C")}
                    },
                    new GraphPath<string>(new[] {"A", "C", "E", "D"}, 3, Comparer)
                },
                new object[]
                {
                    "A", "D", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>(3, 3, "E")},
                        {"E", new DistanceInfo<string>(2, 2, "B")}
                    },
                    new GraphPath<string>(new[] {"A", "B", "E", "D"}, 3, Comparer)
                },
                new object[]
                {
                    "A", "E", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>(3, 3, "E")},
                        {"E", new DistanceInfo<string>(2, 2, "B")}
                    },
                    new GraphPath<string>(new[] {"A", "B", "E"}, 2, Comparer)
                }
            };

        public static IEnumerable<object[]> NotReachableDestinationTests =>
            new List<object[]>
            {
                new object[]
                {
                    "A", "D", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>()},
                        {"E", new DistanceInfo<string>(2, 2, "C")}
                    }
                },
                new object[]
                {
                    "A", "F", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>()},
                        {"E", new DistanceInfo<string>(2, 2, "C")}
                    }
                },
                new object[]
                {
                    "G", "F", new Dictionary<string, DistanceInfo<string>>
                    {
                        {"A", new DistanceInfo<string>(0, 0, "A")},
                        {"B", new DistanceInfo<string>(1, 1, "A")},
                        {"C", new DistanceInfo<string>(1, 1, "A")},
                        {"D", new DistanceInfo<string>()},
                        {"E", new DistanceInfo<string>(2, 2, "C")}
                    }
                }
            };

        [Fact]
        public void Given_DistanceTable_When_Source_And_Destination_AreSame_Then_ReturnOneStepPath()
        {
            // Arrange
            var distances = new Dictionary<string, DistanceInfo<string>>
            {
                {"A", new DistanceInfo<string>(0, 0, "A")},
                {"B", new DistanceInfo<string>(1, 1, "A")},
                {"C", new DistanceInfo<string>(1, 1, "A")}
            };

            // Act
            GraphPath<string> result = Sut.FindPath("A", "A", distances);

            // Assert
            Assert.Equal(new GraphPath<string>(new []{ "A" }, 0, Comparer), result);
        }

        [Fact]
        public void Given_DistanceTable_When_Source_And_Destination_AreSame_ButNotRoot_Then_ReturnOneStepPath()
        {
            // Arrange
            var distances = new Dictionary<string, DistanceInfo<string>>
            {
                {"A", new DistanceInfo<string>(0, 0, "A")},
                {"B", new DistanceInfo<string>(1, 1, "A")},
                {"C", new DistanceInfo<string>(1, 1, "A")}
            };

            // Act
            GraphPath<string> result = Sut.FindPath("C", "C", distances);

            // Assert
            Assert.Equal(new GraphPath<string>(new []{ "C" }, 0, Comparer), result);
        }

        [Fact]
        public void Given_DistanceTable_When_PreviousVertex_And_DestinationAreSame_Then_ReturnThrow()
        {
            // Arrange
            var distances = new Dictionary<string, DistanceInfo<string>>
            {
                {"A", new DistanceInfo<string>(0, 0, "A")},
                {"B", new DistanceInfo<string>(1, 1, "C")},
                {"C", new DistanceInfo<string>(1, 1, "B")}
            };

            // Act
            // Assert
            Assert.Throws<DistanceTableException<string>>(() => Sut.FindPath("A", "C", distances));
        }

        [Theory]
        [MemberData(nameof(ReachableDestinationTests))]
        public void Given_DistanceTable_When_DestinationIsReachable_Then_ReturnPath(string startingVertex,
            string destinationVertex, IDictionary<string, DistanceInfo<string>> distances,
            GraphPath<string> expectedPath)
        {
            // Arrange
            // Act
            GraphPath<string> result = Sut.FindPath(startingVertex, destinationVertex, distances);

            // Assert
            Assert.Equal(expectedPath, result);
        }

        [Theory]
        [MemberData(nameof(NotReachableDestinationTests))]
        public void Given_DistanceTable_When_DestinationIsNotReachable_Or_NonExistent_Then_ReturnNone(
            string startingVertex,
            string destinationVertex, IDictionary<string, DistanceInfo<string>> distances)
        {
            // Arrange
            // Act
            var result = Sut.FindPath(startingVertex, destinationVertex, distances);

            // Assert
            Assert.True(result.IsEmpty);
        }
    }
}