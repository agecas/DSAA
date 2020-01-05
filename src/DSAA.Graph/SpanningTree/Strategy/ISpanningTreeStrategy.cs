using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Shared;

namespace DSAA.Graph.SpanningTree.Strategy
{
    public interface ISpanningTreeStrategy<T>
    {
        Optional<IEnumerable<WeightedEdge<T>>> FindSpanningTreeEdges(IGraph<T> graph);
    }
}