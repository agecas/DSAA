using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class InsertionSortTests : SortListTests
    {
        public InsertionSortTests() : base(o => o.UseInsertionSort<int, IntComparer>())
        {
        }
    }
}