using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class BubbleSortTests : SortListTests
    {
        public BubbleSortTests() : base(o => o.UseBubbleSort<int, IntComparer>())
        {
        }
    }
}