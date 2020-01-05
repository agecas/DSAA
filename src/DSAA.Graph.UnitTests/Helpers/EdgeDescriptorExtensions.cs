using System;
using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;
using Xunit;

namespace DSAA.Graph.UnitTests.Helpers
{
    public static class EdgeDescriptorExtensions
    {
        internal static IEnumerable<WeightedEdge<T>> ToWeightedEdges<T>(this IEnumerable<EdgeDescriptor<T>> descriptors)
        {
            return descriptors.SelectMany(v => v.Vertices.Select(e => new WeightedEdge<T>(v.Edge, e.Value, e.Weight)))
                .ToList();
        }

        internal static void AssertEqualsToAny<T>(this EdgeDescriptor<T>[][] expectedEdgeSets, IEnumerable<WeightedEdge<T>> actual,
            IEqualityComparer<WeightedEdge<T>> comparer)
        {
            var results = expectedEdgeSets.AsEnumerable().Select(e =>
            {
                try
                {
                    Assert.Equal(e.ToWeightedEdges(), actual, comparer);

                    return (Pass: true, Exception: (Exception) null);
                }
                catch (Exception exception)
                {
                    return (Pass: false, Exception: exception);
                }
            }).ToList();

            if (results.All(r => r.Pass == false))
                throw new AggregateException(results.Select(r => r.Exception));
        }
    }
}