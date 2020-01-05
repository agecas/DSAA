using System;
using System.Collections.Generic;
using DSAA.Graph.Domain;

namespace DSAA.Graph
{
    internal sealed class EdgeEqualityComparer<T> : IEqualityComparer<WeightedEdgeDestination<T>>
    {
        private readonly IEqualityComparer<T> _comparer;

        public EdgeEqualityComparer(IEqualityComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public bool Equals(WeightedEdgeDestination<T> x, WeightedEdgeDestination<T> y)
        {
            return _comparer.Equals(x, y);
        }

        public int GetHashCode(WeightedEdgeDestination<T> obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}