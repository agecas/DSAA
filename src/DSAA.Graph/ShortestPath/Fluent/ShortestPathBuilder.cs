using System;
using DSAA.Graph.ShortestPath.Strategy;
using DSAA.Shared;

namespace DSAA.Graph.ShortestPath.Fluent
{
    public sealed class ShortestPathBuilder<T> : ISelectDistanceTableStrategy<T>,
        IBuildPathStrategy<T>
    {
        private IDistanceTableStrategy<T> _strategy;

        public Optional<GraphPath<T>> Build(IGraph<T> graph, T source, T destination)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var converter = new DistanceTableToPathConverter<T>(graph.Comparer);
            var distanceTable = _strategy.BuildDistanceTable(graph, source);
            
            return converter.FindPath(source, destination, distanceTable);
        }

        public IBuildPathStrategy<T> UseUnweightedGraph()
        {
            _strategy = new UnweightedGraphStrategy<T>();
            return this;
        }

        public IBuildPathStrategy<T> UseDijkstra()
        {
            _strategy = new DijkstraStrategy<T>();
            return this;
        }

        public IBuildPathStrategy<T> UseBellmanFord()
        {
            _strategy = new BellmanFordsStrategy<T>();
            return this;
        }
    }
}