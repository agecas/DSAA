namespace DSAA.Graph.UnitTests
{
    public sealed class DirectedAdjacencySetGraphTests : DirectedGraphTests
    {
        public DirectedAdjacencySetGraphTests() : base(type => new AdjacencySetGraph<int>(type))
        {
        }
    }
}