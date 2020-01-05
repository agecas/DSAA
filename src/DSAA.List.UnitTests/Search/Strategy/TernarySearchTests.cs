using DSAA.List.Search;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class TernarySearchTests : SortedListSearchTests
    {
        public TernarySearchTests() : base(o => o.UseTernarySearch<int, IntComparer>())
        {
        }
    }
}