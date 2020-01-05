using DSAA.List.Search;
using DSAA.UnitTests.Shared;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class FibonacciSearchTests : SortedListSearchTests
    {
        public FibonacciSearchTests() : base(o => o.UseFibonacciSearch<int, IntComparer>())
        {
        }
    }
}