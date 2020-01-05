namespace DSAA.Graph.UnitTests.Sort.Strategy
{
    public sealed class AdjacencySetKahnTopologicalSortTests : KahnTopologicalSortTests
    {
        public AdjacencySetKahnTopologicalSortTests() : base(b => b.Sparse())
        {
            
        }
    }
}