using System;
using System.Collections.Generic;
using System.Linq;

namespace DSAA.Graph.ShortestPath
{
    public sealed class GraphPath<T> : IEquatable<GraphPath<T>>
    {
        private readonly IEqualityComparer<T> _comparer;
        public IEnumerable<T> Path { get; }
        public int Distance { get; }

        public GraphPath(IEnumerable<T> path, int distance, IEqualityComparer<T> comparer)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Distance = distance;
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        #region Equality

        public bool Equals(GraphPath<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Path.SequenceEqual(other.Path, _comparer) && Distance == other.Distance;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GraphPath<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Path != null ? Path.GetHashCode() : 0) * 397) ^ Distance;
            }
        }

        public static bool operator ==(GraphPath<T> left, GraphPath<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GraphPath<T> left, GraphPath<T> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}