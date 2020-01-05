using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Fluent;
using DSAA.Graph.Sort;
using DSAA.Graph.UnitTests.Helpers;
using Xunit;

namespace DSAA.Graph.UnitTests.Sort.Strategy
{
    public abstract class KahnTopologicalSortTests
    {
        protected IGraph<int> Graph { get; }

        protected KahnTopologicalSortTests(Func<ISetGraphDensity<int>, IBuildGraph<int>> graphFactory)
        {
            var partialBuilder = new GraphBuilder<int>().Directed();
            Graph = graphFactory(partialBuilder).Build();
        }

        [Fact]
        public void Given_EmptyGraph_When_Sorted_Then_ReturnEmptyList()
        {
            // Arrange
            var graph = GraphBuilder.Create<int>(b => b.Directed().Sparse());

            // Act
            var result = graph.Sort(o => o.UseTopologicalSort()).Single();

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(CyclicTests))]
        public void Given_CyclicGraph_When_Sorted_Then_ReturnNone(EdgeDescriptor<int>[] edges)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Directed().WellConnected()).AddToGraph(edges);

            // Act
            var result = sut.Sort(o => o.UseTopologicalSort());

            // Assert
            Assert.True(result.IsEmpty);
        }

        [Theory]
        [MemberData(nameof(DagTests))]
        public void Given_DAG_When_Sorted_Then_ReturnVerticesInTopologicalOrder(EdgeDescriptor<int>[] edges, int [] expectedSequence)
        {
            // Arrange
            var sut = GraphBuilder.Create<int>(b => b.Directed().WellConnected()).AddToGraph(edges);

            // Act
            var result = sut.Sort(o => o.UseTopologicalSort()).Single();

            // Assert
            Assert.Equal(expectedSequence, result);
        }

        public static IEnumerable<object[]> CyclicTests =>
           new List<object[]>
           {
                new object[] { Graphs.CP_41 },
                new object[] { Graphs.CP_43 },
                new object[] { Graphs.CP_49 },
                new object[] { Graphs.CP_419_Bipartite }
           };

        public static IEnumerable<object[]> DagTests =>
           new List<object[]>
           {
                new object[] { Graphs.CP3_44_DAG, new[] { 0, 7, 1, 6, 2, 3, 5, 4 } },
                new object[] { Graphs.CP3_417_DAG, new[] { 0, 1, 2, 3, 4 } },
                new object[] { Graphs.CP3_418_DAG_Bipartite, new[] { 0, 1, 2, 3, 4 } },
           };
    }
}
