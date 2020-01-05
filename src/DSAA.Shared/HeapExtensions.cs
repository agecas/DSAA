using System;
using System.Collections.Generic;

namespace DSAA.Shared
{
    public static class HeapExtensions
    {
        public static TCollection SiftUp<T, TCollection>(this TCollection data, int index, int endIndex, IComparer<T> comparer,
            Func<IComparer<T>, T, T, bool> validator) where TCollection : IList<T>
        {
            var parentIndexResult = GetParentIndex(index, endIndex);
            var parentIndex = parentIndexResult.GetValueOrDefault();

            while (parentIndexResult.HasValue && validator(comparer, data[index], data[parentIndex]))
            {
                data = data.Swap<T, TCollection>(index, parentIndex);
                index = parentIndex;
                parentIndex = GetParentIndex(index, endIndex).GetValueOrDefault();
            }

            return data;
        }

        public static TCollection SiftDown<T, TCollection>(this TCollection data, int index, int endIndex, IComparer<T> comparer,
            Func<IComparer<T>, T, T, bool> validator) where TCollection : IList<T>
        {
            var nextIndex = index;

            while (GetLeftChildIndex(nextIndex, endIndex) <= endIndex)
            {
                var topIndex = data.FindTopIndex(nextIndex, endIndex, comparer, validator);
                if (topIndex.HasValue && validator(comparer, data[topIndex.Value], data[nextIndex]))
                {
                    data = data.Swap<T, TCollection>(topIndex.Value, nextIndex);
                    nextIndex = topIndex.Value;
                }
                else
                {
                    break;
                }
            }

            return data;
        }

        private static int? FindTopIndex<T>(this IList<T> data, int index, int endIndex, IComparer<T> comparer,
            Func<IComparer<T>, T, T, bool> validator)
        {
            var leftChildIndex = GetLeftChildIndex(index, endIndex);
            var rightChildIndex = GetRightChildIndex(index, endIndex);

            if (leftChildIndex.HasValue && rightChildIndex.HasValue)
                return validator(comparer, data[leftChildIndex.Value], data[rightChildIndex.Value])
                    ? leftChildIndex
                    : rightChildIndex;

            return leftChildIndex ?? rightChildIndex;
        }

        public static int? GetParentIndex(int index, int endIndex)
        {
            return index >= 0 && index <= endIndex ? (index - 1) / 2 : (int?) null;
        }

        private static int? GetLeftChildIndex(int index, int endIndex)
        {
            var childIndex = 2 * index + 1;
            return childIndex <= endIndex ? childIndex : (int?) null;
        }

        private static int? GetRightChildIndex(int index, int endIndex)
        {
            var childIndex = 2 * index + 2;
            return childIndex <= endIndex ? childIndex : (int?) null;
        }
    }
}