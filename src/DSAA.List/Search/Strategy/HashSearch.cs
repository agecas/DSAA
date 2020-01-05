using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class HashSearch<T> : ISearchStrategy<T>
    {
        public IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var map = ToDictionary(collection);
            var key = valueToFind.GetHashCode();
            return map.ContainsKey(key) ? map[key] : new List<int>();
        }

        private Dictionary<int, List<int>> ToDictionary(IList<T> collection)
        {
            var map = new Dictionary<int, List<int>>();

            for (var i = 0; i < collection.Count; i++)
            {
                var value = collection[i];
                var key = value.GetHashCode();
                map = AddOrUpdateValue(map, key, i);
            }

            return map;
        }

        private Dictionary<int, List<int>> AddOrUpdateValue(Dictionary<int, List<int>> map, int key, int index)
        {
            if (map.ContainsKey(key))
            {
                map[key].Add(index);
            }
            else
            {
                map[key] = new List<int> {index};
            }

            return map;
        }
    }
}