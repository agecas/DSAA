using System.Collections.Generic;
using DSAA.Graph.Domain;

namespace DSAA.Graph
{
    public interface IReadOnlyGraph<T> : IEnumerable<T>
    {
        bool Contains(T edge);
        int Count { get; }
        IReadOnlyList<WeightedEdgeDestination<T>> GetAdjacentVertices(T source);
    }
}