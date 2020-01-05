using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;
using DSAA.Graph.SpanningTree.Strategy;
using DSAA.Shared;

namespace DSAA.Graph.SpanningTree.Fluent
{
    public sealed class SpanningTreeBuilder<T> : ISelectSpanningTreeStrategy<T>,
        IBuildSpanningTreeStrategy<T>
    {
        private ISpanningTreeStrategy<T> _strategy;
      
        public IBuildSpanningTreeStrategy<T> UsePrims()
        {
            _strategy = new PrimsSpanningTreeStrategy<T>(graph => graph.First());
            return this;
        }

        public IBuildSpanningTreeStrategy<T> UseKruskals()
        {
            _strategy = new KruskalsSpanningTreeStrategy<T>();
            return this;
        }

        public Optional<IEnumerable<WeightedEdge<T>>> Build(IGraph<T> graph)
        {
            return _strategy.FindSpanningTreeEdges(graph);
        }
    }
}