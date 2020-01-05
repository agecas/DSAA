using System;
using System.Collections.Generic;

namespace DSAA.Shared
{
    public sealed class LambdaComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> _comparer;

        public LambdaComparer(Func<T, T, int> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public int Compare(T x, T y)
        {
            return _comparer(x, y);
        }
    }
}