using DSAA.List.Search;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class BinarySearchTests : SortedListSearchTests
    {
        public BinarySearchTests() : base(o => o.UseBinarySearch<int, IntComparer>())
        {
        }
    }
}