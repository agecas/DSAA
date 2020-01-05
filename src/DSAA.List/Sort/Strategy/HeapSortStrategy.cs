using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.List.Sort.Strategy
{
    public sealed class HeapSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly Func<IComparer<T>, T, T, bool> _validator = (c, left, right) => c.Compare(left, right) >= 0;

        public HeapSortStrategy(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var heap = Heapify(collection);

            for (var index = heap.Count - 1; index > 0; index--)
            {
                heap = heap
                    .Swap<T, IList<T>>(0, index)
                    .SiftDown(0, index - 1, _comparer, _validator);
            }

            return heap;
        }

        private IList<T> Heapify(IList<T> heap)
        {
            var endIndex = heap.Count - 1;

            for (var index = (endIndex - 1) / 2; index >= 0; index--)
            {
                heap = heap.SiftDown(index, endIndex, _comparer, _validator);
            }

            return heap;
        }
    }
}