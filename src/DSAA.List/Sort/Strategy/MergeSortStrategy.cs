using System;
using System.Collections.Generic;

namespace DSAA.List.Sort.Strategy
{
    public sealed class MergeSortStrategy<T> : ISortStrategy<T>
    {
        private readonly IComparer<T> _comparer;

        public MergeSortStrategy(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IList<T> Sort(IList<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));


            return Sort(collection, 0, collection.Count - 1);
        }

        public IList<T> Sort(IList<T> collection, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var middleIndex = (startIndex + endIndex) / 2;
                collection = Sort(collection, startIndex, middleIndex);
                collection = Sort(collection, middleIndex + 1, endIndex);
                collection = Merge(collection, startIndex, middleIndex, endIndex);
            }

            return collection;
        }

        public IList<T> Merge(IList<T> list, int startIndex, int middleIndex, int endIndex)
        {
            var listCopy = new List<T>(list);

            var leftStartIndex = startIndex;
            var leftEndIndex = middleIndex;

            var rightStartIndex = middleIndex + 1;
            var rightEndIndex = endIndex;

            var leftHalfPointer = leftStartIndex;
            var rightHalfPointer = rightStartIndex;

            var listPointer = leftStartIndex;

            while (leftHalfPointer <= leftEndIndex && rightHalfPointer <= rightEndIndex)
            {
                if (_comparer.Compare(listCopy[leftHalfPointer], listCopy[rightHalfPointer]) <= 0)
                {
                    list[listPointer] = listCopy[leftHalfPointer];
                    leftHalfPointer++;
                }
                else
                {
                    list[listPointer] = listCopy[rightHalfPointer];
                    rightHalfPointer++;
                }

                listPointer++;
            }

            while (leftHalfPointer <= leftEndIndex)
            {
                list[listPointer] = listCopy[leftHalfPointer];
                leftHalfPointer++;
                listPointer++;
            }

            while (rightHalfPointer <= rightEndIndex)
            {
                list[listPointer] = listCopy[rightHalfPointer];
                rightHalfPointer++;
                listPointer++;
            }

            return list;
        }
    }
}