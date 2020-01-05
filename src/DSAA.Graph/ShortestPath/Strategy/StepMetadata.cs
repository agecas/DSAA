using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath.Fluent;

namespace DSAA.Graph.ShortestPath.Strategy
{
    public sealed class StepMetadata<T>
    {
        private readonly T _source;
        private readonly WeightedEdgeDestination<T> _destination;

        private StepMetadata(T source, WeightedEdgeDestination<T> destination,
            IReadOnlyDictionary<T, DistanceInfo<T>> distances, int distance)
        {
            _source = source;
            _destination = destination;
            SourceDistance = distances[source];
            DestinationDistance = distances[destination];
            Distance = distance;
            Edges = SourceDistance.NumberOfEdges + 1;
        }

        public int Edges { get; }

        public int Distance { get; }

        public DistanceInfo<T> DestinationDistance { get; }

        public DistanceInfo<T> SourceDistance { get; }

        public bool FoundShorterDistance()
        {
            return SourceDistance.HasValue &&
                   (DestinationDistance.Empty || Distance < DestinationDistance.Distance);
        }

        public bool SameDistanceButLessEdges()
        {
            return SourceDistance.HasValue &&
                   (DestinationDistance.Empty || Distance == DestinationDistance.Distance &&
                    DestinationDistance.NumberOfEdges > Edges);
        }

        public DistanceInfo<T> ToSource() => new DistanceInfo<T>(Distance, Edges, _source);
        public DistanceInfo<T> ToDestination() => new DistanceInfo<T>(Distance, Edges, _destination);

        public static StepMetadata<T> ForPath(T source, WeightedEdgeDestination<T> destination,
            IReadOnlyDictionary<T, DistanceInfo<T>> distances)
        {
            var distance = distances[source].Distance + destination.Weight;
            return new StepMetadata<T>(source, destination, distances, distance);
        }

        public static StepMetadata<T> ForSpanningTree(T source, WeightedEdgeDestination<T> destination,
            IReadOnlyDictionary<T, DistanceInfo<T>> distances) => new StepMetadata<T>(source, destination, distances, destination.Weight);
    }
}