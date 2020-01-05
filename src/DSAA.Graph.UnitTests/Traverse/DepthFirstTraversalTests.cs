using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Fluent;
using DSAA.Graph.UnitTests.Helpers;
using Xunit;

namespace DSAA.Graph.UnitTests.Traverse
{
    /// <summary>
    /// Examples taken from: https://visualgo.net/en/dfsbfs
    /// Matrix and Set based test cases differ due to underlying implementations NOT preserving node order and
    /// if a graph have edges with more than one vertex, such graph can be traversed in more than one way due
    /// to the fact that AdjacentVertices are NOT ORDERED
    /// </summary>
    public sealed class DepthFirstTraversalTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void Given_SparseGraph_When_HasNoNodes_Then_ReturnEmpty(int startAt)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Directed().Sparse());

            // Act
            var result = sut.Traverse(o => o.DepthFirst(), startAt);

            // Assert
            Assert.Empty(result);
        }  
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void Given_WellConnectedGraph_When_HasNoNodes_Then_ReturnEmpty(int startAt)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Directed().WellConnected());

            // Act
            var result = sut.Traverse(o => o.DepthFirst(), startAt);

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(DagTestCases))]
        public void Given_Graph_When_GraphIsDag_And_BasedOnAdjacencySet_Then_TraverseInExpectedOrder(EdgeDescriptor<int>[] edges, int startAt, int[] expectedSequence)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Directed().Sparse()).AddToGraph(edges);

            // Act
            var result = sut.Traverse(o => o.DepthFirst(), startAt).ToList();

            // Assert
            Assert.Equal(expectedSequence, result);
        }

        [Theory]
        [MemberData(nameof(DagTestCases))]
        public void Given_Graph_When_GraphIsDag_And_BasedOnAdjacencyMatrix_Then_TraverseInExpectedOrder(EdgeDescriptor<int>[] edges, int startAt, int[] expectedSequence)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Directed().WellConnected()).AddToGraph(edges);

            // Act
            var result = sut.Traverse(o => o.DepthFirst(), startAt);

            // Assert
            Assert.Equal(expectedSequence, result);
        }

        [Theory]
        [MemberData(nameof(UndirectedTestCasesForAdjacencySetBasedGraph))]
        public void Given_Graph_When_GraphIsUndirected_And_BasedOnAdjacencySet_Then_TraverseInExpectedOrder(EdgeDescriptor<int>[] edges, int startAt, int[] expectedSequence)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Undirected().Sparse()).AddToGraph(edges);

            // Act
            var result = sut.Traverse(o => o.DepthFirst(), startAt);

            // Assert
            Assert.Equal(expectedSequence, result);
        }

        [Theory]
        [MemberData(nameof(UndirectedTestCasesForAdjacencyMatrixBasedGraph))]
        public void Given_Graph_When_GraphIsUndirected_And_BasedOnAdjacencyMatrix_Then_TraverseInExpectedOrder(EdgeDescriptor<int>[] edges, int startAt, int[] expectedSequence)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Undirected().WellConnected()).AddToGraph(edges);

            // Act
            var result = sut.Traverse(o => o.DepthFirst(), startAt);

            // Assert
            Assert.Equal(expectedSequence, result);
        }

        public static IEnumerable<object[]> DagTestCases =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0, new int[0] },

                new object[] { Graphs.CP3_417_DAG, 0, new[] { 0, 3, 4, 2, 1 } },
                new object[] { Graphs.CP3_417_DAG, 1, new[] { 1, 4, 3 } },
                new object[] { Graphs.CP3_417_DAG, 2, new[] { 2, 4 } },
                new object[] { Graphs.CP3_417_DAG, 3, new[] { 3, 4 } },
                new object[] { Graphs.CP3_417_DAG, 4, new[] { 4 } },

                new object[] { Graphs.CP3_44_DAG, 0, new[] { 0, 2, 5, 3, 4, 1 } },
                new object[] { Graphs.CP3_44_DAG, 1, new[] { 1, 3, 4, 2, 5 } },
                new object[] { Graphs.CP3_44_DAG, 2, new[] { 2, 5, 3, 4 } },
                new object[] { Graphs.CP3_44_DAG, 3, new[] { 3, 4 } },
                new object[] { Graphs.CP3_44_DAG, 4, new[] { 4 } },
                new object[] { Graphs.CP3_44_DAG, 5, new[] { 5 } },
                new object[] { Graphs.CP3_44_DAG, 6, new[] { 6 } },
                new object[] { Graphs.CP3_44_DAG, 7, new[] { 7, 6 } },

                new object[] { Graphs.CP3_418_DAG_Bipartite, 0, new[] { 0, 2, 3, 4, 1 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 1, new[] { 1, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 2, new[] { 2, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 3, new[] { 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 4, new[] { 4 } },

                new object[] { Graphs.CP_49, 0, new[] { 0, 1, 3, 4, 5, 7, 6, 2 } },
                new object[] { Graphs.CP_49, 1, new[] { 1, 3, 4, 5, 7, 6, 2 } },
                new object[] { Graphs.CP_49, 2, new[] { 2, 1, 3, 4, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 3, new[] { 3, 4, 5, 7, 6, 2, 1 } },
                new object[] { Graphs.CP_49, 4, new[] { 4, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 5, new[] { 5, 7, 6, 4 } },
                new object[] { Graphs.CP_49, 6, new[] { 6, 4, 5, 7 } },
                new object[] { Graphs.CP_49, 7, new[] { 7, 6, 4, 5 } },

                new object[] { Graphs.CP_419_Bipartite, 0, new[] { 0, 4, 1, 2, 3 } },
                new object[] { Graphs.CP_419_Bipartite, 1, new[] { 1, 2, 3 } },
                new object[] { Graphs.CP_419_Bipartite, 2, new[] { 2, 3, 1 } },
                new object[] { Graphs.CP_419_Bipartite, 3, new[] { 3 } },
                new object[] { Graphs.CP_419_Bipartite, 4, new[] { 4 } }
            };
      
        public static IEnumerable<object[]> UndirectedTestCasesForAdjacencySetBasedGraph =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0, new int[0] },

                new object[] { Graphs.CP_41, 0, new[] { 0, 1, 3, 4, 2 } },
                new object[] { Graphs.CP_41, 1, new[] { 1, 3, 4, 2, 0 } },
                new object[] { Graphs.CP_41, 2, new[] { 2, 3, 4, 1, 0 } },
                new object[] { Graphs.CP_41, 3, new[] { 3, 4, 2, 1, 0 } },
                new object[] { Graphs.CP_41, 4, new[] { 4, 3, 2, 1, 0 } },
                new object[] { Graphs.CP_41, 5, new[] { 5 } },
                new object[] { Graphs.CP_41, 6, new[] { 6, 8, 7 } },
                new object[] { Graphs.CP_41, 7, new[] { 7, 6, 8 } },
                new object[] { Graphs.CP_41, 8, new[] { 8, 6, 7 } },

                new object[] { Graphs.CP_43, 0, new[] { 0, 4, 8, 9, 10, 11, 12, 7, 3, 2, 6, 5, 1 } },
                new object[] { Graphs.CP_43, 1, new[] { 1, 5, 10, 11, 12, 7, 3, 2, 6, 9, 8, 4, 0 } },
                new object[] { Graphs.CP_43, 2, new[] { 2, 6, 11, 12, 7, 3, 10, 9, 8, 4, 0, 1, 5 } },
                new object[] { Graphs.CP_43, 3, new[] { 3, 7, 12, 11, 10, 9, 8, 4, 0, 1, 5, 6, 2 } },
                new object[] { Graphs.CP_43, 4, new[] { 4, 8, 9, 10, 11, 12, 7, 3, 2, 6, 5, 1, 0 } },
                new object[] { Graphs.CP_43, 5, new[] { 5, 10, 11, 12, 7, 3, 2, 6, 1, 0, 4, 8, 9 } },
                new object[] { Graphs.CP_43, 6, new[] { 6, 11, 12, 7, 3, 2, 1, 5, 10, 9, 8, 4, 0 } },
                new object[] { Graphs.CP_43, 7, new[] { 7, 12, 11, 10, 9, 8, 4, 0, 1, 5, 6, 2, 3 } },
                new object[] { Graphs.CP_43, 8, new[] { 8, 9, 10, 11, 12, 7, 3, 2, 6, 5, 1, 0, 4 } },
                new object[] { Graphs.CP_43, 9, new[] { 9, 10, 11, 12, 7, 3, 2, 6, 5, 1, 0, 4, 8 } },
                new object[] { Graphs.CP_43, 10, new[] { 10, 11, 12, 7, 3, 2, 6, 5, 1, 0, 4, 8, 9 } },
                new object[] { Graphs.CP_43, 11, new[] { 11, 12, 7, 3, 2, 6, 5, 10, 9, 8, 4, 0, 1 } },
                new object[] { Graphs.CP_43, 12, new[] { 12, 11, 10, 9, 8, 4, 0, 1, 5, 6, 2, 3, 7 } }
            };
      
        public static IEnumerable<object[]> UndirectedTestCasesForAdjacencyMatrixBasedGraph =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0, new int[0] },

                new object[] { Graphs.CP_41, 0, new[] { 0, 1, 3, 4, 2 } },
                new object[] { Graphs.CP_41, 1, new[] { 1, 3, 4, 2, 0 } },
                new object[] { Graphs.CP_41, 2, new[] { 2, 3, 4, 1, 0 } },
                new object[] { Graphs.CP_41, 3, new[] { 3, 4, 2, 1, 0 } },
                new object[] { Graphs.CP_41, 4, new[] { 4, 3, 2, 1, 0 } },
                new object[] { Graphs.CP_41, 5, new[] { 5 } },
                new object[] { Graphs.CP_41, 6, new[] { 6, 8, 7 } },
                new object[] { Graphs.CP_41, 7, new[] { 7, 6, 8 } },
                new object[] { Graphs.CP_41, 8, new[] { 8, 6, 7 } },

                new object[] { Graphs.CP_43, 0, new[] { 0, 4, 8, 9, 10, 11, 12, 7, 3, 2, 6, 5, 1 } },
                new object[] { Graphs.CP_43, 1, new[] { 1, 5, 10, 9, 8, 4, 0, 11, 12, 7, 3, 2, 6 } },
                new object[] { Graphs.CP_43, 2, new[] { 2, 6, 11, 12, 7, 3, 10, 9, 8, 4, 0, 1, 5 } },
                new object[] { Graphs.CP_43, 3, new[] { 3, 7, 12, 11, 10, 9, 8, 4, 0, 1, 5, 6, 2 } },
                new object[] { Graphs.CP_43, 4, new[] { 4, 8, 9, 10, 11, 12, 7, 3, 2, 6, 5, 1, 0 } },
                new object[] { Graphs.CP_43, 5, new[] { 5, 10, 9, 8, 4, 0, 1, 2, 6, 11, 12, 7, 3 } },
                new object[] { Graphs.CP_43, 6, new[] { 6, 11, 12, 7, 3, 2, 1, 5, 10, 9, 8, 4, 0 } },
                new object[] { Graphs.CP_43, 7, new[] { 7, 12, 11, 10, 9, 8, 4, 0, 1, 5, 6, 2, 3 } },
                new object[] { Graphs.CP_43, 8, new[] { 8, 9, 10, 11, 12, 7, 3, 2, 6, 5, 1, 0, 4 } },
                new object[] { Graphs.CP_43, 9, new[] { 9, 10, 11, 12, 7, 3, 2, 6, 5, 1, 0, 4, 8 } },
                new object[] { Graphs.CP_43, 10, new[] { 10, 9, 8, 4, 0, 1, 5, 6, 11, 12, 7, 3, 2 } },
                new object[] { Graphs.CP_43, 11, new[] { 11, 12, 7, 3, 2, 6, 5, 10, 9, 8, 4, 0, 1 } },
                new object[] { Graphs.CP_43, 12, new[] { 12, 11, 10, 9, 8, 4, 0, 1, 5, 6, 2, 3, 7 } }
            };
    }
}