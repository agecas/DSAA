using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class ShellSortTests : SortListTests
    {
        public ShellSortTests() : base(o => o.UseShellSort<int, IntComparer>())
        {
        }
    }
}