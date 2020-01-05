using DSAA.List.Search;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class JumpSearchTests : SortedListSearchTests
    {
        public JumpSearchTests() : base(o => o.UseJumpSearch<int, IntComparer>())
        {
        }
    }
}