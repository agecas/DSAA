using System;
using System.Collections;
using System.Collections.Generic;
using DSAA.Shared;

namespace DSAA.Heap
{
    public abstract class BinaryHeap<T> : IBinaryHeap<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly Func<IComparer<T>, T, T, bool> _validator;
        private List<T> _data = new List<T>();

        private int LastIndex => _data.Count - 1;

        protected BinaryHeap(IComparer<T> comparer, Func<IComparer<T>, T, T, bool> validator)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public int Count => _data.Count;
        public bool Empty => Count == 0;

        public bool Contains(T value) => _data.Count > 0 && _data.FindIndex(v => v.Equals(value)) > 0;

        public IBinaryHeap<T> Push(T value)
        {
            _data.Add(value);
            _data = _data.SiftUp(LastIndex, LastIndex, _comparer, _validator);
            return this;
        }

        public Optional<T> Pop()
        {
            if (_data.Count == 0)
                return Optional<T>.None();

            var result = _data[0];
            _data[0] = _data[^1];
            _data.RemoveAt(LastIndex);
            _data = _data.SiftDown(0, LastIndex, _comparer, _validator);

            return result;
        }

        public Optional<T> Peek()
        {
            return _data.Count > 0 ? Optional<T>.Some(_data[0]) : Optional<T>.None();
        }

        public IBinaryHeap<T> Remove(T value)
        {
            var index = _data.FindIndex(v => v.Equals(value));
            if (index < 0)
                return this;

            if (index == 0)
            {
                Pop();
                return this;
            }

            _data[index] = _data[^1];
            _data.RemoveAt(LastIndex);

            if (index > LastIndex)
                return this;

            var parentIndex = HeapExtensions.GetParentIndex(index, LastIndex).GetValueOrDefault();
            var parent = _data[parentIndex];

            if (_validator(_comparer, _data[index], parent))
            {
                _data = _data.SiftUp(index, LastIndex, _comparer, _validator);
            }
            else
            {
                _data = _data.SiftDown(index, LastIndex, _comparer, _validator);
            }

            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
