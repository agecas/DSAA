namespace DSAA.Graph.UnitTests.Sort.Strategy
{
    public sealed class AdjacencyMatrixKahnTopologicalSortTests : KahnTopologicalSortTests
    {
        public AdjacencyMatrixKahnTopologicalSortTests() : base(b => b.WellConnected())
        {
            
        }
    }
}
