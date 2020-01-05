using DSAA.Graph.Domain;
using Xunit;

namespace DSAA.Graph.UnitTests
{
    internal static class VertexExtensions
    {
        internal static void AssertVertex<T>(this WeightedEdgeDestination<T> actual, T expectedValue, int expectedWeight)
        {
            Assert.Equal(expectedValue, actual.Value);
            Assert.Equal(expectedWeight, actual.Weight);
        }
    }
}