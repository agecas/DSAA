using System.Collections.Generic;
using System.Linq;
using DSAA.Graph.Domain;
using DSAA.Graph.ShortestPath;
using DSAA.Graph.UnitTests.Helpers;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Graph.UnitTests.ShortestPath.Strategy
{
    public sealed class ShortestPathForWeightedGraphUsingBellmanFordTests
    {
        private static readonly IEqualityComparer<int> Comparer = new IntComparer();

        private IGraph<int> Sut { get; } = new AdjacencySetGraph<int>(GraphType.Directed, Comparer);

        public static IEnumerable<object[]> ReachableDestinationTests =>
            new List<object[]>
            {
                // CP3 4.3 U/U
                new object[] { 0, 0, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1 }, 1, Comparer) },
                new object[] { 0, 2, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2 }, 2, Comparer) },
                new object[] { 0, 3, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 3 }, 3, Comparer) },
                new object[] { 0, 4, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 4 }, 1, Comparer) },
                new object[] { 0, 5, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 5 }, 2, Comparer) },
                new object[] { 0, 6, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 6 }, 3, Comparer) },
                new object[] { 0, 7, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 3, 7 }, 4, Comparer) },
                new object[] { 0, 8, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 4, 8 }, 2, Comparer) },
                new object[] { 0, 9, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 4, 8, 9 }, 3, Comparer) },
                new object[] { 0, 10, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 5, 10 }, 3, Comparer) },
                new object[] { 0, 11, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 6, 11 }, 4, Comparer) },
                new object[] { 0, 12, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 0, 1, 2, 3, 7, 12 }, 5, Comparer) },

                new object[] { 6, 0, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 1, 0 }, 3, Comparer) },
                new object[] { 6, 1, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 1 }, 2, Comparer) },
                new object[] { 6, 2, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2 }, 1, Comparer) },
                new object[] { 6, 3, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 3 }, 2, Comparer) },
                new object[] { 6, 4, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 2, 1, 0, 4}, 4, Comparer) },
                new object[] { 6, 5, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 5 }, 1, Comparer) },
                new object[] { 6, 6, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6 }, 0, Comparer) },
                new object[] { 6, 7, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 11, 12, 7 }, 3, Comparer) },
                new object[] { 6, 8, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 11, 10, 9, 8 }, 4, Comparer) },
                new object[] { 6, 9, Graphs.CP3_43_UU, new GraphPath<int>(new []  { 6, 11, 10, 9 }, 3, Comparer) },
                new object[] { 6, 10, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 6, 11, 10 }, 2, Comparer) },
                new object[] { 6, 11, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 6, 11 }, 1, Comparer) },
                new object[] { 6, 12, Graphs.CP3_43_UU, new GraphPath<int>(new [] { 6, 11, 12 }, 2, Comparer) },

                // CP3 4.4 D/U
                new object[] { 0, 0, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 1 }, 1, Comparer) },
                new object[] { 0, 2, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 2 }, 1, Comparer) },
                new object[] { 0, 3, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 1, 3 }, 2, Comparer) },
                new object[] { 0, 4, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 1, 3, 4 }, 3, Comparer) },
                new object[] { 0, 5, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 0, 2, 5 }, 2, Comparer) },

                new object[] { 2, 2, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 3, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2, 3 }, 1, Comparer) },
                new object[] { 2, 4, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2, 3, 4 }, 2, Comparer) },
                new object[] { 2, 5, Graphs.CP3_44_DU, new GraphPath<int>(new [] { 2, 5 }, 1, Comparer) },

                // CP3 4.17 D/W
                new object[] { 0, 0, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 0, 1 }, 2, Comparer) },
                new object[] { 0, 2, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 0, 2 }, 6, Comparer) },
                new object[] { 0, 3, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 0, 1, 3 }, 5, Comparer) },
                new object[] { 0, 4, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 0, 2, 4 }, 7, Comparer) },

                new object[] { 1, 1, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 3, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 1, 3 }, 3, Comparer) },
                new object[] { 1, 4, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 1, 4 }, 6, Comparer) },

                new object[] { 2, 2, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 4, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 2, 4 }, 1, Comparer) },

                new object[] { 3, 3, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 3 }, 0, Comparer) },
                new object[] { 3, 4, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 3, 4 }, 5, Comparer) },

                new object[] { 4, 4, Graphs.CP3_417_DW, new GraphPath<int>(new [] { 4 }, 0, Comparer) },
                
                // CP3 4.40 Tree
                new object[] { 0, 0, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 0, 1 }, 2, Comparer) },
                new object[] { 0, 2, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 0, 1, 3, 2 }, 16, Comparer) },
                new object[] { 0, 3, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 0, 1, 3 }, 11, Comparer) },
                new object[] { 0, 4, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 0, 1, 3, 4 }, 12, Comparer) },
                new object[] { 0, 5, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 0, 5 }, 4, Comparer) },

                new object[] { 1, 1, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 2, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 1, 3, 2 }, 14, Comparer) },
                new object[] { 1, 3, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 1, 3 }, 9, Comparer) },
                new object[] { 1, 4, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 1, 3, 4 }, 10, Comparer) },

                new object[] { 2, 2, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 2 }, 0, Comparer) },

                new object[] { 3, 3, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 3 }, 0, Comparer) },
                new object[] { 3, 2, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 3, 2 }, 5, Comparer) },
                new object[] { 3, 4, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 3, 4 }, 1, Comparer) },

                new object[] { 4, 4, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 4 }, 0, Comparer) },

                new object[] { 5, 5, Graphs.CP3_440_Tree, new GraphPath<int>(new [] { 5 }, 0, Comparer) },
                
                // Dijkstra's Killer
                new object[] { 0, 0, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1 }, 16, Comparer) },
                new object[] { 0, 2, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2 }, -16, Comparer) },
                new object[] { 0, 3, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3 }, -8, Comparer) },
                new object[] { 0, 4, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4 }, -24, Comparer) },
                new object[] { 0, 5, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5 }, -20, Comparer) },
                new object[] { 0, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5, 6 }, -28, Comparer) },
                new object[] { 0, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5, 6, 7 }, -26, Comparer) },
                new object[] { 0, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, -30, Comparer) },
                new object[] { 0, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, -29, Comparer) },
                new object[] { 0, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, -31, Comparer) },

                new object[] { 1, 1, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 2, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2 }, -32, Comparer) },
                new object[] { 1, 3, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3 }, -24, Comparer) },
                new object[] { 1, 4, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4 }, -40, Comparer) },
                new object[] { 1, 5, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5 }, -36, Comparer) },
                new object[] { 1, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5, 6 }, -44, Comparer) },
                new object[] { 1, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5, 6, 7 }, -42, Comparer) },
                new object[] { 1, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5, 6, 7, 8 }, -46, Comparer) },
                new object[] { 1, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, -45, Comparer) },
                new object[] { 1, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, -47, Comparer) },

                new object[] { 2, 2, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 3, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3 }, 8, Comparer) },
                new object[] { 2, 4, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4 }, -8, Comparer) },
                new object[] { 2, 5, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4, 5 }, -4, Comparer) },
                new object[] { 2, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4, 5, 6 }, -12, Comparer) },
                new object[] { 2, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4, 5, 6, 7 }, -10, Comparer) },
                new object[] { 2, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4, 5, 6, 7, 8 }, -14, Comparer) },
                new object[] { 2, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4, 5, 6, 7, 8, 9 }, -13, Comparer) },
                new object[] { 2, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, -15, Comparer) },

                new object[] { 3, 3, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3 }, 0, Comparer) },
                new object[] { 3, 4, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4 }, -16, Comparer) },
                new object[] { 3, 5, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4, 5 }, -12, Comparer) },
                new object[] { 3, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4, 5, 6 }, -20, Comparer) },
                new object[] { 3, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4, 5, 6, 7 }, -18, Comparer) },
                new object[] { 3, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4, 5, 6, 7, 8 }, -22, Comparer) },
                new object[] { 3, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4, 5, 6, 7, 8, 9 }, -21, Comparer) },
                new object[] { 3, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 3, 4, 5, 6, 7, 8, 9, 10 }, -23, Comparer) },

                new object[] { 4, 4, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4 }, 0, Comparer) },
                new object[] { 4, 5, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4, 5 }, 4, Comparer) },
                new object[] { 4, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4, 5, 6 }, -4, Comparer) },
                new object[] { 4, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4, 5, 6, 7 }, -2, Comparer) },
                new object[] { 4, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4, 5, 6, 7, 8 }, -6, Comparer) },
                new object[] { 4, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4, 5, 6, 7, 8, 9 }, -5, Comparer) },
                new object[] { 4, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 4, 5, 6, 7, 8, 9, 10 }, -7, Comparer) },

                new object[] { 5, 5, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 5 }, 0, Comparer) },
                new object[] { 5, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 5, 6 }, -8, Comparer) },
                new object[] { 5, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 5, 6, 7 }, -6, Comparer) },
                new object[] { 5, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 5, 6, 7, 8 }, -10, Comparer) },
                new object[] { 5, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 5, 6, 7, 8, 9 }, -9, Comparer) },
                new object[] { 5, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 5, 6, 7, 8, 9, 10 }, -11, Comparer) },

                new object[] { 6, 6, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 6 }, 0, Comparer) },
                new object[] { 6, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 6, 7 }, 2, Comparer) },
                new object[] { 6, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 6, 7, 8 }, -2, Comparer) },
                new object[] { 6, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 6, 7, 8, 9 }, -1, Comparer) },
                new object[] { 6, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 6, 7, 8, 9, 10 }, -3, Comparer) },

                new object[] { 7, 7, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 7 }, 0, Comparer) },
                new object[] { 7, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 7, 8 }, -4, Comparer) },
                new object[] { 7, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 7, 8, 9 }, -3, Comparer) },
                new object[] { 7, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 7, 8, 9, 10 }, -5, Comparer) },

                new object[] { 8, 8, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 8 }, 0, Comparer) },
                new object[] { 8, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 8, 9 }, 1, Comparer) },
                new object[] { 8, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 8, 9, 10 }, -1, Comparer) },

                new object[] { 9, 9, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 9 }, 0, Comparer) },
                new object[] { 9, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 9, 10 }, -2, Comparer) },

                new object[] { 10, 10, Graphs.DijkstrasKiller, new GraphPath<int>(new [] { 10 }, 0, Comparer) },

                // CP3 4.18 -ve weight
                new object[] { 0, 0, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 0}, 0, Comparer) },
                new object[] { 0, 1, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 0, 1 }, 1, Comparer) },
                new object[] { 0, 2, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 0, 2 }, 10, Comparer) },
                new object[] { 0, 3, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 0, 2, 3 }, 0, Comparer) },
                new object[] { 0, 4, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 0, 2, 3, 4 }, 3, Comparer) },

                new object[] { 1, 1, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 3, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 1, 3 }, 2, Comparer) },
                new object[] { 1, 4, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 1, 3, 4 }, 5, Comparer) },

                new object[] { 2, 2, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 3, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 2, 3 }, -10, Comparer) },
                new object[] { 2, 4, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 2, 3, 4 }, -7, Comparer) },

                new object[] { 3, 3, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 3 }, 0, Comparer) },
                new object[] { 3, 4, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 3, 4 }, 3, Comparer) },

                new object[] { 4, 4, Graphs.CP3_418_ve_weight, new GraphPath<int>(new [] { 4 }, 0, Comparer) },

                // Bellman Ford's Killer
                new object[] { 0, 0, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0, 1 }, 6, Comparer) },
                new object[] { 0, 2, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0, 1, 2 }, 11, Comparer) },
                new object[] { 0, 3, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0, 1, 2, 3 }, 15, Comparer) },
                new object[] { 0, 4, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4 }, 18, Comparer) },
                new object[] { 0, 5, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5 }, 20, Comparer) },
                new object[] { 0, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 0, 1, 2, 3, 4, 5, 6 }, 21, Comparer) },

                new object[] { 1, 1, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 2, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 1, 2 }, 5, Comparer) },
                new object[] { 1, 3, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 1, 2, 3 }, 9, Comparer) },
                new object[] { 1, 4, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 1, 2, 3, 4 }, 12, Comparer) },
                new object[] { 1, 5, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5 }, 14, Comparer) },
                new object[] { 1, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 1, 2, 3, 4, 5, 6 }, 15, Comparer) },

                new object[] { 2, 2, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 3, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 2, 3 }, 4, Comparer) },
                new object[] { 2, 4, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 2, 3, 4 }, 7, Comparer) },
                new object[] { 2, 5, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 2, 3, 4, 5 }, 9, Comparer) },
                new object[] { 2, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 2, 3, 4, 5, 6 }, 10, Comparer) },

                new object[] { 3, 3, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 3 }, 0, Comparer) },
                new object[] { 3, 4, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 3, 4 }, 3, Comparer) },
                new object[] { 3, 5, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 3, 4, 5 }, 5, Comparer) },
                new object[] { 3, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 3, 4, 5, 6 }, 6, Comparer) },

                new object[] { 4, 4, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 4 }, 0, Comparer) },
                new object[] { 4, 5, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 4, 5 }, 2, Comparer) },
                new object[] { 4, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 4, 5, 6 }, 3, Comparer) },

                new object[] { 5, 5, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 5 }, 0, Comparer) },
                new object[] { 5, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 5, 6 }, 1, Comparer) },

                new object[] { 6, 6, Graphs.BellmanFordsKiller, new GraphPath<int>(new [] { 6 }, 0, Comparer) },
                
                // DAG
                new object[] { 0, 0, Graphs.Dag, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.Dag, new GraphPath<int>(new [] { 0, 1 }, 1, Comparer) },
                new object[] { 0, 2, Graphs.Dag, new GraphPath<int>(new [] { 0, 2 }, 7, Comparer) },
                new object[] { 0, 3, Graphs.Dag, new GraphPath<int>(new [] { 0, 1, 3 }, 10, Comparer) },
                new object[] { 0, 4, Graphs.Dag, new GraphPath<int>(new [] { 0, 2, 4 }, 11, Comparer) },
                new object[] { 0, 5, Graphs.Dag, new GraphPath<int>(new [] { 0, 2, 4, 5 }, 14, Comparer) },

                new object[] { 1, 1, Graphs.Dag, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 3, Graphs.Dag, new GraphPath<int>(new [] { 1, 3 }, 9, Comparer) },
                new object[] { 1, 4, Graphs.Dag, new GraphPath<int>(new [] { 1, 3, 4 }, 19, Comparer) },
                new object[] { 1, 5, Graphs.Dag, new GraphPath<int>(new [] { 1, 3, 5 }, 14, Comparer) },

                new object[] { 2, 2, Graphs.Dag, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 4, Graphs.Dag, new GraphPath<int>(new [] { 2, 4 }, 4, Comparer) },
                new object[] { 2, 5, Graphs.Dag, new GraphPath<int>(new [] { 2, 4, 5 }, 7, Comparer) },

                new object[] { 3, 3, Graphs.Dag, new GraphPath<int>(new [] { 3 }, 0, Comparer) },
                new object[] { 3, 4, Graphs.Dag, new GraphPath<int>(new [] { 3, 4 }, 10, Comparer) },
                new object[] { 3, 5, Graphs.Dag, new GraphPath<int>(new [] { 3, 5 }, 5, Comparer) },

                new object[] { 4, 4, Graphs.Dag, new GraphPath<int>(new [] { 4 }, 0, Comparer) },
                new object[] { 4, 5, Graphs.Dag, new GraphPath<int>(new [] { 4, 5 }, 3, Comparer) },

                new object[] { 5, 5, Graphs.Dag, new GraphPath<int>(new [] { 5 }, 0, Comparer) },
                
                // From 0 to 1 Data Structures - Bellman
                new object[] { 0, 0, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 0 }, 0, Comparer) },
                new object[] { 0, 1, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 0, 2, 1 }, -4, Comparer) },
                new object[] { 0, 2, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 0, 2 }, 1, Comparer) },
                new object[] { 0, 3, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 0, 2, 1, 4, 3 }, -5, Comparer) },
                new object[] { 0, 4, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 0, 2, 1, 4 }, -6, Comparer) },
                
                new object[] { 1, 1, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 1 }, 0, Comparer) },
                new object[] { 1, 3, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 1, 4, 3 }, -1, Comparer) },
                new object[] { 1, 4, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 1, 4 }, -2, Comparer) },

                new object[] { 2, 2, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 2 }, 0, Comparer) },
                new object[] { 2, 1, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 2, 1 }, -5, Comparer) },
                new object[] { 2, 3, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 2, 1, 4, 3 }, -6, Comparer) },
                new object[] { 2, 4, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 2, 1, 4 }, -7, Comparer) },

                new object[] { 3, 3, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 3 }, 0, Comparer) },

                new object[] { 4, 3, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 4, 3 }, 1, Comparer) },
                new object[] { 4, 4, Graphs.Ds0Bellman, new GraphPath<int>(new [] { 4 }, 0, Comparer) },
                
                // https://stackoverflow.com/questions/13159337/why-doesnt-dijkstras-algorithm-work-for-negative-weight-edges
                new object[] { 0, 2, Graphs.StackBellman, new GraphPath<int>(new [] { 0, 1, 2 }, -5, Comparer) },
            };

        public static IEnumerable<object[]> UnreachableDestinationTests =>
            new List<object[]>
            {
                // CP3 4.4 D/U
                new object[] { 3, 0, Graphs.CP3_44_DU },
                new object[] { 0, 6, Graphs.CP3_44_DU },
                new object[] { 0, 7, Graphs.CP3_44_DU },
                new object[] { 5, 3, Graphs.CP3_44_DU },
                new object[] { 4, 3, Graphs.CP3_44_DU },
                new object[] { 2, 1, Graphs.CP3_44_DU },

                // CP3 4.17 D/W
                new object[] { 1, 0, Graphs.CP3_417_DW },
                new object[] { 1, 2, Graphs.CP3_417_DW },
                new object[] { 2, 0, Graphs.CP3_417_DW },
                new object[] { 2, 1, Graphs.CP3_417_DW },
                new object[] { 2, 3, Graphs.CP3_417_DW },
                new object[] { 3, 0, Graphs.CP3_417_DW },
                new object[] { 3, 1, Graphs.CP3_417_DW },
                new object[] { 3, 2, Graphs.CP3_417_DW },
                new object[] { 4, 0, Graphs.CP3_417_DW },
                new object[] { 4, 1, Graphs.CP3_417_DW },
                new object[] { 4, 2, Graphs.CP3_417_DW },
                new object[] { 4, 3, Graphs.CP3_417_DW },

                // CP3 4.18 -ve weight
                new object[] { 1, 0, Graphs.CP3_418_ve_weight },
                new object[] { 1, 2, Graphs.CP3_418_ve_weight },
                new object[] { 2, 0, Graphs.CP3_418_ve_weight },
                new object[] { 2, 1, Graphs.CP3_418_ve_weight },
                new object[] { 3, 0, Graphs.CP3_418_ve_weight },
                new object[] { 3, 2, Graphs.CP3_418_ve_weight },
                new object[] { 4, 0, Graphs.CP3_418_ve_weight },
                new object[] { 4, 1, Graphs.CP3_418_ve_weight },
                new object[] { 4, 2, Graphs.CP3_418_ve_weight },
                new object[] { 4, 3, Graphs.CP3_418_ve_weight },

                // Bellman Ford's Killer
                new object[] { 1, 0, Graphs.BellmanFordsKiller },
                new object[] { 2, 0, Graphs.BellmanFordsKiller },
                new object[] { 2, 1, Graphs.BellmanFordsKiller },
                new object[] { 3, 0, Graphs.BellmanFordsKiller },
                new object[] { 3, 1, Graphs.BellmanFordsKiller },
                new object[] { 3, 2, Graphs.BellmanFordsKiller },
                new object[] { 4, 0, Graphs.BellmanFordsKiller },
                new object[] { 4, 1, Graphs.BellmanFordsKiller },
                new object[] { 4, 2, Graphs.BellmanFordsKiller },
                new object[] { 4, 3, Graphs.BellmanFordsKiller },
                new object[] { 5, 0, Graphs.BellmanFordsKiller },
                new object[] { 5, 1, Graphs.BellmanFordsKiller },
                new object[] { 5, 2, Graphs.BellmanFordsKiller },
                new object[] { 5, 3, Graphs.BellmanFordsKiller },
                new object[] { 5, 4, Graphs.BellmanFordsKiller },
                new object[] { 6, 0, Graphs.BellmanFordsKiller },
                new object[] { 6, 1, Graphs.BellmanFordsKiller },
                new object[] { 6, 2, Graphs.BellmanFordsKiller },
                new object[] { 6, 3, Graphs.BellmanFordsKiller },
                new object[] { 6, 4, Graphs.BellmanFordsKiller },
                new object[] { 6, 5, Graphs.BellmanFordsKiller },

                // Dijkstra's Killer
                new object[] { 1, 0, Graphs.DijkstrasKiller },
                new object[] { 2, 0, Graphs.DijkstrasKiller },
                new object[] { 2, 1, Graphs.DijkstrasKiller },
                new object[] { 3, 0, Graphs.DijkstrasKiller },
                new object[] { 3, 1, Graphs.DijkstrasKiller },
                new object[] { 3, 2, Graphs.DijkstrasKiller },
                new object[] { 4, 0, Graphs.DijkstrasKiller },
                new object[] { 4, 1, Graphs.DijkstrasKiller },
                new object[] { 4, 2, Graphs.DijkstrasKiller },
                new object[] { 4, 3, Graphs.DijkstrasKiller },
                new object[] { 5, 0, Graphs.DijkstrasKiller },
                new object[] { 5, 1, Graphs.DijkstrasKiller },
                new object[] { 5, 2, Graphs.DijkstrasKiller },
                new object[] { 5, 3, Graphs.DijkstrasKiller },
                new object[] { 5, 4, Graphs.DijkstrasKiller },
                new object[] { 6, 0, Graphs.DijkstrasKiller },
                new object[] { 6, 1, Graphs.DijkstrasKiller },
                new object[] { 6, 2, Graphs.DijkstrasKiller },
                new object[] { 6, 3, Graphs.DijkstrasKiller },
                new object[] { 6, 4, Graphs.DijkstrasKiller },
                new object[] { 6, 5, Graphs.DijkstrasKiller },
                new object[] { 7, 0, Graphs.DijkstrasKiller },
                new object[] { 7, 1, Graphs.DijkstrasKiller },
                new object[] { 7, 2, Graphs.DijkstrasKiller },
                new object[] { 7, 3, Graphs.DijkstrasKiller },
                new object[] { 7, 4, Graphs.DijkstrasKiller },
                new object[] { 7, 5, Graphs.DijkstrasKiller },
                new object[] { 7, 6, Graphs.DijkstrasKiller },
                new object[] { 8, 0, Graphs.DijkstrasKiller },
                new object[] { 8, 1, Graphs.DijkstrasKiller },
                new object[] { 8, 2, Graphs.DijkstrasKiller },
                new object[] { 8, 3, Graphs.DijkstrasKiller },
                new object[] { 8, 4, Graphs.DijkstrasKiller },
                new object[] { 8, 5, Graphs.DijkstrasKiller },
                new object[] { 8, 6, Graphs.DijkstrasKiller },
                new object[] { 8, 7, Graphs.DijkstrasKiller },
                new object[] { 9, 0, Graphs.DijkstrasKiller },
                new object[] { 9, 1, Graphs.DijkstrasKiller },
                new object[] { 9, 2, Graphs.DijkstrasKiller },
                new object[] { 9, 3, Graphs.DijkstrasKiller },
                new object[] { 9, 4, Graphs.DijkstrasKiller },
                new object[] { 9, 5, Graphs.DijkstrasKiller },
                new object[] { 9, 6, Graphs.DijkstrasKiller },
                new object[] { 9, 7, Graphs.DijkstrasKiller },
                new object[] { 9, 8, Graphs.DijkstrasKiller },
                new object[] { 10, 0, Graphs.DijkstrasKiller },
                new object[] { 10, 1, Graphs.DijkstrasKiller },
                new object[] { 10, 2, Graphs.DijkstrasKiller },
                new object[] { 10, 3, Graphs.DijkstrasKiller },
                new object[] { 10, 4, Graphs.DijkstrasKiller },
                new object[] { 10, 5, Graphs.DijkstrasKiller },
                new object[] { 10, 6, Graphs.DijkstrasKiller },
                new object[] { 10, 7, Graphs.DijkstrasKiller },
                new object[] { 10, 8, Graphs.DijkstrasKiller },
                new object[] { 10, 9, Graphs.DijkstrasKiller },

                // DAG
                new object[] { 1, 0, Graphs.Dag },
                new object[] { 1, 2, Graphs.Dag },
                new object[] { 2, 0, Graphs.Dag },
                new object[] { 2, 1, Graphs.Dag },
                new object[] { 3, 0, Graphs.Dag },
                new object[] { 3, 1, Graphs.Dag },
                new object[] { 3, 2, Graphs.Dag },
                new object[] { 4, 0, Graphs.Dag },
                new object[] { 4, 1, Graphs.Dag },
                new object[] { 4, 2, Graphs.Dag },
                new object[] { 4, 3, Graphs.Dag },
                new object[] { 5, 0, Graphs.Dag },
                new object[] { 5, 1, Graphs.Dag },
                new object[] { 5, 2, Graphs.Dag },
                new object[] { 5, 3, Graphs.Dag },
                new object[] { 5, 4, Graphs.Dag },

            };

        public static IEnumerable<object[]> CycleTests =>
            new List<object[]>
            {
                // CP3 4.19 -ve cycle
                new object[] { 0, 1, Graphs.CP3_419_ve_cycle },
                new object[] { 0, 2, Graphs.CP3_419_ve_cycle },
                new object[] { 0, 3, Graphs.CP3_419_ve_cycle },
                new object[] { 0, 4, Graphs.CP3_419_ve_cycle },

                new object[] { 1, 2, Graphs.CP3_419_ve_cycle },
                new object[] { 1, 3, Graphs.CP3_419_ve_cycle },
                new object[] { 1, 4, Graphs.CP3_419_ve_cycle },

                // From 0 to 1 Data Structures - Bellman
                new object[] { 0, 1, Graphs.Ds1BellmanCycle },
                new object[] { 0, 2, Graphs.Ds1BellmanCycle },
                new object[] { 0, 3, Graphs.Ds1BellmanCycle },
                new object[] { 0, 4, Graphs.Ds1BellmanCycle },

                new object[] { 1, 2, Graphs.Ds1BellmanCycle },
                new object[] { 1, 3, Graphs.Ds1BellmanCycle },
                new object[] { 1, 4, Graphs.Ds1BellmanCycle },

                // From 0 to 1 Data Structures - Bellman
                new object[] { 0, 1, Graphs.Ds2BellmanCycle },
                new object[] { 0, 2, Graphs.Ds2BellmanCycle },
                new object[] { 0, 3, Graphs.Ds2BellmanCycle },
                new object[] { 0, 4, Graphs.Ds2BellmanCycle },
                new object[] { 0, 5, Graphs.Ds2BellmanCycle },
                new object[] { 0, 6, Graphs.Ds2BellmanCycle },

                new object[] { 1, 2, Graphs.Ds2BellmanCycle },
                new object[] { 1, 3, Graphs.Ds2BellmanCycle },
                new object[] { 1, 4, Graphs.Ds2BellmanCycle },
                new object[] { 1, 5, Graphs.Ds2BellmanCycle },
                new object[] { 1, 6, Graphs.Ds2BellmanCycle },
            };

        public static IEnumerable<object[]> MultiplePathsTests =>
            new List<object[]>
            {
                // CP3 4.3 U/U
                new object[] { 2, 3, Graphs.Ds0BellmanMultiplePath, new GraphPath<int>(new [] { 2, 4, 3 }, 3, Comparer) },
            };


        [Theory]
        [MemberData(nameof(CycleTests))]
        public void Given_Graph_When_CycleIsDetected_Then_ReturnNone(int startingVertex,
            int destinationVertex, EdgeDescriptor<int>[] edges)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            var result =
                Sut.FindShortestPath(startingVertex, destinationVertex, o => o.UseBellmanFord());

            // Assert
            Assert.True(result.IsEmpty);
        }

        [Theory]
        [MemberData(nameof(ReachableDestinationTests))]
        public void Given_Graph_When_DestinationIsReachable_Then_ReturnPath(int startingVertex,
            int destinationVertex, EdgeDescriptor<int>[] edges, GraphPath<int> expectedPath)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            GraphPath<int> result =
                Sut.FindShortestPath(startingVertex, destinationVertex, o => o.UseBellmanFord());

            // Assert
            Assert.Equal(expectedPath, result);
        }

        [Theory]
        [MemberData(nameof(UnreachableDestinationTests))]
        public void Given_Graph_When_DestinationIsNotReachable_Then_ReturnNone(int startingVertex,
            int destinationVertex, EdgeDescriptor<int>[] edges)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            var result =
                Sut.FindShortestPath(startingVertex, destinationVertex, o => o.UseBellmanFord());

            // Assert
            Assert.True(result.IsEmpty);
        }

        [Theory]
        [MemberData(nameof(MultiplePathsTests))]
        public void Given_Graph_When_MultiplePathsWithSameCostFound_Then_ReturnPathWithMinimumEdgeCount(int startingVertex,
            int destinationVertex, EdgeDescriptor<int>[] edges, GraphPath<int> expectedPath)
        {
            // Arrange
            Sut.AddToGraph(edges);

            // Act
            var result =
                Sut.FindShortestPath(startingVertex, destinationVertex, o => o.UseBellmanFord()).Single();


            // Assert
            Assert.Equal(expectedPath, result);
        }
    }
}