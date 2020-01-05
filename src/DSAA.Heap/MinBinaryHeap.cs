using System.Collections.Generic;

namespace DSAA.Heap
{
    public sealed class MinBinaryHeap<T> : BinaryHeap<T>
    {
        public MinBinaryHeap(IComparer<T> comparer) : base(comparer, (c, left, right) => c.Compare(left, right) < 0)
        {
        }
    }
}