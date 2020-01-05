using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class HeapSortTests : SortListTests
    {
        public HeapSortTests() : base(o => o.UseHeapSort<int, IntComparer>())
        {
        }
    }
}