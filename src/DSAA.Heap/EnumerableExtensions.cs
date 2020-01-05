using System;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Heap
{
    public static class EnumerableExtensions
    {
        public static IBinaryHeap<T> ToMinBinaryHeap<T, TComparer>(
            this IEnumerable<T> source)
            where TComparer : IComparer<T>, new()
        {
            return source.ToMinBinaryHeap(new TComparer());
        }

        public static IBinaryHeap<T> ToMinBinaryHeap<T>(this IEnumerable<T> source)
            where T : IComparable<T>
        {
            return source.ToMinBinaryHeap((x, y) => x.CompareTo(y));
        }

        public static IBinaryHeap<T> ToMinBinaryHeap<T>(
            this IEnumerable<T> source,
            Func<T, T, int> comparer)
        {
            return source.ToMinBinaryHeap(new LambdaComparer<T>(comparer));
        }

        public static IBinaryHeap<T> ToMinBinaryHeap<T>(
            this IEnumerable<T> source,
            IComparer<T> comparer)
        {
            return source.ToBinaryHeap(() => new MinBinaryHeap<T>(comparer));
        }

        public static IBinaryHeap<T> ToMaxBinaryHeap<T, TComparer>(
            this IEnumerable<T> source)
            where TComparer : IComparer<T>, new()
        {
            return source.ToMaxBinaryHeap(new TComparer());
        }

        public static IBinaryHeap<T> ToMaxBinaryHeap<T>(this IEnumerable<T> source)
            where T : IComparable<T>
        {
            return source.ToMaxBinaryHeap((x, y) => x.CompareTo(y));
        }

        public static IBinaryHeap<T> ToMaxBinaryHeap<T>(
            this IEnumerable<T> source,
            Func<T, T, int> comparer)
        {
            return source.ToMaxBinaryHeap(new LambdaComparer<T>(comparer));
        }

        public static IBinaryHeap<T> ToMaxBinaryHeap<T>(
            this IEnumerable<T> source,
            IComparer<T> comparer)
        {
            return source.ToBinaryHeap(() => new MaxBinaryHeap<T>(comparer));
        }

        private static IBinaryHeap<T> ToBinaryHeap<T>(
            this IEnumerable<T> source, Func<IBinaryHeap<T>> heapFactory)
        {
            var heap = heapFactory();

            foreach (var item in source) heap.Push(item);

            return heap;
        }
    }
}