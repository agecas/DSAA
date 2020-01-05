using System;
using System.Collections.Generic;

namespace DSAA.Graph.ShortestPath.Fluent
{
    public sealed class DistanceInfo<T>
    {
        public DistanceInfo(int distance, int numberOfEdges, T previousVertex)
        {
            if (numberOfEdges < 0) throw new ArgumentOutOfRangeException(nameof(numberOfEdges));
            Distance = distance;
            PreviousVertex = previousVertex;
            NumberOfEdges = numberOfEdges;
            Empty = false;
        }

        public DistanceInfo()
        {
            Empty = true;
        }

        public int Distance { get; }
        public T PreviousVertex { get; }
        public int NumberOfEdges { get; }

        public bool Empty { get; }
        public bool HasValue => Empty == false;

        public DistanceInfo<T> AddStep(T vertex)
        {
            return new DistanceInfo<T>(Distance + 1, NumberOfEdges + 1, vertex);
        }

        public override string ToString()
        {
            return HasValue ? $"Distance: {Distance}, Last Vertex: {PreviousVertex}" : "Empty";
        }

        public static DistanceInfo<T> Zero(T previousVertex)
        {
            return new DistanceInfo<T>(0, 0, previousVertex);
        }

        #region Comparers

        private sealed class DistanceRelationalComparer : IComparer<DistanceInfo<T>>
        {
            public int Compare(DistanceInfo<T> x, DistanceInfo<T> y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.Distance.CompareTo(y.Distance);
            }
        }

        public static IComparer<DistanceInfo<T>> DistanceComparer { get; } = new DistanceRelationalComparer();

        private sealed class DistanceAndEdgeRelationalComparer : IComparer<DistanceInfo<T>>
        {
            public int Compare(DistanceInfo<T> x, DistanceInfo<T> y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.Distance.CompareTo(y.Distance) == 0
                    ? x.NumberOfEdges.CompareTo(y.NumberOfEdges)
                    : x.Distance.CompareTo(y.Distance);
            }
        }

        public static IComparer<DistanceInfo<T>> DistanceAndEdgeComparer { get; } =
            new DistanceAndEdgeRelationalComparer();

        #endregion
    }
}