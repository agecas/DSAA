using System;
using System.Collections.Generic;
using DSAA.Graph.Fluent;

namespace DSAA.Graph
{
    public static class DictionaryExtensions
    {
        public static IGraph<T> ToGraph<T, TValues>(this IDictionary<T, TValues> lookup, Func<ISetGraphDirection<T>, IBuildGraph<T>> builder)
        where TValues : IEnumerable<T>
        {
            if (lookup == null) throw new ArgumentNullException(nameof(lookup));
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            var graph = builder(new GraphBuilder<T>()).Build();

            foreach (var pair in lookup)
            {
                graph.AddVertex(pair.Key);

                foreach (var value in pair.Value)
                {
                    graph.AddEdge(pair.Key, value);
                }
            }

            return graph;
        }
    }
}
