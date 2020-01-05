using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.List.Sort.Strategy
{
    public sealed class BubbleSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public BubbleSortStrategy(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var swapped = true;

            while (swapped)
            {
                swapped = false;

                for (var index = 0; index < collection.Count - 1; index++)
                {
                    if (_comparer.Compare(collection[index + 1], collection[index]) < 0)
                    {
                        collection = collection.Swap<T, IList<T>>(index, index + 1);
                        swapped = true;
                    }
                }
            }

            return collection;
        }
    }
}