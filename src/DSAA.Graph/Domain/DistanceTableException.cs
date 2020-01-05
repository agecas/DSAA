using System;
using System.Collections.Generic;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Shared;

namespace DSAA.Graph.Domain
{
    public sealed class DistanceTableException<T> : DssaException
    {
        private DistanceTableException(IDictionary<T, DistanceInfo<T>> distances, string message) : base(message)
        {
            Distances = distances ?? throw new ArgumentNullException(nameof(distances));
        }

        public IDictionary<T, DistanceInfo<T>> Distances { get; }

        public static DistanceTableException<T> WhenCyclicVertexFound(T vertex, T source, T destination,
            IDictionary<T, DistanceInfo<T>> distances)
        {
            return new DistanceTableException<T>(distances,
                $"Previous vertex '{vertex}' points to itself, which results in a cycle!");
        }
    }
}