using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Heap
{
    public interface IBinaryHeap<T> : IEnumerable<T>
    {
        int Count { get; }
        bool Empty { get; }
        bool Contains(T value);
        IBinaryHeap<T> Push(T value);
        Optional<T> Pop();
        Optional<T> Peek(); 
        IBinaryHeap<T> Remove(T value);
    }
}