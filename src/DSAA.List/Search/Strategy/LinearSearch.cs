using System;
using System.Collections.Generic;

namespace DSAA.List.Search.Strategy
{
    public sealed class LinearSearch<T> : ISearchStrategy<T>
    {
        public IEnumerable<int> FindAllIndexes(IList<T> collection, T valueToFind)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            for (var i = 0; i < collection.Count; i++)
            {
                if (collection[i].Equals(valueToFind))
                {
                    yield return i;
                }
            }
        }
    }
}