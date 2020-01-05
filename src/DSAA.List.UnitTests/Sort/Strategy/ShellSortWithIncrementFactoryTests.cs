using DSAA.List.Sort;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Sort.Strategy
{
    public class ShellSortWithIncrementFactoryTests : SortListTests
    {
        public ShellSortWithIncrementFactoryTests() : base(o => o.UseShellSort<int, IntComparer>(c => c.Count / 3))
        {
        }
    }
}