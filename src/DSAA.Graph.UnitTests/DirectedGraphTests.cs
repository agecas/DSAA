using System;
using DSAA.Graph.Domain;
using Xunit;

namespace DSAA.Graph.UnitTests
{
    public abstract class DirectedGraphTests : GraphTests
    {
        protected DirectedGraphTests(Func<GraphType, IGraph<int>> graphFactory) : base(() => graphFactory(GraphType.Directed))
        {
        }

        [Fact]
        public void Given_EmptyGraph_When_EdgeAdded_Then_GraphShouldContainOneEdge()
        {
            // Arrange
            // Act
            Sut.AddEdge(1, 2);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1), v => { v.AssertVertex(2, 1); });
        }

        [Fact]
        public void Given_EmptyGraph_When_DuplicateEdgeAdded_Then_GraphShouldContainOneEdge()
        {
            // Arrange
            // Act
            Sut.AddEdge(1, 2)
                .AddEdge(1, 2);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1), v => { v.AssertVertex(2, 1); });
        }

        [Fact]
        public void Given_Graph_When_EdgeWithNoVerticesQueried_Then_ReturnEmpty()
        {
            // Arrange
            // Act
            var result = Sut.AddEdge(1, 2).GetAdjacentVertices(2);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Given_Graph_When_MultipleEdgesAdded_Then_ReturnCorrectVerticesForAllEdges()
        {
            // Arrange
            // Act
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3)
                .AddEdge(3, 5)
                .AddEdge(5, 2)
                .AddEdge(5, 4)
                .AddEdge(2, 4);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(3, 1));

            Assert.Collection(Sut.GetAdjacentVertices(2),
                v => v.AssertVertex(4, 1));

            Assert.Collection(Sut.GetAdjacentVertices(3),
                v => v.AssertVertex(5, 1));

            Assert.Empty(Sut.GetAdjacentVertices(4));

            Assert.Collection(Sut.GetAdjacentVertices(5),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(4, 1));
        }

        [Fact]
        public void Given_Graph_When_MultipleWeightedEdgesAdded_Then_ReturnCorrectVerticesForAllEdges()
        {
            // Arrange
            // Act
            Sut.AddEdge(1, 2, 5)
                .AddEdge(1, 3, 10)
                .AddEdge(3, 5, 20)
                .AddEdge(5, 2, 30)
                .AddEdge(5, 4, 35)
                .AddEdge(2, 4, 15);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(2, 5),
                v => v.AssertVertex(3, 10));

            Assert.Collection(Sut.GetAdjacentVertices(2),
                v => v.AssertVertex(4, 15));

            Assert.Collection(Sut.GetAdjacentVertices(3),
                v => v.AssertVertex(5, 20));

            Assert.Empty(Sut.GetAdjacentVertices(4));

            Assert.Collection(Sut.GetAdjacentVertices(5),
                v => v.AssertVertex(2, 30),
                v => v.AssertVertex(4, 35));
        }

        [Fact]
        public void Given_Graph_When_RemovingNonExistingVertex_Then_ReturnUnchangedGraph()
        {
            // Arrange
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3)
                .AddEdge(3, 5)
                .AddEdge(5, 2)
                .AddEdge(5, 4)
                .AddEdge(2, 4);

            // Act
            Sut.RemoveVertex(100);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(3, 1));

            Assert.Collection(Sut.GetAdjacentVertices(2),
                v => v.AssertVertex(4, 1));

            Assert.Collection(Sut.GetAdjacentVertices(3),
                v => v.AssertVertex(5, 1));

            Assert.Empty(Sut.GetAdjacentVertices(4));

            Assert.Collection(Sut.GetAdjacentVertices(5),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(4, 1));
        }

        [Fact]
        public void Given_Graph_When_VertexWithNoEdgesAdded_Then_ReturnCorrectVerticesForAllEdges()
        {
            // Arrange
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3);

            // Act
            Sut.AddVertex(100);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(3, 1));

            Assert.True(Sut.Contains(100));
            Assert.Empty(Sut.GetAdjacentVertices(100));
        }

        [Fact]
        public void Given_Graph_When_VertexRemoved_Then_ReturnCorrectVerticesForAllEdges()
        {
            // Arrange
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3)
                .AddEdge(3, 5)
                .AddEdge(5, 2)
                .AddEdge(5, 4)
                .AddEdge(2, 4);

            // Act
            Sut.RemoveVertex(2);

            // Assert
            Assert.False(Sut.Contains(2));

            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(3, 1));

            Assert.Collection(Sut.GetAdjacentVertices(3),
                v => v.AssertVertex(5, 1));

            Assert.Empty(Sut.GetAdjacentVertices(4));

            Assert.Collection(Sut.GetAdjacentVertices(5),
                v => v.AssertVertex(4, 1));
        }

        [Fact]
        public void Given_Graph_When_LastVertexRemoved_Then_ReturnCorrectVerticesForAllEdges()
        {
            // Arrange
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3)
                .AddEdge(3, 5)
                .AddEdge(5, 2)
                .AddEdge(5, 4)
                .AddEdge(2, 4);

            // Act
            Sut.RemoveVertex(4);

            // Assert
            Assert.False(Sut.Contains(4));

            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(3, 1));

            Assert.Empty(Sut.GetAdjacentVertices(2));

            Assert.Collection(Sut.GetAdjacentVertices(3),
                v => v.AssertVertex(5, 1));

            Assert.Collection(Sut.GetAdjacentVertices(5),
                v => v.AssertVertex(2, 1));
        }

        [Fact]
        public void Given_Graph_When_RemovingEdge_Then_ReturnAmendedGraph()
        {
            // Arrange
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3);

            // Act
            Sut.RemoveEdge(1, 2);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(3, 1));

            Assert.Empty(Sut.GetAdjacentVertices(2));
            Assert.Empty(Sut.GetAdjacentVertices(3));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(100)]
        public void Given_Graph_When_RemovingNonExistingEdge_Then_ReturnUnchangedGraph(int edgeToRemove)
        {
            // Arrange
            Sut.AddEdge(1, 2)
                .AddEdge(1, 3);

            // Act
            Sut.RemoveEdge(2, edgeToRemove);

            // Assert
            Assert.Collection(Sut.GetAdjacentVertices(1),
                v => v.AssertVertex(2, 1),
                v => v.AssertVertex(3, 1));

            Assert.Empty(Sut.GetAdjacentVertices(2));
            Assert.Empty(Sut.GetAdjacentVertices(3));
        }
    }
}