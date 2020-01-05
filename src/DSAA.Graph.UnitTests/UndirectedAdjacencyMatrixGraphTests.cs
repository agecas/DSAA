namespace DSAA.Graph.UnitTests
{
    public sealed class UndirectedAdjacencyMatrixGraphTests : UndirectedGraphTests
    {
        public UndirectedAdjacencyMatrixGraphTests() : base(type => new AdjacencyMatrixGraph<int>(type))
        {
        }
    }
}