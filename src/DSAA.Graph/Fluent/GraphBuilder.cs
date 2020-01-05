using System;
using System.Collections.Generic;
using DSAA.Graph.Domain;
using DSAA.Shared;

namespace DSAA.Graph.Fluent
{
    public static class GraphBuilder
    {
        public static IGraph<T> Create<T>(Func<ISetGraphDirection<T>, IBuildGraph<T>> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            return builder(new GraphBuilder<T>()).Build();
        }
    }

    public sealed class GraphBuilder<T> : ISetGraphDirection<T>, IBuildGraph<T>, ISetGraphDensity<T>, ISetComparer<T>
    {
        private IEqualityComparer<T> _comparer;
        private Func<GraphType, IEqualityComparer<T>, IGraph<T>> _factory;
        private GraphType _graphType;

        public GraphBuilder()
        {
            _comparer = EqualityComparer<T>.Default;
        }

        public IGraph<T> Build()
        {
            return _factory(_graphType, _comparer);
        }

        public IBuildGraph<T> CompareUsing<TComparer>() where TComparer : IEqualityComparer<T>, new()
        {
            _comparer = new TComparer();
            return this;
        }

        public IBuildGraph<T> CompareUsing(IEqualityComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            return this;
        }

        public IBuildGraph<T> CompareUsing(Func<T, T, bool> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            _comparer = new LambdaEqualityComparer<T>(comparer);
            return this;
        }

        public ISetComparer<T> WellConnected()
        {
            _factory = (type, comparer) => new AdjacencyMatrixGraph<T>(type, comparer);
            return this;
        }

        public ISetComparer<T> Sparse()
        {
            _factory = (type, comparer) => new AdjacencySetGraph<T>(type, comparer);
            return this;
        }

        public ISetGraphDensity<T> Directed()
        {
            _graphType = GraphType.Directed;
            return this;
        }

        public ISetGraphDensity<T> Undirected()
        {
            _graphType = GraphType.Undirected;
            return this;
        }
    }
}