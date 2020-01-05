using System;
using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Shared;

namespace DSAA.Graph.ShortestPath
{
    public sealed class DistanceTableToPathConverter<T>
    {
        private readonly IEqualityComparer<T> _comparer;

        public DistanceTableToPathConverter(IEqualityComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Optional<GraphPath<T>> FindPath(T source, T destination,
            IDictionary<T, DistanceInfo<T>> distances)
        {
            if (distances == null) throw new ArgumentNullException(nameof(distances));
            if (distances.ContainsKey(destination) == false || distances.ContainsKey(source) == false)
                return Optional<GraphPath<T>>.None();

            if (_comparer.Equals(source, destination))
                return new GraphPath<T>(new List<T> { source }, 0, _comparer);

            var distance = distances[destination];
            var totalDistance = distance.Distance;

            var path = new Stack<T>();
            path.Push(destination);

            while (distance.HasValue && _comparer.Equals(distance.PreviousVertex, source) == false)
            {
                path.Push(distance.PreviousVertex);
                distance = distances[distance.PreviousVertex];

                if (_comparer.Equals(distance.PreviousVertex, destination))
                    throw DistanceTableException<T>.WhenCyclicVertexFound(distance.PreviousVertex, source, destination, distances);
            }

            if (distance.Empty) return Optional<GraphPath<T>>.None();
            if (_comparer.Equals(destination, distance.PreviousVertex) == false)
                path.Push(distance.PreviousVertex);

            return new GraphPath<T>(new List<T>(path), totalDistance, _comparer);
        }
    }
}