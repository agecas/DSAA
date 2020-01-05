using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Shared;

namespace DSAA.Graph.SpanningTree.Fluent
{
    public interface IBuildSpanningTreeStrategy<T>
    {
        Optional<IEnumerable<WeightedEdge<T>>> Build(IGraph<T> graph);
    }
}