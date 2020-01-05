using System;
using System.Collections.Generic;

namespace DSAA.Graph.Domain
{
    public sealed class WeightedEdge<T>
    {
        public WeightedEdge(T source, T destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public T Source { get; }
        public T Destination { get; }
        public int Weight { get; }

        public override string ToString()
        {
            return $"{Source} => {Destination} (Weight: {Weight:F})";
        }

        #region Equality

        public static IEqualityComparer<WeightedEdge<T>> DirectionalComparer(IEqualityComparer<T> comparer)
        {
            return new DirectionalWeightedEdgeComparer(comparer);
        }
        public static IEqualityComparer<WeightedEdge<T>> NonDirectionalComparer(IEqualityComparer<T> comparer)
        {
            return new NonDirectionalWeightedEdgeComparer(comparer);
        }

        private sealed class DirectionalWeightedEdgeComparer : IEqualityComparer<WeightedEdge<T>>
        {
            private readonly IEqualityComparer<T> _comparer;

            public DirectionalWeightedEdgeComparer(IEqualityComparer<T> comparer)
            {
                _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public bool Equals(WeightedEdge<T> left, WeightedEdge<T> right)
            {
                if (ReferenceEquals(left, right)) return true;
                if (ReferenceEquals(left, null)) return false;
                if (ReferenceEquals(right, null)) return false;
                if (left.GetType() != right.GetType()) return false;
                return _comparer.Equals(left.Source, right.Source) &&
                       _comparer.Equals(left.Destination, right.Destination) && left.Weight == right.Weight;
            }

            public int GetHashCode(WeightedEdge<T> obj)
            {
                unchecked
                {
                    var hashCode = _comparer.GetHashCode(obj.Source);
                    hashCode = (hashCode * 397) ^ _comparer.GetHashCode(obj.Destination);
                    hashCode = (hashCode * 397) ^ obj.Weight;
                    return hashCode;
                }
            }
        }

        private sealed class NonDirectionalWeightedEdgeComparer : IEqualityComparer<WeightedEdge<T>>
        {
            private readonly IEqualityComparer<T> _comparer;

            public NonDirectionalWeightedEdgeComparer(IEqualityComparer<T> comparer)
            {
                _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public bool Equals(WeightedEdge<T> left, WeightedEdge<T> right)
            {
                if (ReferenceEquals(left, right)) return true;
                if (ReferenceEquals(left, null)) return false;
                if (ReferenceEquals(right, null)) return false;
                if (left.GetType() != right.GetType()) return false;
                return (_comparer.Equals(left.Source, right.Source) &&
                        _comparer.Equals(left.Destination, right.Destination) ||
                        _comparer.Equals(left.Source, right.Destination) &&
                        _comparer.Equals(left.Destination, right.Source))
                       && left.Weight == right.Weight;
            }

            public int GetHashCode(WeightedEdge<T> obj)
            {
                unchecked
                {
                    var hashCode = _comparer.GetHashCode(obj.Source) + _comparer.GetHashCode(obj.Destination);
                    hashCode = (hashCode * 397) ^ obj.Weight;
                    return hashCode;
                }
            }
        }

        #endregion
    }
}