using DSAA.Graph.Sort.Strategy;

namespace DSAA.Graph.Sort
{
    public static class SortOptionsExtensions
    {
        /// <summary>
        ///     Performs sort using Kahn's algorithm.
        ///     Performance:
        ///     Average:    O(V+E)
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseTopologicalSort<T>(this SortOptions<T> options)
        {
            return new SortOptions<T>(new KahnTopologicalSort<T>());
        }
    }
}