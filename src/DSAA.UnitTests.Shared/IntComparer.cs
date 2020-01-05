using System.Collections.Generic;

namespace DSAA.UnitTests.Shared
{
    public sealed class IntComparer : IComparer<int>, IEqualityComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }

        public bool Equals(int x, int y) => x == y;

        public int GetHashCode(int obj) => obj.GetHashCode();
    }
}
