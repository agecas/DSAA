using DSAA.Graph.UnitTests.Helpers;

namespace DSAA.Graph.UnitTests
{
    /// <summary>
    /// Examples taken from: https://visualgo.net/en/dfsbfs
    /// </summary>
    public static class Graphs
    {
        // CP3 4.17 DAG
        public static EdgeDescriptor<int>[] CP3_417_DAG => new[]
        {
            new EdgeDescriptor<int>(0, 1, 2, 3),
            new EdgeDescriptor<int>(1, 3, 4),
            new EdgeDescriptor<int>(2, 4),
            new EdgeDescriptor<int>(3, 4),
        };

        // CP3 4.18 DAG, Bipartite
        public static EdgeDescriptor<int>[] CP3_418_DAG_Bipartite => new[]
        {
            new EdgeDescriptor<int>(0, 1, 2),
            new EdgeDescriptor<int>(1, 3),
            new EdgeDescriptor<int>(2, 3),
            new EdgeDescriptor<int>(3, 4)
        };

        // CP3 4.4 DAG
        public static EdgeDescriptor<int>[] CP3_44_DAG => new[]
        {
            new EdgeDescriptor<int>(0, 1, 2),
            new EdgeDescriptor<int>(1, 2, 3),
            new EdgeDescriptor<int>(2, 3, 5),
            new EdgeDescriptor<int>(3, 4),
            new EdgeDescriptor<int>(7, 6)
        };

        // CP 4.9
        public static EdgeDescriptor<int>[] CP_49 => new[]
        {
            new EdgeDescriptor<int>(0, 1),
            new EdgeDescriptor<int>(1, 3),
            new EdgeDescriptor<int>(2, 1),
            new EdgeDescriptor<int>(3, 2, 4),
            new EdgeDescriptor<int>(4, 5),
            new EdgeDescriptor<int>(5, 7),
            new EdgeDescriptor<int>(6, 4),
            new EdgeDescriptor<int>(7, 6),
        };

        // CP 4.19 Bipartite
        public static EdgeDescriptor<int>[] CP_419_Bipartite => new[]
        {
            new EdgeDescriptor<int>(0, 1, 4),
            new EdgeDescriptor<int>(1, 2),
            new EdgeDescriptor<int>(2, 1, 3),
            new EdgeDescriptor<int>(3),
            new EdgeDescriptor<int>(4),
        };

        // CP3 4.1
        public static EdgeDescriptor<int>[] CP_41 => new[]
        {
            new EdgeDescriptor<int>(0, 1),
            new EdgeDescriptor<int>(1, 0, 2, 3),
            new EdgeDescriptor<int>(2, 1, 3),
            new EdgeDescriptor<int>(3, 1, 2, 4),
            new EdgeDescriptor<int>(4, 3),
            new EdgeDescriptor<int>(5),
            new EdgeDescriptor<int>(6, 7, 8),
            new EdgeDescriptor<int>(7, 6),
            new EdgeDescriptor<int>(8, 6)
        };

        // CP 4.3
        public static EdgeDescriptor<int>[] CP_43 => new[]
        {
            new EdgeDescriptor<int>(0, 1, 4),
            new EdgeDescriptor<int>(1, 0, 2, 5),
            new EdgeDescriptor<int>(2, 1, 3, 6),
            new EdgeDescriptor<int>(3, 2, 7),
            new EdgeDescriptor<int>(4, 0, 8),
            new EdgeDescriptor<int>(5, 1, 6, 10),
            new EdgeDescriptor<int>(6, 2, 5, 11),
            new EdgeDescriptor<int>(7, 3, 12),
            new EdgeDescriptor<int>(8, 4, 9),
            new EdgeDescriptor<int>(9, 8, 10),
            new EdgeDescriptor<int>(10, 5, 9, 11),
            new EdgeDescriptor<int>(11, 6, 10, 12),
            new EdgeDescriptor<int>(12, 7, 11)
        };
    }
}