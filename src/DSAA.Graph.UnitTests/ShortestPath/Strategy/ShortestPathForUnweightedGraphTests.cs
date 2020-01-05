using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath;
using DSAA.Graph.UnitTests.Helpers;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Graph.UnitTests.ShortestPath.Strategy
{
    public sealed class ShortestPathForUnweightedGraphTests
    {
        private static readonly IEqualityComparer<int> Comparer = new IntComparer();

        private AdjacencyMatrixGraph<int> Sut { get; } = new AdjacencyMatrixGraph<int>(GraphType.Directed, Comparer);

        public static IEnumerable<object[]> ReachableDestinationTests =>
            new List<object[]>
            {
                // CP3 4.3 U/U
                new object[] { 0, 0, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1 }, 1, Comparer) },
                new object[] { 0, 2, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2 }, 2, Comparer) },
                new object[] { 0, 3, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 3 }, 3, Comparer) },
                new object[] { 0, 4, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 4 }, 1, Comparer) },
                new object[] { 0, 5, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 5 }, 2, Comparer) },
                new object[] { 0, 6, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 6 }, 3, Comparer) },
                new object[] { 0, 7, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 3, 7 }, 4, Comparer) },
                new object[] { 0, 8, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 4, 8 }, 2, Comparer) },
                new object[] { 0, 9, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 4, 8, 9 }, 3, Comparer) },
                new object[] { 0, 10, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 5, 10 }, 3, Comparer) },
                new object[] { 0, 11, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 6, 11 }, 4, Comparer) },
                new object[] { 0, 12, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 3, 7, 12 }, 5, Comparer) },

                new object[] { 6, 0, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 1, 0 }, 3, Comparer) },
                new object[] { 6, 1, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 1 }, 2, Comparer) },
                new object[] { 6, 2, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2 }, 1, Comparer) },
                new object[] { 6, 3, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 3 }, 2, Comparer) },
                new object[] { 6, 4, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 1, 0, 4}, 4, Comparer) },
                new object[] { 6, 5, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 5 }, 1, Comparer) },
                new object[] { 6, 6, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6 }, 0, Comparer) },
                new object[] { 6, 7, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 3, 7 }, 3, Comparer) },
                new object[] { 6, 8, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 5, 10, 9, 8 }, 4, Comparer) },
                new object[] { 6, 9, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 5, 10, 9 }, 3, Comparer) },
                new object[] { 6, 10, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 6, 5, 10 }, 2, Comparer) },
                new object[] { 6, 11, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 6, 11 }, 1, Comparer) },
                new object[] { 6, 12, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 6, 11, 12 }, 2, Comparer) },

                //// CP3 4.4 D/U
                new object[] { 0, 0, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 1 }, 1, Comparer) },
                new object[] { 0, 2, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 2 }, 1, Comparer) },
                new object[] { 0, 3, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 1, 3 }, 2, Comparer) },
                new object[] { 0, 4, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 1, 3, 4 }, 3, Comparer) },
                new object[] { 0, 5, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 2, 5 }, 2, Comparer) },

                new object[] { 2, 2, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 3, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2, 3 }, 1, Comparer) },
                new object[] { 2, 4, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2, 3, 4 }, 2, Comparer) },
                new object[] { 2, 5, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2, 5 }, 1, Comparer) },
            };

        public static IEnumerable<object[]> UnreachableDestinationTests =>
            new List<object[]>
            {
                // CP3 4.4 D/U
                new object[] { 3, 0, Graphs.CP3_44_DU },
                new object[] { 0, 6, Graphs.CP3_44_DU },
                new object[] { 0, 7, Graphs.CP3_44_DU },
                new object[] { 5, 3, Graphs.CP3_44_DU },
                new object[] { 4, 3, Graphs.CP3_44_DU },
                new object[] { 2, 1, Graphs.CP3_44_DU }
            };

        [Theory]
        [MemberData(nameof(ReachableDestinationTests))]
        public void Given_Graph_When_DestinationIsReachable_Then_ReturnPath(int startingVertex,
            int destinationVertex, EdgeDescriptor<int>[] edges, GraphPath<int> expectedPath)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            GraphPath<int> result =
                Sut.FindShortestPath(startingVertex, destinationVertex, o => o.UseUnweightedGraph());

            // Assert
            Assert.Equal(expectedPath, result);
        }

        [Theory]
        [MemberData(nameof(UnreachableDestinationTests))]
        public void Given_Graph_When_DestinationIsNotReachable_Then_ReturnNone(int startingVertex,
            int destinationVertex, EdgeDescriptor<int>[] edges)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            var result =
                Sut.FindShortestPath(startingVertex, destinationVertex, o => o.UseUnweightedGraph());


            // Assert
            Assert.True(result.IsEmpty);
        }
    }
}