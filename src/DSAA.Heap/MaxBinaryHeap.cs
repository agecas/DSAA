using System.Collections.Generic;

namespace DSAA.Heap
{
    public sealed class MaxBinaryHeap<T> : BinaryHeap<T>
    {
        public MaxBinaryHeap(IComparer<T> comparer) : base(comparer, (c, left, right) => c.Compare(left, right) > 0)
        {
        }
    }
}