using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class MergeSortTests : SortListTests
    {
        public MergeSortTests() : base(o => o.UseMergeSort<int, IntComparer>())
        {
        }
    }
}