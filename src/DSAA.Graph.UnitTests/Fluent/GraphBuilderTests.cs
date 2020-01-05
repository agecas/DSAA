using System;
using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.Fluent;
using DSAA.Shared;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Graph.UnitTests.Fluent
{
    public sealed class GraphBuilderTests
    {
        public static IEnumerable<object[]> GraphBuilders =>
            new List<object[]>
            {
                new object[]
                {
                    Builder.From(b => b.Directed().WellConnected()),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Directed,
                    EqualityComparer<int>.Default.GetType()
                },
                new object[]
                {
                    Builder.From(b => b.Directed().Sparse()),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Directed,
                    EqualityComparer<int>.Default.GetType()
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().WellConnected()),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Undirected,
                    EqualityComparer<int>.Default.GetType()
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().Sparse()),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Undirected,
                    EqualityComparer<int>.Default.GetType()
                },

                new object[]
                {
                    Builder.From(b => b.Directed().WellConnected().CompareUsing<IntComparer>()),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Directed,
                    typeof(IntComparer)
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().WellConnected().CompareUsing<IntComparer>()),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Undirected,
                    typeof(IntComparer)
                },
                new object[]
                {
                    Builder.From(b => b.Directed().Sparse().CompareUsing<IntComparer>()),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Directed,
                    typeof(IntComparer)
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().Sparse().CompareUsing<IntComparer>()),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Undirected,
                    typeof(IntComparer)
                },

                new object[]
                {
                    Builder.From(b => b.Directed().WellConnected().CompareUsing(new IntComparer())),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Directed,
                    typeof(IntComparer)
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().WellConnected().CompareUsing(new IntComparer())),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Undirected,
                    typeof(IntComparer)
                },
                new object[]
                {
                    Builder.From(b => b.Directed().Sparse().CompareUsing(new IntComparer())),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Directed,
                    typeof(IntComparer)
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().Sparse().CompareUsing(new IntComparer())),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Undirected,
                    typeof(IntComparer)
                },

                new object[]
                {
                    Builder.From(b => b.Directed().WellConnected().CompareUsing((x, y) => x == y)),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Directed,
                    typeof(LambdaEqualityComparer<int>)
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().WellConnected().CompareUsing((x, y) => x == y)),
                    typeof(AdjacencyMatrixGraph<int>),
                    GraphType.Undirected,
                    typeof(LambdaEqualityComparer<int>)
                },
                new object[]
                {
                    Builder.From(b => b.Directed().Sparse().CompareUsing((x, y) => x == y)),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Directed,
                    typeof(LambdaEqualityComparer<int>)
                },
                new object[]
                {
                    Builder.From(b => b.Undirected().Sparse().CompareUsing((x, y) => x == y)),
                    typeof(AdjacencySetGraph<int>),
                    GraphType.Undirected,
                    typeof(LambdaEqualityComparer<int>)
                }
            };

        [Theory]
        [MemberData(nameof(GraphBuilders))]
        public void Given_GraphBuilder_When_GraphConfigured_Then_ReturnGraph(Builder builder, Type expectedGraph,
            GraphType expectedType, Type expectedComparer)
        {
            // Arrange
            var result = GraphBuilder.Create(builder.Factory);

            // Act
            // Assert
            Assert.IsType(expectedGraph, result);
            Assert.Equal(expectedType, result.Type);
            Assert.IsType(expectedComparer, result.Comparer);
        }

        public sealed class Builder
        {
            public Func<ISetGraphDirection<int>, IBuildGraph<int>> Factory { get; private set; }

            public static Builder From(Func<ISetGraphDirection<int>, IBuildGraph<int>> builder)
            {
                return new Builder
                {
                    Factory = builder
                };
            }
        }
    }
}