using System.Collections.Generic;
using DSAA.Graph.Domain;

namespace DSAA.Graph
{
    public interface IGraph<T> : IReadOnlyGraph<T>
    {
        IEqualityComparer<T> Comparer { get; }
        GraphType Type { get; }
        IGraph<T> AddVertex(T vertex);
        IGraph<T> RemoveVertex(T vertex);
        IGraph<T> AddEdge(T source, T destination, int weight = 1);
        IGraph<T> RemoveEdge(T source, T destination);
    }
}