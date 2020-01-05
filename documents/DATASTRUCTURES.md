# Data Structures

## Index

- [Home](../README.md)
- [Min/Max Heap](#min/max-heap)
- [Tree](#tree)
- - [Binary Search Tree](#binary-search-tree)
- - [AVL Tree](#avl-tree)
- [Graph](#graph)
- - [Topological Sort](#topological-sort)
- - [Shortest Path](#shortest-path)
- - [Minimum Spanning Tree](#minimum-spanning-tree)

## Min/Max Heap

In computer science, a heap is a specialized tree-based data structure which is essentially an almost complete tree that satisfies the heap property: in a max heap, for any given node C, if P is a parent node of C, then the key (the value) of P is greater than or equal to the key of C. In a min heap, the key of P is less than or equal to the key of C. The node at the "top" of the heap (with no parents) is called the root node.

We provide two implementations of binary heap: Min and Max heaps.

   ### Complexity: 

|          | Best Case |    Average     |   Worst Case   |
| -------- | :-------: | :------------: | :------------: |
| Space    |   O(n)    |      O(n)      |      O(n)      |
| Peek     |   O(1)    |      O(1)      |      O(1)      |
| Push     |   O(1)    |   O(log(n))    |   O(log(n))    |
| Pop      |   O(1)    |   O(log(n))    |   O(log(n))    |
| Remove   | O(log(n)) | O(n \* log(n)) | O(n \* log(n)) |
| Contains |   O(1)    |      O(n)      |      O(n)      |

### API

Heap can be built from any collection that implements _IEnumerable<>_. Otherwise API is pretty standard and supports operations such as: _Push_, _Pop_, _Peek_. Below are the code samples for each.

#### Basics

To build MinHeap using implicit _T_ comparer from any _IEnumerable<>_ collection simply use an extension method as shown below:

```c#
var collection = new List<int> { 2, 1, 3 };

var heap = collection.ToMinBinaryHeap();
```

There are 3 more overloads that can be used if custom _T_ comparer needs to be provided:

1. Custom _IComparer<T>_ with default constructor:

```c#
var collection = new List<int> { 2, 1, 3 };

var heap = collection.ToMinBinaryHeap<int, IntComparer>();
```

2. Providing an instance of a custom comparer for _T_:

```c#
var collection = new List<int> { 2, 1, 3 };

var heap = collection.ToMinBinaryHeap(new IntComparer());
```

3. Providing a _Lambda_ as a comparer function for _T_:

```c#
var collection = new List<int> { 2, 1, 3 };

var heap = collection.ToMinBinaryHeap((x, y) => x.CompareTo(y));
```

For building Max Heap - equivalent _**ToMaxBinaryHeap**_ method with same set of overloads exsits.

To PEEK at the TOP value, call **Peek** method. There are 2 possible outcomes:

1. If the Heap is empty then nothing will be returned.
2. If Heap is not empty then either Minimum or Maximum value will be returned based on your Heap type. Value remains in the heap!

```c#
var result = heap.Peek();
```

To ADD/INSERT new value, simply call **Push** method passing _VALUE_:

```c#
heap = heap.Push(4);
```

To check if the value exists in the heap we can use **CONTAINS** passing **VALUE** to check, if value is found **TRUE** will be returned, otherwise if **VALUE** is NOT found or the heap is empty **FALSE** will be returned.

```c#
var found = heap.Contains(4);
```

To _REMOVE/DELETE_ TOP value, simply call **POP**, this will remove the highest priority value and return the result. In case Heap is empty, no value will be returned:

```c#
var value = heap.Pop();
```

If we want to _REMOVE/DELETE_ any arbitrary value we can call **REMOVE** passing the **VALUE** to be removed. If value is found, it will be removed and the heap will be restored if required. In case value doesn't exist in the heap, the call will simply return without any modifications.

```c#
heap = heap.Remove(5); // Try's to remove value 5
```

#### Traversal

Heap also implements an _IEnumerable<>_, which allows this data structure to be traversed like any other **[Collection](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections)** in .NET, for example using a **[foreach](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/foreach-in)** statement.

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/heap-data-structure/)
- [Wikipedia](<https://en.wikipedia.org/wiki/Heap_(data_structure)>)

## Tree

### Binary Search Tree

In computer science, binary search trees (BST), sometimes called ordered or sorted binary trees, are a particular type of container: a data structure that stores "items" (such as numbers, names etc.) in memory. They allow fast lookup, addition and removal of items, and can be used to implement either dynamic sets of items, or lookup tables that allow finding an item by its key (e.g., finding the phone number of a person by name).

#### Use Case:

In computing, binary trees are used in two very different ways: First, as a means of accessing nodes based on some value or label associated with each node. Binary trees labelled this way are used to implement binary search trees and binary heaps, and are used for efficient searching and sorting.

#### Complexity:

|        | Best Case |  Average  | Worst Case |
| ------ | :-------: | :-------: | :--------: |
| Space  |   O(n)    |   O(n)    |    O(n)    |
| Search |   O(1)    | O(log(n)) |    O(n)    |
| Add    |   O(1)    | O(log(n)) |    O(n)    |
| Delete |   O(1)    | O(log(n)) |    O(n)    |

#### API

Binary Search Tree can be built from any collection that implements _IEnumerable<KeyValuePair<,>>_, most common case perhaps would be a [Dictionary<,>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=netframework-4.8). Otherwise API is pretty standard and supports operations such as: _Add_, _Delete_, _Find_, as well as exposes some common properties like: _Root_ and _IsEmpty_. Below are the code samples for each. It is important to note that this implementation of [Binary Search Tree](#binary-search-tree) is [**Immutable**](https://en.wikipedia.org/wiki/Immutable_object).

##### Basics

To build a binary tree using implicit _Key_ comparer from a dictionary or any other compatible collection simply use an extension method as shown below:

```c#
var dictionary = new Dictionary<int, string>
{
    { 2, "B"  },
    { 1, "A" },
    { 3, "C" }
};

var tree = dictionary.ToImmutableBinarySearchTree();
```

There are 3 more overloads that can be used if custom _Key_ comparer needs to be provided:

1. Custom _IComparer<Key>_ with default constructor:

```c#
var dictionary = new Dictionary<int, string>
{
    { 2, "B"  },
    { 1, "A" },
    { 3, "C" }
};

var tree = dictionary.ToImmutableBinarySearchTree<int, string, IntComparer>();
```

2. Providing an instance of a custom comparer for _Key_:

```c#
var dictionary = new Dictionary<int, string>
{
    { 2, "B"  },
    { 1, "A" },
    { 3, "C" }
};

var tree = dictionary.ToImmutableBinarySearchTree(new IntComparer());
```

3. Providing a _Lambda_ as a comparer function for _Key_:

```c#
var dictionary = new Dictionary<int, string>
{
    { 2, "B"  },
    { 1, "A" },
    { 3, "C" }
};

var tree = dictionary.ToImmutableBinarySearchTree((x, y) => x.CompareTo(y));
```

To find any value by key, call **Find** method and pass _key_ as parameter. There are 2 possible outcomes:

1. If there is no matching key, the resulting collection will be empty
2. If there is matching key, the resulting collection will contain 1 or more values stored under that key

```c#
var result = tree.Find(1);
```

To ADD/INSERT new value, simply call **Add** method passing _KEY_ and _VALUE_. If _KEY_ is already present in the tree, the value will be put next to last value added under same _KEY_:

```c#
tree = tree.Add(4, "D");
```

To _REMOVE/DELETE_ a value, use its _KEY_ and call **Delete** method. If _KEY_ is not found, the method will simply return, otherwise it will remove matching node with all of its values and the tree will be rearranged if required to match [Binary Search Tree](#binary-search-tree) constraints:

```c#
tree = tree.Delete(3);
```

##### Traversal

We support 4 ways to traverse a [Binary Search Tree](#binary-search-tree), all available via fluent API as shown in the below examples:

1. [Breadth First](https://en.wikipedia.org/wiki/Breadth-first_search)

```c#
var results = tree.Traverse(o => o.BreadthFirst());
```

2. [Depth First (Pre Order)](<https://en.wikipedia.org/wiki/Tree_traversal#Pre-order_(NLR)>)

```c#
var results = tree.Traverse(o => o.DepthFirst().PreOrder());
```

3. [Depth First (In Order)](<https://en.wikipedia.org/wiki/Tree_traversal#In-order_(LNR)>)

```c#
var results = tree.Traverse(o => o.DepthFirst().InOrder());
```

4. [Depth First (Post Order)](<https://en.wikipedia.org/wiki/Tree_traversal#Post-order_(LRN)>)

```c#
var results = tree.Traverse(o => o.DepthFirst().PostOrder());
```

#### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/binary-search-tree-data-structure/)
- [Wikipedia](https://en.wikipedia.org/wiki/Binary_search_tree)

### AVL Tree

In computer science, an AVL tree (named after inventors Adelson-Velsky and Landis) is a self-balancing binary search tree. It was the first such data structure to be invented. In an AVL tree, the heights of the two child subtrees of any node differ by at most one; if at any time they differ by more than one, rebalancing is done to restore this property.

#### Use Case:

AVL trees are often compared with [Red Black Tree's](#red-black-tree) because both support the same set of operations and take _O(log n)_ time for the basic operations. For lookup-intensive applications, AVL trees are faster than [Red Black Tree's](#red-black-tree) because they are more strictly balanced.

#### Complexity:

|        | Best Case |  Average  | Worst Case |
| ------ | :-------: | :-------: | :--------: |
| Space  |   O(n)    |   O(n)    |    O(n)    |
| Search |   O(1)    | O(log(n)) | O(log(n))  |
| Add    |   O(1)    | O(log(n)) | O(log(n))  |
| Delete |   O(1)    | O(log(n)) | O(log(n))  |

#### API

[AVL Tree's](#avl-tree) API is the same as [Binary Search Tree's](#binary-search-tree) API, with the exception of factory method names being different, as shown below:

```c#
var dictionary = new Dictionary<int, string>
{
    { 2, "B"  },
    { 1, "A" },
    { 3, "C" }
};

var tree = dictionary.ToImmutableAvlTree();
```

For rest of the examples, including operations such as: Find, Add, Delete and Traverse please consult [Binary Search Tree's](#binary-search-tree) API section.

#### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/avl-tree-set-1-insertion/)
- [Wikipedia](https://en.wikipedia.org/wiki/AVL_tree)

## Graph

In computer science, a graph is an abstract data type that is meant to implement the undirected graph and directed graph concepts from mathematics; specifically, the field of graph theory.

A graph data structure consists of a finite (and possibly mutable) set of vertices (also called nodes or points), together with a set of unordered pairs of these vertices for an undirected graph or a set of ordered pairs for a directed graph. These pairs are known as edges (also called links or lines), and for a directed graph are also known as arrows. The vertices may be part of the graph structure, or may be external entities represented by integer indices or references.

A graph data structure may also associate to each edge some edge value, such as a symbolic label or a numeric attribute (cost, capacity, length, etc.).

### Adjacency Set/Matrix based Graph

We provide two most popular/useful implementations of the graph:

1. AdjacencySet - which is used mainly for sparsly connected graphs and has a space complexity of O(V+E)
2. AdjacencyMatrix - used for well connected graphs and has a space complexity of (O \* O), but offers better performance

### Use Case:

Graphs are perhaps the most widely used data structures to represent various relationships. Perhaps most commonly known applications would be: maps, social networks, various workflows.

### Complexity:

|                   | Adjacency Set | Adjacency Matrix |
| ----------------- | :-----------: | :--------------: |
| Add Vertex        |     O(1)      |    O(V \* V)     |
| Add Edge          |     O(1)      |       O(1)       |
| Remove Vertex     |   O(V + E)    |    O(V \* V)     |
| Remove Edge       |     O(1)      |       O(1)       |
| Adjacent Vertices |     O(E)      |       O(V)       |
| Contains Edge     |     O(1)      |       O(1)       |

### API

Graph can be built either by directly instantiating one of the available implementations using provided **ctor's** or by useing a fluent _GraphBuilder_ interface. Once we have the _Graph_ structure, the interface is quite standard and allows us to _add/remove_ vertices and edges, as well as check for an _existance_ of a certain edge as well as to _retrieve_ all adjacent vertices of any given edge.

#### Basics

To create a graph we can directly instanciate one of the following classes as shown in the below examples. To create an [AdjacencySet](https://en.wikipedia.org/wiki/Adjacency_list) based graph use the following code snippet:

```c#

 var gas1 = new AdjacencySetGraph<int>(GraphType.Directed);
 var gas2 = new AdjacencySetGraph<int>(GraphType.Directed, EqualityComparer<int>.Default);
 var gam1 = new AdjacencyMatrixGraph<int>(GraphType.Directed);
 var gam2 = new AdjacencyMatrixGraph<int>(GraphType.Directed, comparer);
 var gam3 = new AdjacencyMatrixGraph<int>(GraphType.Directed, comparer, 10);

```

- [AdjacencySet](https://en.wikipedia.org/wiki/Adjacency_list) _ctor_ has one GENERIC argument and 1 to 2 paramerers:
- - **T** - specifies the data type that you wish to store in the graph
- - graphType - specifies if you wish to create _Directed_ or _Undirected_ graph
- - comparer - is an [IEqualityComparer<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1?view=netframework-4.8) for given type T, this is used for comparisons in traversal algorithsm as well as in internally used arrays/set's If T is a custom type we advice to provide a custom implementation for [IEqualityComparer<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1?view=netframework-4.8) or alternatively the default will be used, which may result in incorrect results.
- [AdjacencyMatrix](https://en.wikipedia.org/wiki/Adjacency_matrix) _ctor_ has one GENERIC argument and 1 to 3 paramerers:
- - **T** - same as for [AdjacencySet](https://en.wikipedia.org/wiki/Adjacency_list)
- - graphType - same as for [AdjacencySet](https://en.wikipedia.org/wiki/Adjacency_list)
- - comparer - same as for [AdjacencySet](https://en.wikipedia.org/wiki/Adjacency_list)
- - size - if number of Vertices is known, you may specifcy the size of the matrix to avoid resizing when building the graph, otherwise a default size is used and as the graph grows the internal arrays will be resized as needed. Resizing as considerable performance penalties.

Alternatively you may use a fluent API to construct desired graph:

```c#
var graph1 = GraphBuilder.Create<int>(b => b.Directed().WellConnected());
var graph2 = GraphBuilder.Create<int>(b => b.Undirected().WellConnected());
var graph3 = GraphBuilder.Create<int>(b => b.Directed().Sparse());
var graph4 = GraphBuilder.Create<int>(b => b.Undirected().Sparse());
```

Examples above constructs graphs using default comparer, as mentioned before if the generic argument _T_ is a custom type, its is recommended to provide an implementation of [IEqualityComparer<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1?view=netframework-4.8) for _T_ and supply it to the graph. Using fluent API allows to supply such comparer in 3 different ways:

1. Providing an instance of a custom comparer for _T_:

```c#
var graph = GraphBuilder.Create<int>(b => b.Directed().WellConnected().CompareUsing(new IntComparer()));
```

2. Providing an implementation of comparer for _T_ with parameterless _ctor_:

```c#
var graph = GraphBuilder.Create<int>(b => b.Directed().WellConnected().CompareUsing<IntComparer>());
```

3. Providing a _Lambda_ as a comparer function for _T_:

```c#
var graph = GraphBuilder.Create<int>(b => b.Directed().WellConnected().CompareUsing((x, y) => x == y));
```

At this point we know how to create desired graph, but it is of no use unless it contains data. We will look at the API exposed by our graph implementation for populating and consuming data.

To check if graph contains given _Edge_ we can use _Contains_ method:

```c#
var result = graph.Contains(5);
```

To add _Vertex_ with no _Edges_ we can use _AddVertex_ method:

```c#
graph = graph.AddVertex(5);
```

To remove _Vertex_ we can use _RemoveVertex_ method:

```c#
graph = graph.RemoveVertex(5);
```

> if _Vertex_ is NOT part of the graph the call will simply return

To add one _Edge_ we can use _AddEdge_ method:

```c#
graph = graph.AddEdge(1, 2);
graph = graph.AddEdge(1, 2, 5); // Weighted edge with a weight of 5
```

> Second line demonstrates how to add weighted edge.
> By default each edge will have a weight of 1 and will be treated equally

To remove _Edge_ we can use _RemoveEdge_ method:

```c#
graph = graph.RemoveVertex(1, 2);
```

> If such connection/edge doesn't exist the call simply returns.

To obtain a list of _Edges_ for given _Vertex_ we can use _GetAdjacentVertices_ method:

```c#
var vertices = graph.GetAdjacentVertices(1);
```

> If _Vertex_ doesn't exist or doesn't have any _Vertices_ than an empty collection will be returned.

##### Enumeration

Any graph implements [IEnumerable<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=netframework-4.8) which enables us to enumerate _Vertices_ within the graph irregardless of its type or connectivity properties. This can be achieved using code snippet below:

```c#
var vertices = graph.ToList();

// or alternatively

foreach (var vertex in graph) {
    // process vertex
}
```

> Ordering of _Vertices_ is NOT guaranteed and will differ based on underlying implementation, as well as graph built order.

##### Traversal

We support 2 ways to traverse a [Graph](#graph), both methods available via fluent API as shown in the below examples:

1. [Breadth First](https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/)

```c#
var results = graph.Traverse(o => o.BreadthFirst(), 1);
```

2. [Depth First](https://www.geeksforgeeks.org/depth-first-search-or-dfs-for-a-graph/)

```c#
var results = graph.Traverse(o => o.DepthFirst(), 1);
```

> The second parameter with a value of _1_ in both cases is the _Starting Vertex_ for the graph we are traversing.

> Note that some graphs can be traversed in more than one way and order of adjacent _Vertices_ for any given *Vertex *is not guaranteed.

#### Advanced API

##### Topological Sort

Graph can be sorted using [Kahn's Topological Sort](https://en.wikipedia.org/wiki/Topological_sorting) algorithm. The algorithm requires that graph would exibit certain properties, such as, graph must be a [DAG](https://en.wikipedia.org/wiki/Directed_acyclic_graph), in other words it needs to be a _DIRECTED_ graph with at least one _Vertex_ that has no incoming connections (no _Vertices_ are pointing towards it). Graph can have more than 1 topological sort or in case it is not a [DAG](https://en.wikipedia.org/wiki/Directed_acyclic_graph) it may have NONE. If no topological sort is possible the result will simply contain an empty _Option<>_.

##### Use Case

- A common application of topological sorting is in scheduling a sequence of jobs

Please consult the below examples for more details on the API:

```c#
var results = graph.Sort(o => o.UseTopologicalSort());

if (results.Any()) {
    var topologicalOrder = results.Single();
} else {
    // No topological sort possible
}
```

##### Shortest Path

One of the most common graph problems is finding [Shortest Path](https://en.wikipedia.org/wiki/Shortest_path_problem). We have a couple of algorithms to find the shortest path, such as: BFS based shortest path. Currently you can choose from [Unweighted Graph](https://www.geeksforgeeks.org/shortest-path-unweighted-graph/), [Dijkstra's](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm), [Bellman Ford's](https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm) algorithms.

##### Use Case

- Shortest path algorithms are applied to automatically find directions between physical locations, such as driving directions.
- In a networking or telecommunications mindset, this shortest path problem is sometimes called the min-delay path problem and usually tied with a widest path problem. For example, the algorithm may seek the shortest (min-delay) widest path, or widest shortest (min-delay) path.
- Other applications, often studied in operations research, include plant and facility layout, robotics, transportation, and VLSI design.

Usage examples are below:

```c#
var travelFrom = 1;
var travelTo = 5;

var results = graph.FindShortestPath(travelFrom, travelTo, o => o.UseUnweightedGraph());
    results = graph.FindShortestPath(travelFrom, travelTo, o => o.UseDijkstra());
    results = graph.FindShortestPath(travelFrom, travelTo, o => o.UseBellmanFord());
    results = graph.FindShortestPath(travelFrom, travelTo);

if (results.Any()) {
    var path = results.Single();
} else {
    // No path found travelFrom => travelto
}
```

In the sample above you see 4 different ways to find shortest path. Top 3 lines show how to select a specfic algorithm, the 4th line ommits the algorithm selection and will select the best algorithm based on the supplied graph properties. Obviously 4th option is the easiest to use, however if the built in selection rules doesn't meet your requirements you may select an algortihm yourself, all of that is great, but which one?

The general rules for selecting shortest path algorithm is as such:

- Graph is unweighted (all vertices are of the same weight) and all weights are positive - go for **UseUnweightedGraph**
- Graph is weighted and all weights are positive - go for **Dijkstra's**
- Graph is weighted, _DIRECTED_ and some of the weights are negative - go for **Bellman Ford's**
- Graph is weighted, _UNDIRECTED_ and some of the weights are negative - sadly you are out of luck! Currently library does not support finding shortest path for such graphs, but will offer an algorithm in the future.

If you select incorrect algorithm for a given graph, let's say you have a graph with negative weights and decide to use [Dijkstra's](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm), this will result in a sub optimal path. Please be careful when selecting an algorithm. You may use _MetadataProvider_ service to extract graph metadata to help you make a better choice.

> When using _graph.FindShortestPath(travelFrom, travelTo)_ without explicitly specifying an algorithm it is determined that no algorithm can be used for finding [Shortest Path](https://en.wikipedia.org/wiki/Shortest_path_problem) for the given graph, an exception of type _GraphException_ will be thrown.

> When using either [Dijkstra's](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm) or [Bellman Ford's](https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm) algorithms, in case more than one shortest path is found, the preferred path is with the least number of edges, otherwise first available path will be returned.

##### Minimum Spanning Tree 

A minimum spanning tree (MST) or minimum weight spanning tree is a subset of the edges of a connected, edge-weighted undirected graph that connects all the vertices together, without any cycles and with the minimum possible total edge weight. That is, it is a spanning tree whose sum of edge weights is as small as possible. More generally, any edge-weighted undirected graph (not necessarily connected) has a minimum spanning forest, which is a union of the minimum spanning trees for its connected components.

Currently we support 2 algorithms: [Prim's](https://en.wikipedia.org/wiki/Prim%27s_algorithm) and [Kruskal's](https://en.wikipedia.org/wiki/Kruskal%27s_algorithm). Both of these algorithms can be used on _DIRECTED_ graphs, the difference being that [Kruskal's](https://en.wikipedia.org/wiki/Kruskal%27s_algorithm) can find multiple [MST](https://en.wikipedia.org/wiki/Minimum_spanning_tree), given a [_Disconnected_](<https://en.wikipedia.org/wiki/Connectivity_(graph_theory)>) graph.

##### Use Case

- Telecommunications company trying to lay cable in a new neighborhood
- Most efficient routing for an electrical grid

Usage examples are below:

```c#
var results = graph.FindSpanningTree(o => o.UsePrims());
    results = graph.FindSpanningTree(o => o.UseKruskals());
    results = graph.FindSpanningTree();

if (results.Any()) {
    var tree = results.Single();
} else {
    // No Minimum Spanning Tree found
}
```

Similar to the [_Shortest Path_](#Shortest-Path) API we provide an overload that _omits_ the specific algorithm and uses built-in heuristics to choose the most appropriate algorithm or you may choose it yourself. Mind the limitation of each algorithm if choosing yourself:

- Graph is _DIRECTED_ and _CONNECTED_ - go for [Prim's](https://en.wikipedia.org/wiki/Prim%27s_algorithm)
- Graph is _DIRECTED_ and _DISCONNECTED_ (Forest) - go for [Kruskal's](https://en.wikipedia.org/wiki/Kruskal%27s_algorithm)
- Graph is _UNDIRECTED_ - at the moment we do NOT support [MST](https://en.wikipedia.org/wiki/Minimum_spanning_tree) for _UNDIRECTED_ graphs. This will likely to be addressed in the future.

If you select incorrect algorithm for a given graph, let's say you have a _DIRECTED_ and _DISCONNECTED_ graph and you have selected [Prim's](https://en.wikipedia.org/wiki/Prim%27s_algorithm) algoritm, your [MST](https://en.wikipedia.org/wiki/Minimum_spanning_tree) will only cover _VERTICES_ that are reachable from the first _VERTEX_, which may or may not be what you want. You may use _MetadataProvider_ service to extract graph metadata to help you make a better choice.

> When using _graph.FindSpanningTree()_ without explicitly specifying an algorithm it is determined that no algorithm can be used for finding [MST](https://en.wikipedia.org/wiki/Minimum_spanning_tree) for the given graph, an exception of type _GraphException_ will be thrown.

##### Complexity:

|                                           | Adjacency Set | Adjacency Matrix |
| ----------------------------------------- | :-----------: | :--------------: |
| Topological Sort                          |    O(V+E)     |      O(V+E)      |
| Shortest Path (Unweighted)                |    O(V+E)     |     O(V\*V)      |
| Shortest Path (Weighted) - Dijkstra's     | O(E\*log(V))  |   O(E\*log(V))   |
| Shortest Path (Weighted) - Bellman Ford's |    O(E\*V)    |    O(V\*V\*V)    |
| Minimum Spanning Tree - Prim's            | O(E\*log(V))  |   O(E\*log(V))   |
| Minimum Spanning Tree - Kruskal's         | O(E\*log(E))  |   O(E\*log(E))   |

#### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/graph-and-its-representations/)
- [Wikipedia](<https://en.wikipedia.org/wiki/Graph_(abstract_data_type)>)
