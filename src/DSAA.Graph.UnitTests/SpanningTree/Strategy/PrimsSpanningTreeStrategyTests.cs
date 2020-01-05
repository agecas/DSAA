using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;
using DSAA.Graph.UnitTests.Helpers;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Graph.UnitTests.SpanningTree.Strategy
{
    public sealed class PrimsSpanningTreeStrategyTests
    {
        private static readonly IEqualityComparer<int> Comparer = new IntComparer();

        private static readonly IEqualityComparer<WeightedEdge<int>> EdgeComparer =
            WeightedEdge<int>.NonDirectionalComparer(Comparer);

        private IGraph<int> Sut { get; } = new AdjacencyMatrixGraph<int>(GraphType.Undirected, Comparer);

        public static IEnumerable<object[]> ConnectedGraphTests =>
            new List<object[]>
            {
                new object[]
                {
                    Graphs.CP_410, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 4), (3, 6), (4, 6)),
                            new EdgeDescriptor<int>(1, (2, 2))
                        }
                    }
                },
                new object[]
                {
                    Graphs.CP_414, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 9)),
                            new EdgeDescriptor<int>(1, (3, 19)),
                            new EdgeDescriptor<int>(3, (2, 51), (4, 31))
                        }
                    }
                },
                new object[]
                {
                    Graphs.K5, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (2, 13), (3, 13)),
                            new EdgeDescriptor<int>(1, (4, 13)),
                            new EdgeDescriptor<int>(3, (1, 13))
                        }
                    }
                },
                new object[]
                {
                    Graphs.Rail, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 10)),
                            new EdgeDescriptor<int>(1, (2, 10), (6, 8)),
                            new EdgeDescriptor<int>(2, (3, 10), (7, 8)),
                            new EdgeDescriptor<int>(3, (4, 10), (8, 8)),
                            new EdgeDescriptor<int>(6, (5, 10)),
                            new EdgeDescriptor<int>(8, (9, 10))
                        },
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 10)),
                            new EdgeDescriptor<int>(1, (2, 10), (6, 8)),
                            new EdgeDescriptor<int>(2, (7, 8)),
                            new EdgeDescriptor<int>(3, (4, 10)),
                            new EdgeDescriptor<int>(6, (5, 10)),
                            new EdgeDescriptor<int>(7, (8, 10)),
                            new EdgeDescriptor<int>(8, (3, 8), (9, 10))
                        }
                    }
                },
                new object[]
                {
                    Graphs.Tessellation, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 8), (2, 12)),
                            new EdgeDescriptor<int>(1, (4, 9)),
                            new EdgeDescriptor<int>(2, (3, 14)),
                            new EdgeDescriptor<int>(3, (5, 8)),
                            new EdgeDescriptor<int>(5, (7, 11)),
                            new EdgeDescriptor<int>(7, (8, 9)),
                            new EdgeDescriptor<int>(8, (6, 11))
                        }
                    }
                },
                new object[]
                {
                    Graphs.Ds0Prims, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 2), (2, 3)),
                            new EdgeDescriptor<int>(1, (3, 2)),
                            new EdgeDescriptor<int>(3, (4, 4))
                        }
                    }
                },
                new object[]
                {
                    Graphs.Ds1Prims, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 3)),
                            new EdgeDescriptor<int>(1, (2, 2), (4, 5)),
                            new EdgeDescriptor<int>(4, (5, 4)),
                            new EdgeDescriptor<int>(5, (3, 4))
                        },
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 3), (4, 5)),
                            new EdgeDescriptor<int>(1, (2, 2)),
                            new EdgeDescriptor<int>(4, (5, 4)),
                            new EdgeDescriptor<int>(5, (3, 4))
                        }
                    }
                }
            };

        public static IEnumerable<object[]> DisconnectedGraphTests =>
            new List<object[]>
            {
                new object[]
                {
                    Graphs.Rail_0_Disconnected, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0)
                        }
                    }
                },
                new object[]
                {
                    Graphs.Rail_5_Disconnected, new[]
                    {
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 10)),
                            new EdgeDescriptor<int>(1, (2, 10), (6, 8)),
                            new EdgeDescriptor<int>(2, (3, 10)),
                            new EdgeDescriptor<int>(2, (7, 8)),
                            new EdgeDescriptor<int>(3, (4, 10)),
                            new EdgeDescriptor<int>(3, (8, 8)),
                            new EdgeDescriptor<int>(8, (9, 10))
                        },
                        new []
                        {
                            new EdgeDescriptor<int>(0, (1, 10)),
                            new EdgeDescriptor<int>(1, (2, 10), (6, 8)),
                            new EdgeDescriptor<int>(2, (7, 8)),
                            new EdgeDescriptor<int>(3, (4, 10)),
                            new EdgeDescriptor<int>(7, (8, 10)),
                            new EdgeDescriptor<int>(8, (3, 8), (9, 10))
                        }
                    }
                },
            };

        [Theory]
        [MemberData(nameof(ConnectedGraphTests))]
        public void Given_Graph_When_Connected_Then_ReturnSpanningTreeForEntireGraph(EdgeDescriptor<int>[] edges,
            EdgeDescriptor<int>[][] expectedEdgeCollections)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            var result = Sut.FindSpanningTree(o => o.UsePrims())
                .Single()
                .OrderBy(e => e.Source)
                .ThenBy(e => e.Destination)
                .ToList();

            // Assert
            expectedEdgeCollections.AssertEqualsToAny(result, EdgeComparer);
        }

        [Theory]
        [MemberData(nameof(DisconnectedGraphTests))]
        public void Given_Graph_When_Disconnected_Then_ReturnPartialSpanningTree(EdgeDescriptor<int>[] edges,
            EdgeDescriptor<int>[][] expectedEdgeCollections)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            var result = Sut.FindSpanningTree(o => o.UsePrims())
                .Single()
                .OrderBy(e => e.Source)
                .ThenBy(e => e.Destination)
                .ToList();

            // Assert
            expectedEdgeCollections.AssertEqualsToAny(result, EdgeComparer);
        }

        [Fact]
        public void Given_Graph_When_Empty_Then_ReturnEmpty()
        {
            // Arrange
            // Act
            var result = Sut.FindSpanningTree(o => o.UsePrims());

            // Assert
            Assert.True(result.IsEmpty);
        }
    }
}