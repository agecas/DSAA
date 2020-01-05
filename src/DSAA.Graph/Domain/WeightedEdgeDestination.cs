using System;
using System.Collections.Generic;

namespace DSAA.Graph.Domain
{
    public sealed class WeightedEdgeDestination<T> : IEquatable<WeightedEdgeDestination<T>>
    {
        public T Value { get; }
        public int Weight { get; }

        public WeightedEdgeDestination(T value, int weight)
        {
            Value = value;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Weight: {Weight:F}, Value: {Value}";
        }

        public static implicit operator T(WeightedEdgeDestination<T> edge) => edge.Value;

        #region Equality

        public bool Equals(WeightedEdgeDestination<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value) && Weight == other.Weight;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is WeightedEdgeDestination<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value) * 397) ^ Weight;
            }
        }

        public static bool operator ==(WeightedEdgeDestination<T> left, WeightedEdgeDestination<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WeightedEdgeDestination<T> left, WeightedEdgeDestination<T> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}