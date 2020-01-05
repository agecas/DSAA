using System;
using System.Collections.Generic;

namespace DSAA.Graph.Traverse.Fluent
{
    public sealed class TraverseStrategyBuilder<T> : ISelectTraverseStrategy<T>,
        IBuildTraverseStrategy<T>
    {
        private readonly IGraph<T> _graph;
        private readonly T _startingVertex;
        private IEnumerable<T> _strategy;

        public TraverseStrategyBuilder(IGraph<T> graph, T startingVertex)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _startingVertex = startingVertex;
        }

        public IEnumerable<T> Build()
        {
            return _strategy;
        }

        public IBuildTraverseStrategy<T> BreadthFirst()
        {
            _strategy = new BreadthTraversal<T>(_graph, _startingVertex);
            return this;
        }

        public IBuildTraverseStrategy<T> DepthFirst()
        {
            _strategy = new DepthFirstTraversal<T>(_graph, _startingVertex);
            return this;
        }
    }
}