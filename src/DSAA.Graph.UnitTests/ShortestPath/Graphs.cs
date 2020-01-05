using DSAA.Graph.UnitTests.Helpers;

namespace DSAA.Graph.UnitTests.ShortestPath
{
    /// <summary>
    /// Examples taken from: https://visualgo.net/en/sssp
    /// </summary>
    public static class Graphs
    {
        // CP3 4.3 U/U
        public static EdgeDescriptor<int>[] CP3_43_UU => new[]
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

        // CP3 4.4 D/U
        public static EdgeDescriptor<int>[] CP3_44_DU => new[]
        {
            new EdgeDescriptor<int>(0, 1, 2),
            new EdgeDescriptor<int>(1, 2, 3),
            new EdgeDescriptor<int>(2, 3, 5),
            new EdgeDescriptor<int>(3, 4),
            new EdgeDescriptor<int>(4),
            new EdgeDescriptor<int>(5),
            new EdgeDescriptor<int>(6),
            new EdgeDescriptor<int>(7, 6)
        };

        // CP3 4.17 D/W
        public static EdgeDescriptor<int>[] CP3_417_DW => new[]
        {
            new EdgeDescriptor<int>(0, (1, 2), (2, 6), (3, 7)),
            new EdgeDescriptor<int>(1, (3, 3), (4, 6)),
            new EdgeDescriptor<int>(2, (4, 1)),
            new EdgeDescriptor<int>(3, (4, 5)),
            new EdgeDescriptor<int>(4)
        };

        // CP3 4.18 -ve weight
        public static EdgeDescriptor<int>[] CP3_418_ve_weight => new[]
        {
            new EdgeDescriptor<int>(0, (1, 1), (2, 10)),
            new EdgeDescriptor<int>(1, (3, 2)),
            new EdgeDescriptor<int>(2, (3, -10)),
            new EdgeDescriptor<int>(3, (4, 3)),
            new EdgeDescriptor<int>(4)
        };

        // CP3 4.19 -ve cycle
        public static EdgeDescriptor<int>[] CP3_419_ve_cycle => new[]
        {
            new EdgeDescriptor<int>(0, (1, 99), (4, -99)),
            new EdgeDescriptor<int>(1, (2, 15)),
            new EdgeDescriptor<int>(2, (1, -42), (3, 10)),
            new EdgeDescriptor<int>(3),
            new EdgeDescriptor<int>(4)
        };

        // CP3 4.40 Tree
        public static EdgeDescriptor<int>[] CP3_440_Tree => new[]
        {
            new EdgeDescriptor<int>(0, (1, 2), (5, 4)),
            new EdgeDescriptor<int>(1, (0, 2), (3, 9)),
            new EdgeDescriptor<int>(2, (3, 5)),
            new EdgeDescriptor<int>(3, (1, 9), (2, 5), (4, 1)),
            new EdgeDescriptor<int>(4, (3, 1)),
            new EdgeDescriptor<int>(5, (0, 4))
        };

        // Bellman Ford's Killer
        public static EdgeDescriptor<int>[] BellmanFordsKiller => new[]
        {
            new EdgeDescriptor<int>(0, (1, 6)),
            new EdgeDescriptor<int>(1, (2, 5)),
            new EdgeDescriptor<int>(2, (3, 4)),
            new EdgeDescriptor<int>(3, (4, 3)),
            new EdgeDescriptor<int>(4, (5, 2)),
            new EdgeDescriptor<int>(5, (6, 1)),
            new EdgeDescriptor<int>(6)
        };

        // Dijkstra's Killer
        public static EdgeDescriptor<int>[] DijkstrasKiller => new[]
        {
            new EdgeDescriptor<int>(0, (1, 16), (2, 0)),
            new EdgeDescriptor<int>(1, (2, -32)),
            new EdgeDescriptor<int>(2, (3, 8), (4, 0)),
            new EdgeDescriptor<int>(3, (4, -16)),
            new EdgeDescriptor<int>(4, (5, 4), (6, 0)),
            new EdgeDescriptor<int>(5, (6, -8)),
            new EdgeDescriptor<int>(6, (7, 2), (8, 0)),
            new EdgeDescriptor<int>(7, (8, -4)),
            new EdgeDescriptor<int>(8, (9, 1), (10, 0)),
            new EdgeDescriptor<int>(9, (10, -2)),
            new EdgeDescriptor<int>(10)
        };

        // DAG
        public static EdgeDescriptor<int>[] Dag => new[]
        {
            new EdgeDescriptor<int>(0, (1 ,1), (2, 7)),
            new EdgeDescriptor<int>(1, (3, 9), (5, 15)),
            new EdgeDescriptor<int>(2, (4, 4)),
            new EdgeDescriptor<int>(3, (4, 10), (5, 5)),
            new EdgeDescriptor<int>(4, (5, 3)),
            new EdgeDescriptor<int>(5)
        };

        // From 0 to 1 Data Structures - Bellman
        public static EdgeDescriptor<int>[] Ds0DijkstraMultiplePath => new[]
        {
            new EdgeDescriptor<int>(0, (1 ,5), (2, 1)),
            new EdgeDescriptor<int>(1, (3, 3), (4, 5)),
            new EdgeDescriptor<int>(2, (4, 3)),
            new EdgeDescriptor<int>(3),
            new EdgeDescriptor<int>(4, (3, 4))
        };  
        public static EdgeDescriptor<int>[] Ds0BellmanMultiplePath => new[]
        {
            new EdgeDescriptor<int>(0, (1 , -1)),
            new EdgeDescriptor<int>(1, (3, 3), (4, 5)),
            new EdgeDescriptor<int>(2, (0, 1), (4, -1)),
            new EdgeDescriptor<int>(3),
            new EdgeDescriptor<int>(4, (3, 4))
        };   
        
        public static EdgeDescriptor<int>[] Ds0Bellman => new[]
        {
            new EdgeDescriptor<int>(0, (1 ,2), (2, 1)),
            new EdgeDescriptor<int>(1, (3, 3), (4, -2)),
            new EdgeDescriptor<int>(2, (1, -5), (4, 2)),
            new EdgeDescriptor<int>(3),
            new EdgeDescriptor<int>(4, (3, 1))
        };

        public static EdgeDescriptor<int>[] Ds1BellmanCycle => new[]
        {
            new EdgeDescriptor<int>(0, (1 ,2), (2, 3)),
            new EdgeDescriptor<int>(1, (4, -5)),
            new EdgeDescriptor<int>(2, (4, 6)),
            new EdgeDescriptor<int>(3, (1, 2)),
            new EdgeDescriptor<int>(4, (3, -4))
        };

        public static EdgeDescriptor<int>[] Ds2BellmanCycle => new[]
        {
            new EdgeDescriptor<int>(0, (1 ,2)),
            new EdgeDescriptor<int>(1, (2 ,2)),
            new EdgeDescriptor<int>(2, (3 ,2)),
            new EdgeDescriptor<int>(3, (4 ,2), (6, -2)),
            new EdgeDescriptor<int>(4, (5 ,2)),
            new EdgeDescriptor<int>(5),
            new EdgeDescriptor<int>(6, (2, -2))
        };

        // https://stackoverflow.com/questions/13159337/why-doesnt-dijkstras-algorithm-work-for-negative-weight-edges
        public static EdgeDescriptor<int>[] StackBellman => new[]
        {
            new EdgeDescriptor<int>(0, (1 ,5), (2, 2)),
            new EdgeDescriptor<int>(1, (2 , -10)),
            new EdgeDescriptor<int>(2),
        };
    }
}