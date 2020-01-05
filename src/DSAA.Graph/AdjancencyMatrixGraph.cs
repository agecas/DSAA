using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DSAA.Graph.Domain;

namespace DSAA.Graph
{
    public sealed class AdjacencyMatrixGraph<T> : IGraph<T>
    {
        private const int DefaultSize = 4;

        private readonly Dictionary<T, int> _vertexToIndexMap;
        private readonly Dictionary<int, T> _indexToVertexMap = new Dictionary<int, T>();

        private int _counter;
        private int?[][] _matrix;

        public AdjacencyMatrixGraph(GraphType graphType) : this(graphType, DefaultSize)
        {
        }

        public AdjacencyMatrixGraph(GraphType graphType, int size) : this(graphType, EqualityComparer<T>.Default, size)
        {
        }

        public AdjacencyMatrixGraph(GraphType graphType, IEqualityComparer<T> comparer) : this(graphType, comparer,
            DefaultSize)
        {
        }

        public AdjacencyMatrixGraph(GraphType graphType, IEqualityComparer<T> comparer, int size)
        {
            if (!Enum.IsDefined(typeof(GraphType), graphType))
                throw new InvalidEnumArgumentException(nameof(graphType), (int) graphType, typeof(GraphType));
            Type = graphType;
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _vertexToIndexMap = new Dictionary<T, int>(comparer);
            _matrix = InitializeMatrix(size);
        }

        public IEqualityComparer<T> Comparer { get; }

        public bool Contains(T edge)
        {
            return _vertexToIndexMap.ContainsKey(edge);
        }

        public int Count => _vertexToIndexMap.Count;

        public GraphType Type { get; }

        public IGraph<T> AddVertex(T vertex)
        {
            GetOrAddIndex(vertex);
            return this;
        }

        public IGraph<T> RemoveVertex(T vertex)
        {
            if (_vertexToIndexMap.ContainsKey(vertex) == false)
            {
                return this;
            }

            _counter--;
            
            var indexToRemove = _vertexToIndexMap[vertex];
            var lastIndex = _counter;
            var lastVertex = _indexToVertexMap[lastIndex];

            _matrix[indexToRemove] = _matrix[_counter];
            _matrix[_counter] = Enumerable.Repeat((int?)null, _matrix.Length).ToArray();

            foreach (var row in _matrix)
            {
                row[indexToRemove] = row[lastIndex];
                row[lastIndex] = null;
            }

            _vertexToIndexMap[lastVertex] = indexToRemove;
            _vertexToIndexMap.Remove(vertex);
            
            _indexToVertexMap[indexToRemove] = lastVertex;
            _indexToVertexMap.Remove(lastIndex);

            return this;
        }

        public IGraph<T> AddEdge(T source, T destination, int weight = 1)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            var sourceIndex = GetOrAddIndex(source);

            if (Comparer.Equals(source, destination) == false)
            {
                var destinationIndex = GetOrAddIndex(destination);
                if (_matrix.Length == _counter + 1 - 2)
                    _matrix = DoubleMatrixSize();

                _matrix[sourceIndex][destinationIndex] = weight;

                if (Type == GraphType.Undirected)
                    _matrix[destinationIndex][sourceIndex] = weight;
            }
            
            return this;
        }

        public IGraph<T> RemoveEdge(T source, T destination)
        {
            var sourceIndex = GetIndex(source);
            var destinationIndex = GetIndex(destination);

            if (sourceIndex.HasValue && destinationIndex.HasValue)
            {
                _matrix[sourceIndex.Value][destinationIndex.Value] = null;

                if (Type == GraphType.Undirected)
                    _matrix[destinationIndex.Value][sourceIndex.Value] = null;
            }

            return this;
        }

        public IReadOnlyList<WeightedEdgeDestination<T>> GetAdjacentVertices(T source)
        {
            var sourceIndex = GetIndex(source);
            if (sourceIndex.HasValue)
                return _matrix[sourceIndex.GetValueOrDefault()]
                    .Select((weight, index) => new {weight, index})
                    .Where(cell => cell.weight.HasValue)
                    .Select(cell => new { vertex = _indexToVertexMap[cell.index], cell.weight})
                    .Select(cell => new WeightedEdgeDestination<T>(cell.vertex, cell.weight.GetValueOrDefault()))
                    .ToList();

            return new List<WeightedEdgeDestination<T>>();
        }

        private int?[][] InitializeMatrix(int size)
        {
            var matrix = new int?[size][];

            for (var i = 0; i < matrix.Length; i++) matrix[i] = Enumerable.Repeat((int?)null, size).ToArray();

            return matrix;
        }

        private int?[][] DoubleMatrixSize()
        {
            var bigMatrix = InitializeMatrix(_matrix.Length * 2);

            for (var rowIndex = 0; rowIndex < _matrix.Length; rowIndex++)
                Array.Copy(_matrix[rowIndex], bigMatrix[rowIndex], _matrix[rowIndex].Length);

            return bigMatrix;
        }

        private int? GetIndex(T edge)
        {
            if (_vertexToIndexMap.ContainsKey(edge))
                return _vertexToIndexMap[edge];

            return null;
        }

        private int GetOrAddIndex(T source)
        {
            if (_vertexToIndexMap.ContainsKey(source))
                return _vertexToIndexMap[source];

            var sourceIndex = _counter;

            _vertexToIndexMap[source] = sourceIndex;
            _indexToVertexMap[sourceIndex] = source;
            _counter++;

            return sourceIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _vertexToIndexMap.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}