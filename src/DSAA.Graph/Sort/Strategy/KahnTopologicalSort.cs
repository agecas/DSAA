using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Shared;

namespace DSAA.Graph.Sort.Strategy
{
    public sealed class KahnTopologicalSort<T> : ISortStrategy<T>
    {
        public Optional<IEnumerable<T>> Sort(IGraph<T> graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (graph.Count == 0)
                return new List<T>();

            var inDegreeMap = CalculateInDegree(graph);
            var verticesWithoutAdjacent = inDegreeMap.Where(kv => kv.Value == 0).Select(kv => kv.Key).ToList();

            if (verticesWithoutAdjacent.Count == 0)
                return Optional<IEnumerable<T>>.None();

            var verticesToProcess = new Queue<T>(verticesWithoutAdjacent);
            var sortedVertices = new List<T>();

            while (verticesToProcess.Count > 0)
            {
                var vertex = verticesToProcess.Dequeue();
                var adjacentVertices = graph.GetAdjacentVertices(vertex);

                sortedVertices.Add(vertex);

                foreach (var adjacentVertex in adjacentVertices)
                {
                    var updatedInDegree = inDegreeMap[adjacentVertex] - 1;
                    inDegreeMap[adjacentVertex] = updatedInDegree;

                    if (updatedInDegree == 0)
                        verticesToProcess.Enqueue(adjacentVertex);
                }
            }

            if (sortedVertices.Count != graph.Count)
            {
                return Optional<IEnumerable<T>>.None();
            }

            return sortedVertices;
        }

        private IDictionary<T, int> CalculateInDegree(IGraph<T> graph)
        {
            var vertices = graph.ToList();
            var inDegreeMap = vertices.ToDictionary(v => v, v => 0);

            foreach (var vertex in vertices)
            {
                var adjacentVertices = graph.GetAdjacentVertices(vertex);

                foreach (var adjacentVertex in adjacentVertices)
                {
                        inDegreeMap[adjacentVertex]++;
                }
            }

            return inDegreeMap;
        }
    }
}