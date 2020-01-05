namespace DSAA.Graph.UnitTests
{
    public sealed class DirectedAdjacencyMatrixGraphTests : DirectedGraphTests
    {
        public DirectedAdjacencyMatrixGraphTests() : base(type => new AdjacencyMatrixGraph<int>(type))
        {
        }
    }
}