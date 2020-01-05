using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.UnitTests.Helpers;
using Xunit;

namespace DSAA.Graph.UnitTests
{
    public abstract class GraphTests
    {
        protected GraphTests(Func<IGraph<int>> graphFactory)
        {
            Sut = graphFactory();
        }

        protected IGraph<int> Sut { get; }

        [Fact]
        public void Given_EmptyGraph_When_NonExistingEdgeQueried_Then_ReturnEmpty()
        {
            // Arrange
            // Act
            var result = Sut.GetAdjacentVertices(3);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Given_Graph_When_NonExistingEdgeQueried_Then_ReturnEmpty()
        {
            // Arrange
            // Act
            var result = Sut.AddEdge(1, 2).GetAdjacentVertices(3);

            // Assert
            Assert.Empty(result);
            Assert.Equal(2, Sut.Count);
        }

        [Fact]
        public void Given_Graph_When_AddedSelfPointingEdge_Then_ReturnVertexWithNoEdges()
        {
            // Arrange
            // Act
            var result = Sut.AddEdge(1, 1).GetAdjacentVertices(1);

            // Assert
            Assert.Empty(result);
            Assert.Equal(1, Sut.Count);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(4, true)]
        [InlineData(100, false)]
        public void Given_Graph_When_CheckingEdge_Then_ReturnTrueIfFound(int edge, bool expected)
        {
            // Arrange
            Sut.AddEdge(1, 2, 3)
               .AddEdge(3, 5)
               .AddEdge(5, 2, 4)
               .AddEdge(2, 4);

            // Act
            // Assert
            Assert.Equal(expected, Sut.Contains(edge));
        }

        [Theory]
        [MemberData(nameof(EnumeratingTestCases))]
        public void Given_GraphWithAtLeastOneVertex_When_Enumerated_Then_ReturnAllEdges(EdgeDescriptor<int>[] edges, int[] expectedSequence)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            // Assert
            Assert.Equal(expectedSequence, Sut.OrderBy(v => v));
        }

        [Theory]
        [MemberData(nameof(EdgeCountTestCases))]
        public void Given_Graph_When_SomeOrNoneEdgesAreAdded_Then_ReturnCorrectCount(EdgeDescriptor<int>[] edges, int expectedCount)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            // Assert
            Assert.Equal(expectedCount, Sut.Count);
        }

        public static IEnumerable<object[]> EdgeCountTestCases =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0 },
                new object[] { new[]
                {
                    new EdgeDescriptor<int>(1, 2), 
                }, 2 },  
                new object[] { new[]
                {
                    new EdgeDescriptor<int>(1, 2, 3), 
                    new EdgeDescriptor<int>(1, 4, 5), 
                    new EdgeDescriptor<int>(5, 2, 3), 
                }, 5 },
            };

        public static IEnumerable<object[]> EnumeratingTestCases =>
           new List<object[]>
           {
                new object[] { new EdgeDescriptor<int>[] {}, new int[0] },
                new object[] { Graphs.CP3_417_DAG, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP3_44_DAG, new[] { 0, 1, 2, 3, 4, 5, 6, 7 } },
                new object[] { Graphs.CP_49, new[] { 0, 1, 2, 3, 4, 5, 6, 7 } },
                new object[] { Graphs.CP_419_Bipartite, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP_41, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
                new object[] { Graphs.CP_43, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 } },
           };
    }
}