using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.List.Sort.Strategy
{
    public sealed class InsertionSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public InsertionSortStrategy(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            for (var i = 1; i < collection.Count; i++)
            for (var j = i - 1; j >= 0 && _comparer.Compare(collection[j], collection[j + 1]) > 0; j--)
                collection = collection.Swap<T, IList<T>>(j, j + 1);

            return collection;
        }
    }
}