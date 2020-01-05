using DSAA.List.Search;

namespace DSAA.List.UnitTests.Search.Strategy
{
    public class HashTableSearchTests : UnsortedListSearchTests
    {
        public HashTableSearchTests() : base(o => o.UseHashTableSearch())
        {
        }
    }
}