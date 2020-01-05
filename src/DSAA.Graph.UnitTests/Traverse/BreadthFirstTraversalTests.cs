using System.Collections.Generic;
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
    public sealed class BreadthFirstTraversalTests
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
            var result = sut.Traverse(o => o.BreadthFirst(), startAt);

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
            var result = sut.Traverse(o => o.BreadthFirst(), startAt);

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
            var result = sut.Traverse(o => o.BreadthFirst(), startAt);

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
            var result = sut.Traverse(o => o.BreadthFirst(), startAt);

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
            var result = sut.Traverse(o => o.BreadthFirst(), startAt);

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
            var result = sut.Traverse(o => o.BreadthFirst(), startAt);

            // Assert
            Assert.Equal(expectedSequence, result);
        }

        public static IEnumerable<object[]> DagTestCases =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0, new int[0] },

                new object[] { Graphs.CP3_417_DAG, 0, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP3_417_DAG, 1, new[] { 1, 3, 4 } },
                new object[] { Graphs.CP3_417_DAG, 2, new[] { 2, 4 } },
                new object[] { Graphs.CP3_417_DAG, 3, new[] { 3, 4 } },
                new object[] { Graphs.CP3_417_DAG, 4, new[] { 4 } },
                              
                new object[] { Graphs.CP3_44_DAG, 0, new[] { 0, 1, 2, 3, 5, 4 } },
                new object[] { Graphs.CP3_44_DAG, 1, new[] { 1, 2, 3, 5, 4 } },
                new object[] { Graphs.CP3_44_DAG, 2, new[] { 2, 3, 5, 4 } },
                new object[] { Graphs.CP3_44_DAG, 3, new[] { 3, 4 } },
                new object[] { Graphs.CP3_44_DAG, 4, new[] { 4 } },
                new object[] { Graphs.CP3_44_DAG, 5, new[] { 5 } },
                new object[] { Graphs.CP3_44_DAG, 6, new[] { 6 } },
                new object[] { Graphs.CP3_44_DAG, 7, new[] { 7, 6 } },
                               
                new object[] { Graphs.CP3_418_DAG_Bipartite, 0, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 1, new[] { 1, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 2, new[] { 2, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 3, new[] { 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, 4, new[] { 4 } },
                              
                new object[] { Graphs.CP_49, 0, new[] { 0, 1, 3, 2, 4, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 1, new[] { 1, 3, 2, 4, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 2, new[] { 2, 1, 3, 4, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 3, new[] { 3, 2, 4, 1, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 4, new[] { 4, 5, 7, 6 } },
                new object[] { Graphs.CP_49, 5, new[] { 5, 7, 6, 4 } },
                new object[] { Graphs.CP_49, 6, new[] { 6, 4, 5, 7 } },
                new object[] { Graphs.CP_49, 7, new[] { 7, 6, 4, 5 } },
                              
                new object[] { Graphs.CP_419_Bipartite, 0, new[] { 0, 1, 4, 2, 3 } },
                new object[] { Graphs.CP_419_Bipartite, 1, new[] { 1, 2, 3 } },
                new object[] { Graphs.CP_419_Bipartite, 2, new[] { 2, 1, 3 } },
                new object[] { Graphs.CP_419_Bipartite, 3, new[] { 3 } },
                new object[] { Graphs.CP_419_Bipartite, 4, new[] { 4 } }
            };
      
        public static IEnumerable<object[]> UndirectedTestCasesForAdjacencySetBasedGraph =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0, new int[0] },

                new object[] { Graphs.CP_41, 0, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP_41, 1, new[] { 1, 0, 2, 3, 4 } },
                new object[] { Graphs.CP_41, 2, new[] { 2, 1, 3, 0, 4 } },
                new object[] { Graphs.CP_41, 3, new[] { 3, 1, 2, 4, 0 } },
                new object[] { Graphs.CP_41, 4, new[] { 4, 3, 1, 2, 0 } },
                new object[] { Graphs.CP_41, 5, new[] { 5 } },
                new object[] { Graphs.CP_41, 6, new[] { 6, 7, 8 } },
                new object[] { Graphs.CP_41, 7, new[] { 7, 6, 8 } },
                new object[] { Graphs.CP_41, 8, new[] { 8, 6, 7 } },

                new object[] { Graphs.CP_43, 0, new[] { 0, 1, 4, 2, 5, 8, 3, 6, 10, 9, 7, 11, 12 } },
                new object[] { Graphs.CP_43, 1, new[] { 1, 0, 2, 5, 4, 3, 6, 10, 8, 7, 11, 9, 12 } },
                new object[] { Graphs.CP_43, 2, new[] { 2, 1, 3, 6, 0, 5, 7, 11, 4, 10, 12, 8, 9 } },
                new object[] { Graphs.CP_43, 3, new[] { 3, 2, 7, 1, 6, 12, 0, 5, 11, 4, 10, 8, 9 } },
                new object[] { Graphs.CP_43, 4, new[] { 4, 0, 8, 1, 9, 2, 5, 10, 3, 6, 11, 7, 12 } },
                new object[] { Graphs.CP_43, 5, new[] { 5, 1, 6, 10, 0, 2, 11, 9, 4, 3, 12, 8, 7 } },
                new object[] { Graphs.CP_43, 6, new[] { 6, 2, 5, 11, 1, 3, 10, 12, 0, 7, 9, 4, 8 } },
                new object[] { Graphs.CP_43, 7, new[] { 7, 3, 12, 2, 11, 1, 6, 10, 0, 5, 9, 4, 8 } },
                new object[] { Graphs.CP_43, 8, new[] { 8, 4, 9, 0, 10, 1, 5, 11, 2, 6, 12, 3, 7 } },
                new object[] { Graphs.CP_43, 9, new[] { 9, 8, 10, 4, 5, 11, 0, 1, 6, 12, 2, 7, 3 } },
                new object[] { Graphs.CP_43, 10, new[] { 10, 5, 9, 11, 1, 6, 8, 12, 0, 2, 4, 7, 3  } },
                new object[] { Graphs.CP_43, 11, new[] { 11, 6, 10, 12, 2, 5, 9, 7, 1, 3, 8, 0, 4 } },
                new object[] { Graphs.CP_43, 12, new[] { 12, 7, 11, 3, 6, 10, 2, 5, 9, 1, 8, 0, 4 } }
            };

        public static IEnumerable<object[]> UndirectedTestCasesForAdjacencyMatrixBasedGraph =>
            new List<object[]>
            {
                new object[] { new EdgeDescriptor<int>[] {}, 0, new int[0] },

                new object[] { Graphs.CP_41, 0, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP_41, 1, new[] { 1, 0, 2, 3, 4 } },
                new object[] { Graphs.CP_41, 2, new[] { 2, 1, 3, 0, 4 } },
                new object[] { Graphs.CP_41, 3, new[] { 3, 1, 2, 4, 0 } },
                new object[] { Graphs.CP_41, 4, new[] { 4, 3, 1, 2, 0 } },
                new object[] { Graphs.CP_41, 5, new[] { 5 } },
                new object[] { Graphs.CP_41, 6, new[] { 6, 7, 8 } },
                new object[] { Graphs.CP_41, 7, new[] { 7, 6, 8 } },
                new object[] { Graphs.CP_41, 8, new[] { 8, 6, 7 } },

                new object[] { Graphs.CP_43, 0, new[] { 0, 1, 4, 2, 5, 8, 3, 6, 10, 9, 7, 11, 12 } },
                new object[] { Graphs.CP_43, 1, new[] { 1, 0, 2, 5, 4, 3, 6, 10, 8, 7, 11, 9, 12 } },
                new object[] { Graphs.CP_43, 2, new[] { 2, 1, 3, 6, 0, 5, 7, 11, 4, 10, 12, 8, 9 } },
                new object[] { Graphs.CP_43, 3, new[] { 3, 2, 7, 1, 6, 12, 0, 5, 11, 4, 10, 8, 9 } },
                new object[] { Graphs.CP_43, 4, new[] { 4, 0, 8, 1, 9, 2, 5, 10, 3, 6, 11, 7, 12 } },
                new object[] { Graphs.CP_43, 5, new[] { 5, 1, 6, 10, 0, 2, 11, 9, 4, 3, 12, 8, 7 } },
                new object[] { Graphs.CP_43, 6, new[] { 6, 2, 5, 11, 1, 3, 10, 12, 0, 7, 9, 4, 8 } },
                new object[] { Graphs.CP_43, 7, new[] { 7, 3, 12, 2, 11, 1, 6, 10, 0, 5, 9, 4, 8 } },
                new object[] { Graphs.CP_43, 8, new[] { 8, 4, 9, 0, 10, 1, 5, 11, 2, 6, 12, 3, 7 } },
                new object[] { Graphs.CP_43, 9, new[] { 9, 8, 10, 4, 5, 11, 0, 1, 6, 12, 2, 7, 3 } },
                new object[] { Graphs.CP_43, 10, new[] { 10, 5, 11, 9, 1, 6, 12, 8, 0, 2, 7, 4, 3 } },
                new object[] { Graphs.CP_43, 11, new[] { 11, 6, 10, 12, 2, 5, 9, 7, 1, 3, 8, 0, 4 } },
                new object[] { Graphs.CP_43, 12, new[] { 12, 7, 11, 3, 6, 10, 2, 5, 9, 1, 8, 0, 4 } }
            };
    }
}