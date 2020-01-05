using System;
using System.Collections.Generic;
using DSAA.Graph;
using DSAA.Graph.Fluent;
using DSAA.Graph.ShortestPath;
using DSAA.Graph.ShortestPath.Fluent;
using DSAA.Graph.ShortestPath.Strategy;
using DSAA.Graph.Sort;
using DSAA.List.Sort;
using DSAA.Tree;
using DSAA.Tree.BinarySearch;

namespace DSAA.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var collection = new List<int> {2, 5, 3, 1, 4};
            var sorted = collection.Sort(o => o.UseQuickSort<int, IntComparer>());

            // Breadth first - first all level
            // Depth first - reach as deep as possible to a leaf node
            var dictionary = new Dictionary<int, string>
            {
                { 2, "B"  }, 
                { 1, "A" }, 
                { 3, "C" }
            };
                
            var tree = dictionary.ToImmutableBinarySearchTree();
           // var result = tree.Find(1);
           // tree.Insert()
           // tree.Delete()
           //var result =  tree.Find(2);


            //bTree.Delete();
            //bTree.Insert();
            //bTree.Find();
            //bTree.Root();
            //bTree.IsEmpty
            var bResults = tree.Traverse(o => o.BreadthFirst()); // Returns values in Order!???
            var dpreResults = tree.Traverse(o => o.DepthFirst().PreOrder()); // - pre post in - refer to when the parent node is processed relative to its children.
            var dinResults = tree.Traverse(o => o.DepthFirst().InOrder());
            var dpostResults = tree.Traverse(o => o.DepthFirst().PostOrder());

            // bst is same as btree but with extra constraints (ordered binary tree) => BST - 
            // for every node in the tree
            //  each node in the left sub-tree of that node has a value that is LESS THAN OR EQUAL TO THE VALUE of the node
            //  each node in the right sub-tree of that node has a value greater than the value of the node
            // FAST
            //  Insertion
            //  Lookups

            //var g = new AdjacencySetGraph<int>(GraphType.Directed, comparer);
            //var g = new AdjacencyMatrixGraph<int>(GraphType.Directed, comparer, 10);

            var graph1 = GraphBuilder.Create<int>(b => b.Directed().WellConnected());
            var graph2 = GraphBuilder.Create<int>(b => b.Undirected().WellConnected());
            var graph3 = GraphBuilder.Create<int>(b => b.Directed().Sparse());
            var graph4 = GraphBuilder.Create<int>(b => b.Undirected().Sparse());

            var graph1comaprer = GraphBuilder.Create<int>(b => b.Directed().WellConnected().CompareUsing<IntComparer>());
            var graph2comaprer = GraphBuilder.Create<int>(b => b.Undirected().WellConnected().CompareUsing<IntComparer>());
            var graph3comaprer = GraphBuilder.Create<int>(b => b.Directed().Sparse().CompareUsing<IntComparer>());
            var graph4comaprer = GraphBuilder.Create<int>(b => b.Undirected().Sparse().CompareUsing<IntComparer>());

            var graph1comaprerInstance = GraphBuilder.Create<int>(b => b.Directed().WellConnected().CompareUsing(new IntComparer()));
            var graph2comaprerInstance = GraphBuilder.Create<int>(b => b.Undirected().WellConnected().CompareUsing(new IntComparer()));
            var graph3comaprerInstance = GraphBuilder.Create<int>(b => b.Directed().Sparse().CompareUsing(new IntComparer()));
            var graph4comaprerInstance = GraphBuilder.Create<int>(b => b.Undirected().Sparse().CompareUsing(new IntComparer()));

            var graph1comaprerLambda = GraphBuilder.Create<int>(b => b.Directed().WellConnected().CompareUsing((x, y) => x == y));
            var graph2comaprerLambda = GraphBuilder.Create<int>(b => b.Undirected().WellConnected().CompareUsing((x, y) => x == y));
            var graph3comaprerLambda = GraphBuilder.Create<int>(b => b.Directed().Sparse().CompareUsing((x, y) => x == y));
            var graph4comaprerLambda = GraphBuilder.Create<int>(b => b.Undirected().Sparse().CompareUsing((x, y) => x == y));

            var graphbResults = graph1.Traverse(o => o.BreadthFirst(), 1); // Returns values in Order!???
            var graphdpreResults = graph1.Traverse(o => o.DepthFirst(), 1); // - pre post in - refer to when the parent node is processed relative to its children.

            var sortedGraph = graph1.Sort(o => o.UseTopologicalSort());
            var path1 = graph1.FindShortestPath(1, 5, o => o.UseUnweightedGraph());
            var path2 = graph1.FindShortestPath(1, 5, o => o.UseDijkstra());
            var path3 = graph1.FindShortestPath(1, 5, o => o.UseBellmanFord());
            var path4 = graph1.FindShortestPath(1, 5);
            var spTree1 = graph1.FindSpanningTree(o => o.UsePrims());
            var spTree2 = graph1.FindSpanningTree(o => o.UseKruskals());
            var spTree3 = graph1.FindSpanningTree(); // <- auto selects based on graph properties

            // https://stackoverflow.com/questions/1195872/when-should-i-use-kruskal-as-opposed-to-prim-and-vice-versa

            //Console.WriteLine($"Sorted collection: {string.Join(", ", sorted)}");


            graph1
                .AddEdge(0, 1, 2)
                .AddEdge(0, 2, 1)
                .AddEdge(1, 3, 3)
                .AddEdge(1, 4, -2)
                .AddEdge(2, 1, -5)
                .AddEdge(2, 4, 2)
                .AddVertex(3)
                .AddEdge(4, 3, 1);

            PrintTable(0, graph1);
            PrintTable(1, graph1);
            PrintTable(2, graph1);
            PrintTable(3, graph1);
            PrintTable(4, graph1);

        }

        private static void PrintTable(int source, IGraph<int> graph)
        {
            var bellman = new BellmanFordsStrategy<int>();
            var table = bellman.BuildDistanceTable(graph, source);

            Console.WriteLine(string.Empty);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Source vertex => {source}");

            foreach (var info in table)
            {
                Console.WriteLine($"{info.Key} => {info.Value}");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}