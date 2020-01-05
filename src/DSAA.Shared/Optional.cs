using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSAA.Shared
{
    public sealed class Optional<T> : IEnumerable<T>
    {
        private readonly List<T> _data = new List<T>();

        private Optional(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _data.Add(value);
        }

        private Optional()
        {
        }

        public static Optional<T> None() => new Optional<T>();
        public static Optional<T> Some(T value) => new Optional<T>(value);

        public bool HasData => _data.Any();
        public bool IsEmpty => _data.Count == 0;

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator T(Optional<T> container) => container.Single();
        public static implicit operator Optional<T>(T value) => new Optional<T>(value);

        private bool Equals(Optional<T> other)
        {
            return Equals(_data, other._data);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Optional<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (_data != null ? _data.GetHashCode() : 0);
        }

        public static bool operator ==(Optional<T> left, Optional<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Optional<T> left, Optional<T> right)
        {
            return !Equals(left, right);
        }
    }
}