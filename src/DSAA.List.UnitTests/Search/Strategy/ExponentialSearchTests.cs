using DSAA.List.Search;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class ExponentialSearchTests : SortedListSearchTests
    {
        public ExponentialSearchTests() : base(o => o.UseExponentialSearch<int, IntComparer>())
        {
        }
    }
}