﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSAA.Graph.Traverse
{
    public sealed class BreadthTraversal<T> : IEnumerable<T>
    {
        private readonly IGraph<T> _graph;
        private readonly T _startingVertex;

        public BreadthTraversal(IGraph<T> graph, T startingVertex)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _startingVertex = startingVertex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_graph.Count == 0)
            {
                yield break;
            }

            var visited = new HashSet<T>(_graph.Comparer);
            var verticesToVisit = new Queue<T>(new[]
            {
                _startingVertex
            });

            while (verticesToVisit.Count > 0)
            {
                var vertex = verticesToVisit.Dequeue();

                if (visited.Contains(vertex))
                {
                    continue;
                }

                visited.Add(vertex);

                yield return vertex;

                var vertices = _graph.GetAdjacentVertices(vertex)
                    .Where(v => visited.Contains(v) == false)
                    .ToList();

                foreach (var vertexToVisit in vertices)
                {
                    verticesToVisit.Enqueue(vertexToVisit);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
