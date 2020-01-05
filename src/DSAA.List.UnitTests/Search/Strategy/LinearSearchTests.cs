using DSAA.List.Search;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class LinearSearchTests : UnsortedListSearchTests
    {
        public LinearSearchTests() : base(o => o.UseLinearSearch())
        {
        }
    }
}