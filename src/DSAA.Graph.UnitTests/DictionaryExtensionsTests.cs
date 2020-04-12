using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DSAA.Graph.UnitTests
{
    public sealed class DictionaryExtensionsTests
    {
        [Fact]
        public void Given_DictionaryWithIEnumerableValues_When_ConvertedToGraph_Then_ReturnCorrectlyConfiguredGraph()
        {
            // Arrange
            var data = new Dictionary<string, IEnumerable<string>>
            {
                { "EUR", new [] { "GBP", "USD" } },
                { "USD", new List<string> { "GBP", "EUR" } }
            };

            var sut = data.ToGraph(b => b.Directed().WellConnected());

            // Act
            var result = sut.ToList();

            // Assert
            Assert.Collection(result, k =>
            {
                Assert.Equal("EUR", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("GBP", v.Value);
                }, v =>
                {
                    Assert.Equal("USD", v.Value);
                });
            }, k =>
            {
                Assert.Equal("GBP", k);
                Assert.Empty(sut.GetAdjacentVertices(k));
            }, k =>
            {
                Assert.Equal("USD", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("EUR", v.Value);
                }, v =>
                {
                    Assert.Equal("GBP", v.Value);
                });
            });
        }

        [Fact]
        public void Given_DictionaryWithListValues_When_ConvertedToGraph_Then_ReturnCorrectlyConfiguredGraph()
        {
            // Arrange
            var data = new Dictionary<string, List<string>>
            {
                { "EUR", new List<string> { "GBP", "USD" } },
                { "USD", new List<string> { "GBP", "EUR" } }
            };

            var sut = data.ToGraph(b => b.Directed().WellConnected());

            // Act
            var result = sut.ToList();

            // Assert
            Assert.Collection(result, k =>
            {
                Assert.Equal("EUR", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("GBP", v.Value);
                }, v =>
                {
                    Assert.Equal("USD", v.Value);
                });
            }, k =>
            {
                Assert.Equal("GBP", k);
                Assert.Empty(sut.GetAdjacentVertices(k));
            }, k =>
            {
                Assert.Equal("USD", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("EUR", v.Value);
                }, v =>
                {
                    Assert.Equal("GBP", v.Value);
                });
            });
        }

        [Fact]
        public void Given_DictionaryWithHashSetValues_When_ConvertedToGraph_Then_ReturnCorrectlyConfiguredGraph()
        {
            // Arrange
            var data = new Dictionary<string, HashSet<string>>
            {
                { "EUR", new HashSet<string> { "GBP", "USD" } },
                { "USD", new HashSet<string> { "GBP", "EUR" } }
            };

            var sut = data.ToGraph(b => b.Directed().WellConnected());

            // Act
            var result = sut.ToList();

            // Assert
            Assert.Collection(result, k =>
            {
                Assert.Equal("EUR", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("GBP", v.Value);
                }, v =>
                {
                    Assert.Equal("USD", v.Value);
                });
            }, k =>
            {
                Assert.Equal("GBP", k);
                Assert.Empty(sut.GetAdjacentVertices(k));
            }, k =>
            {
                Assert.Equal("USD", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("EUR", v.Value);
                }, v =>
                {
                    Assert.Equal("GBP", v.Value);
                });
            });
        }

        [Fact]
        public void Given_DictionaryWithDuplicateValues_When_ConvertedToGraph_Then_ReturnCorrectlyConfiguredGraph()
        {
            // Arrange
            var data = new Dictionary<string, List<string>>
            {
                { "EUR", new List<string> { "GBP", "USD", "USD", "GBP" } },
                { "USD", new List<string> { "GBP", "EUR", "GBP", "EUR", "EUR" } }
            };

            var sut = data.ToGraph(b => b.Directed().WellConnected());

            // Act
            var result = sut.ToList();

            // Assert
            Assert.Collection(result, k =>
            {
                Assert.Equal("EUR", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("GBP", v.Value);
                }, v =>
                {
                    Assert.Equal("USD", v.Value);
                });
            }, k =>
            {
                Assert.Equal("GBP", k);
                Assert.Empty(sut.GetAdjacentVertices(k));
            }, k =>
            {
                Assert.Equal("USD", k);
                Assert.Collection(sut.GetAdjacentVertices(k), v =>
                {
                    Assert.Equal("EUR", v.Value);
                }, v =>
                {
                    Assert.Equal("GBP", v.Value);
                });
            });
        }
    }
}
