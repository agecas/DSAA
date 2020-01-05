using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class QuickSortTests : SortListTests
    {
        public QuickSortTests() : base(o => o.UseQuickSort<int, IntComparer>())
        {
        }
    }
}