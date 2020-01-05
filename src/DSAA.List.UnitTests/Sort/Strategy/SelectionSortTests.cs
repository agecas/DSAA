using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class SelectionSortTests : SortListTests
    {
        public SelectionSortTests() : base(o => o.UseSelectionSort<int, IntComparer>())
        {
        }
    }
}