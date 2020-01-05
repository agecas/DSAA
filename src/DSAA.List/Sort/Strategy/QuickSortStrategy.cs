using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.List.Sort.Strategy
{
    public sealed class QuickSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public QuickSortStrategy(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            collection = Sort(collection, 0, collection.Count - 1);
          
            return collection;
        }

        private IList<T> Sort(IList<T> collection, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var (updatedCollection, partitionIndex) = PartitionList(collection, startIndex, endIndex);
                collection = updatedCollection;
                collection = Sort(collection, startIndex, partitionIndex);
                collection = Sort(collection, partitionIndex + 1, endIndex);
            }

            return collection;
        }

        private (IList<T> Collection, int Index) PartitionList(IList<T> collection, int startIndex, int endIndex)
        {
            var pivotIndex = GetPivotIndex(startIndex, endIndex);
            T pivotValue = collection[pivotIndex];

            var leftIndex = startIndex;
            var rightIndex = endIndex;

            while (true)
            {
                while (leftIndex <= endIndex && _comparer.Compare(collection[leftIndex], pivotValue) < 0)
                    leftIndex++;

                while (rightIndex <= endIndex && _comparer.Compare(collection[rightIndex], pivotValue) > 0)
                    rightIndex--;

                if (rightIndex <= leftIndex)
                    return (collection, rightIndex);

                collection = collection.Swap<T, IList<T>>(leftIndex, rightIndex);

                leftIndex++;
                rightIndex--;
            }
        }

        public int GetPivotIndex(int startIndex, int endIndex)
        {
            return (startIndex + endIndex) / 2;
        }
    }
}