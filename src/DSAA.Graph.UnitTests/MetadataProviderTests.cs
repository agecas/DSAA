using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.UnitTests.Helpers;
using Xunit;

namespace DSAA.Graph.UnitTests
{
    public sealed class MetadataProviderTests
    {
        public MetadataProvider Sut => new MetadataProvider();

        public static IEnumerable<object[]> MetadataTests =>
            new List<object[]>
            {
                // Directed
                new object[] {GraphType.Directed, Graphs.CP_41, 9, 14, true, false, false},
                new object[] {GraphType.Directed, Graphs.CP_43, 13, 32, false, false, false},
                new object[] {GraphType.Directed, Graphs.CP3_44_DAG, 8, 8, true, false, false},
                new object[] {GraphType.Directed, Graphs.CP_49, 8, 9, false, false, false},
                new object[] {GraphType.Directed, Graphs.CP3_417_DAG, 5, 7, false, false, false},
                new object[] {GraphType.Directed, Graphs.CP3_418_DAG_Bipartite, 5, 5, false, false, false},
                new object[] {GraphType.Directed, Graphs.CP_419_Bipartite, 5, 5, false, false, false},
                new object[] {GraphType.Directed, SpanningTree.Graphs.CP_410, 5, 14, false, true, false},
                new object[] {GraphType.Directed, SpanningTree.Graphs.CP_414, 5, 14, false, true, false},
                new object[] {GraphType.Directed, SpanningTree.Graphs.K5, 5, 20, false, true, false},
                new object[] {GraphType.Directed, SpanningTree.Graphs.Rail, 10, 26, false, true, false},
                new object[] {GraphType.Directed, SpanningTree.Graphs.Tessellation, 9, 32, false, true, false},
                new object[] {GraphType.Directed, ShortestPath.Graphs.CP3_43_UU, 13, 32, false, false, false},
                new object[] {GraphType.Directed, ShortestPath.Graphs.CP3_44_DU, 8, 8, true, false, false},
                new object[] {GraphType.Directed, ShortestPath.Graphs.CP3_417_DW, 5, 7, false, true, false},
                new object[] {GraphType.Directed, ShortestPath.Graphs.CP3_418_ve_weight, 5, 5, false, true, true},
                new object[] {GraphType.Directed, ShortestPath.Graphs.CP3_419_ve_cycle, 5, 5, false, true, true},
                new object[] {GraphType.Directed, ShortestPath.Graphs.CP3_440_Tree, 6, 10, false, true, false},
                new object[] {GraphType.Directed, ShortestPath.Graphs.BellmanFordsKiller, 7, 6, false, true, false},
                new object[] {GraphType.Directed, ShortestPath.Graphs.DijkstrasKiller, 11, 15, false, true, true},
                new object[] {GraphType.Directed, ShortestPath.Graphs.Dag, 6, 8, false, true, false},

                // Undirected
                new object[] {GraphType.Undirected, Graphs.CP_41, 9, 7, true, false, false},
                new object[] {GraphType.Undirected, Graphs.CP_43, 13, 16, false, false, false},
                new object[] {GraphType.Undirected, Graphs.CP3_44_DAG, 8, 8, true, false, false},
                new object[] {GraphType.Undirected, Graphs.CP_49, 8, 9, false, false, false},
                new object[] {GraphType.Undirected, Graphs.CP3_417_DAG, 5, 7, false, false, false},
                new object[] {GraphType.Undirected, Graphs.CP3_418_DAG_Bipartite, 5, 5, false, false, false},
                new object[] {GraphType.Undirected, Graphs.CP_419_Bipartite, 5, 4, false, false, false},
                new object[] {GraphType.Undirected, SpanningTree.Graphs.CP_410, 5, 7, false, true, false},
                new object[] {GraphType.Undirected, SpanningTree.Graphs.CP_414, 5, 7, false, true, false},
                new object[] {GraphType.Undirected, SpanningTree.Graphs.K5, 5, 10, false, true, false},
                new object[] {GraphType.Undirected, SpanningTree.Graphs.Rail, 10, 13, false, true, false},
                new object[] {GraphType.Undirected, SpanningTree.Graphs.Tessellation, 9, 16, false, true, false},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.CP3_43_UU, 13, 16, false, false, false},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.CP3_44_DU, 8, 8, true, false, false},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.CP3_417_DW, 5, 7, false, true, false},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.CP3_418_ve_weight, 5, 5, false, true, true},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.CP3_419_ve_cycle, 5, 4, false, true, true},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.CP3_440_Tree, 6, 5, false, true, false},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.BellmanFordsKiller, 7, 6, false, true, false},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.DijkstrasKiller, 11, 15, false, true, true},
                new object[] {GraphType.Undirected, ShortestPath.Graphs.Dag, 6, 8, false, true, false}
            };

        [Theory]
        [InlineData(GraphType.Undirected)]
        [InlineData(GraphType.Directed)]
        public void Given_Graph_When_Empty_Then_ReturnEmptyMetadata(GraphType type)
        {
            // Arrange
            var graph = new AdjacencySetGraph<int>(type);

            // Act
            var result = Sut.GetMetadata(graph);

            // Assert
            Assert.Equal(type, result.Type);
            Assert.Equal(0, result.VerticesCount);
            Assert.Equal(0, result.EdgeCount);
            Assert.False(result.Connected);
            Assert.False(result.HasNegativeWeights);
        }

        [Theory]
        [MemberData(nameof(MetadataTests))]
        public void Given_DirectedGraph_When_HaveVerticesAndEdgesWithVaryingWeights_Then_ReturnCorrectMetadata(
            GraphType type, EdgeDescriptor<int>[] edges, int vertexCount, int edgeCount, bool disconnected,
            bool weighted, bool negativeWeights)
        {
            // Arrange
            var graph = new AdjacencySetGraph<int>(type);
            graph.AddToGraph(edges);

            // Act
            var result = Sut.GetMetadata(graph);

            // Assert
            Assert.Equal(type, result.Type);
            Assert.Equal(vertexCount, result.VerticesCount);
            Assert.Equal(edgeCount, result.EdgeCount);
            Assert.Equal(disconnected, result.Disconnected);
            Assert.Equal(weighted, result.Weighted);
            Assert.Equal(negativeWeights, result.HasNegativeWeights);
        }
    }
}