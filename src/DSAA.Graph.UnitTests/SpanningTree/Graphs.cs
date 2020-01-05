using DSAA.Graph.UnitTests.Helpers;

namespace DSAA.Graph.UnitTests.SpanningTree
{
    /// <summary>
    /// Examples taken from: https://visualgo.net/en/mst
    /// </summary>
    public static class Graphs
    {
        // CP 4.10
        public static EdgeDescriptor<int>[] CP_410 => new[]
        {
            new EdgeDescriptor<int>(0, (1, 4), (2, 4), (3, 6), (4, 6)),
            new EdgeDescriptor<int>(1, (0, 4), (2, 2)),
            new EdgeDescriptor<int>(2, (0, 4), (1, 2), (3, 8)),
            new EdgeDescriptor<int>(3, (0, 6), (2, 8), (4, 9)),
            new EdgeDescriptor<int>(4, (0, 6), (3, 9)),
        };

        // CP 4.14
        public static EdgeDescriptor<int>[] CP_414 => new[]
        {
            new EdgeDescriptor<int>(0, (1, 9), (2, 75)),
            new EdgeDescriptor<int>(1, (0, 9), (2, 95), (3, 19), (4, 42)),
            new EdgeDescriptor<int>(2, (0, 75), (1, 95), (3, 51)),
            new EdgeDescriptor<int>(3, (1, 19), (2, 51), (4, 31)),
            new EdgeDescriptor<int>(4, (1, 42), (3, 31)),
        };

        // K5
        public static EdgeDescriptor<int>[] K5 => new[]
        {
            new EdgeDescriptor<int>(0, (1, 24), (2, 13), (3, 13), (4, 22)),
            new EdgeDescriptor<int>(1, (0, 24), (2, 22), (3, 13), (4, 13)),
            new EdgeDescriptor<int>(2, (0, 13), (1, 22), (3, 19), (4, 14)),
            new EdgeDescriptor<int>(3, (0, 13), (1, 13), (2, 19), (4, 19)),
            new EdgeDescriptor<int>(4, (0, 22), (1, 13), (2, 14), (3, 19)),
        };

        // Rail
        public static EdgeDescriptor<int>[] Rail => new[]
        {
            new EdgeDescriptor<int>(0, (1, 10)),
            new EdgeDescriptor<int>(1, (0, 10), (2, 10), (6, 8), (7, 13)),
            new EdgeDescriptor<int>(2, (1, 10), (3, 10), (7, 8), (8, 13)),
            new EdgeDescriptor<int>(3, (2, 10), (4, 10), (8, 8)),
            new EdgeDescriptor<int>(4, (3, 10)),
            new EdgeDescriptor<int>(5, (6, 10)),
            new EdgeDescriptor<int>(6, (5, 10), (1, 8), (7, 10)),
            new EdgeDescriptor<int>(7, (6, 10), (8, 11), (1, 13), (2, 8)),
            new EdgeDescriptor<int>(8, (7, 10), (9, 10), (2, 13), (3, 8)),
            new EdgeDescriptor<int>(9, (8, 10)),
        };

        // Rail - 0 disconnected
        public static EdgeDescriptor<int>[] Rail_0_Disconnected => new[]
        {
            new EdgeDescriptor<int>(0),
            new EdgeDescriptor<int>(1, (2, 10), (6, 8), (7, 13)),
            new EdgeDescriptor<int>(2, (1, 10), (3, 10), (7, 8), (8, 13)),
            new EdgeDescriptor<int>(3, (2, 10), (4, 10), (8, 8)),
            new EdgeDescriptor<int>(4, (3, 10)),
            new EdgeDescriptor<int>(5, (6, 10)),
            new EdgeDescriptor<int>(6, (5, 10), (1, 8), (7, 10)),
            new EdgeDescriptor<int>(7, (6, 10), (8, 11), (1, 13), (2, 8)),
            new EdgeDescriptor<int>(8, (7, 10), (9, 10), (2, 13), (3, 8)),
            new EdgeDescriptor<int>(9, (8, 10)),
        };

        // Rail - 5 disconnected
        public static EdgeDescriptor<int>[] Rail_5_Disconnected => new[]
        {
            new EdgeDescriptor<int>(0, (1, 10)),
            new EdgeDescriptor<int>(1, (0, 10), (2, 10), (6, 8), (7, 13)),
            new EdgeDescriptor<int>(2, (1, 10), (3, 10), (7, 8), (8, 13)),
            new EdgeDescriptor<int>(3, (2, 10), (4, 10), (8, 8)),
            new EdgeDescriptor<int>(4, (3, 10)),
            new EdgeDescriptor<int>(5),
            new EdgeDescriptor<int>(6, (1, 8), (7, 10)),
            new EdgeDescriptor<int>(7, (6, 10), (8, 11), (1, 13), (2, 8)),
            new EdgeDescriptor<int>(8, (7, 10), (9, 10), (2, 13), (3, 8)),
            new EdgeDescriptor<int>(9, (8, 10)),
        };

        // Rail - Symmetric
        public static EdgeDescriptor<int>[] Rail_Symmetric => new[]
        {
            new EdgeDescriptor<int>(0, (1, 10)),
            new EdgeDescriptor<int>(1, (2, 10), (6, 8)),
            new EdgeDescriptor<int>(2, (1, 10), (6, 13)),
            new EdgeDescriptor<int>(5, (6, 10)),
            new EdgeDescriptor<int>(6, (1, 8), (2, 13), (5, 10)),
            new EdgeDescriptor<int>(3, (4, 10), (7, 13), (8, 8)),
            new EdgeDescriptor<int>(4, (3, 10)),
            new EdgeDescriptor<int>(7, (3, 13), (8, 10)),
            new EdgeDescriptor<int>(8, (3, 8), (7, 10), (9, 10)),
            new EdgeDescriptor<int>(9, (8, 10))

        };

        // Tessellation
        public static EdgeDescriptor<int>[] Tessellation => new[]
        {
            new EdgeDescriptor<int>(0, (1, 8), (2, 12)),
            new EdgeDescriptor<int>(1, (0, 8), (2, 13), (3, 25), (4, 9)),
            new EdgeDescriptor<int>(2, (0, 12), (1, 13), (3, 14), (6, 21)),
            new EdgeDescriptor<int>(3, (1, 25), (2, 14), (4, 20), (5, 8), (6, 12), (7, 12), (8, 16)),
            new EdgeDescriptor<int>(4, (1, 9), (3, 20), (5, 19)),
            new EdgeDescriptor<int>(5, (3, 8), (4, 19), (7, 11)),
            new EdgeDescriptor<int>(6, (2, 21), (3, 12), (8, 11)),
            new EdgeDescriptor<int>(7, (3, 12), (5, 11), (8, 9)),
            new EdgeDescriptor<int>(8, (3, 16), (6, 11), (7, 9)),
        };

        // From 0 to 1 Data Structures
        public static EdgeDescriptor<int>[] Ds0Prims => new[]
        {
            new EdgeDescriptor<int>(0, (1, 2), (2, 3)),
            new EdgeDescriptor<int>(1, (0, 2), (3, 2), (4, 5)),
            new EdgeDescriptor<int>(2, (0, 3), (4, 6)),
            new EdgeDescriptor<int>(3, (1, 2), (4, 4)),
            new EdgeDescriptor<int>(4, (1, 5), (2, 6), (3, 4)),
        };

        public static EdgeDescriptor<int>[] Ds1Prims => new[]
        {
            new EdgeDescriptor<int>(0, (1, 3), (2, 15), (4, 5)),
            new EdgeDescriptor<int>(1, (0, 3), (2, 2), (4, 5), (5, 8)),
            new EdgeDescriptor<int>(2, (0, 15), (1, 2), (5, 9)),
            new EdgeDescriptor<int>(3, (4, 11), (5, 4)),
            new EdgeDescriptor<int>(4, (0, 5), (1, 5), (3, 11), (5, 4)),
            new EdgeDescriptor<int>(5, (1, 8), (2, 9), (3, 4), (4, 4))
        };
    }
}