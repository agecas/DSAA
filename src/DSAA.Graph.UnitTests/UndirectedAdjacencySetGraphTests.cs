namespace DSAA.Graph.UnitTests
{
    public sealed class UndirectedAdjacencySetGraphTests : UndirectedGraphTests
    {
        public UndirectedAdjacencySetGraphTests() : base(type => new AdjacencySetGraph<int>(type))
        {
        }
    }
}