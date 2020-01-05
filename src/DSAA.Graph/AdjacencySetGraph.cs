using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DSAA.Graph.Domain;

namespace DSAA.Graph
{
    public sealed class AdjacencySetGraph<T> : IGraph<T>
    {
        private readonly IDictionary<T, HashSet<WeightedEdgeDestination<T>>> _vertexToEdgesMap;

        public AdjacencySetGraph(GraphType graphType) : this(graphType, EqualityComparer<T>.Default)
        {
        }

        public AdjacencySetGraph(GraphType graphType, IEqualityComparer<T> comparer)
        {
            if (!Enum.IsDefined(typeof(GraphType), graphType))
                throw new InvalidEnumArgumentException(nameof(graphType), (int) graphType, typeof(GraphType));
            Type = graphType;
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _vertexToEdgesMap = new Dictionary<T, HashSet<WeightedEdgeDestination<T>>>(comparer);
        }

        public IEqualityComparer<T> Comparer { get; }

        public bool Contains(T edge)
        {
            return _vertexToEdgesMap.ContainsKey(edge);
        }

        public int Count => _vertexToEdgesMap.Count;

        public GraphType Type { get; }

        public IGraph<T> AddVertex(T vertex)
        {
            AddNewVertexIfNew(vertex);
            return this;
        }

        public IGraph<T> RemoveVertex(T vertex)
        {
            if (_vertexToEdgesMap.ContainsKey(vertex) == false) return this;

            _vertexToEdgesMap.Remove(vertex);

            foreach (var set in _vertexToEdgesMap.Values) set.Remove(ToVertex(vertex));

            return this;
        }

        public IGraph<T> AddEdge(T source, T destination, int weight = 1)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            var sourceVertices = GetOrAddVerticesForEdge(source);

            if (Comparer.Equals(source, destination) == false)
            {
                sourceVertices.Add(ToVertex(destination, weight));
                var destinationVertices = GetOrAddVerticesForEdge(destination);

                if (Type == GraphType.Undirected) destinationVertices.Add(ToVertex(source, weight));
            }

            return this;
        }

        public IGraph<T> RemoveEdge(T source, T destination)
        {
            if (_vertexToEdgesMap.ContainsKey(source))
            {
                _vertexToEdgesMap[source].Remove(ToVertex(destination));

                if (Type == GraphType.Undirected && _vertexToEdgesMap.ContainsKey(destination))
                    _vertexToEdgesMap[destination].Remove(ToVertex(source));
            }

            return this;
        }

        public IReadOnlyList<WeightedEdgeDestination<T>> GetAdjacentVertices(T source)
        {
            return _vertexToEdgesMap.ContainsKey(source) ? _vertexToEdgesMap[source].ToList() : new List<WeightedEdgeDestination<T>>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _vertexToEdgesMap.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private HashSet<WeightedEdgeDestination<T>> GetOrAddVerticesForEdge(T source)
        {
            AddNewVertexIfNew(source);

            return _vertexToEdgesMap[source];
        }

        private void AddNewVertexIfNew(T source)
        {
            if (_vertexToEdgesMap.ContainsKey(source) == false)
                _vertexToEdgesMap[source] = new HashSet<WeightedEdgeDestination<T>>(new EdgeEqualityComparer<T>(Comparer));
        }

        private WeightedEdgeDestination<T> ToVertex(T value, int weight = 0)
        {
            return new WeightedEdgeDestination<T>(value, weight);
        }
    }
}