using System.Linq;
using DSAA.Graph.Domain;

namespace DSAA.Graph.UnitTests.Helpers
{
    public sealed class EdgeDescriptor<T>
    {
        public EdgeDescriptor(T edge)
        {
            Edge = edge;
            Vertices = new WeightedEdgeDestination<T>[0];
        }

        public EdgeDescriptor(T edge,params T[] vertices)
        {
            Edge = edge;
            Vertices = vertices.Select(v => new WeightedEdgeDestination<T>(v, 1)).ToArray();
        }

        public EdgeDescriptor(T edge, params (T Destination, int Weight)[] weightedVertices)
        {
            Edge = edge;
            Vertices = weightedVertices.Select(v => new WeightedEdgeDestination<T>(v.Destination, v.Weight)).ToArray();
        }

        public T Edge { get; }
        public WeightedEdgeDestination<T>[] Vertices { get; }

        public override string ToString()
        {
            return $"{Edge} => {string.Join(", ", Vertices.AsEnumerable())}";
        }
    }
}