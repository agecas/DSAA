# Algorithms and Data Structures

Project contains implementations of most popular **Algorithms** and **Data Strucutres**. Each **Algorithm** will have its most important properties such as **Time/Space complexity** defined, example use cases, installation instructions, as well as code samples on how it can be used.

## Installation

All **Algorithms** are split among number of **DSAA** [Nuget](https://www.nuget.org/) packages, which can be installed using following commands:

```powershell
Install-Package DSSA.Graph
Install-Package DSSA.Heap
Install-Package DSSA.List
Install-Package DSSA.Tree
```

or using dotnet CLI:

```bash
dotnet add package DSAA.Graph
dotnet add package DSAA.Heap
dotnet add package DSAA.List
dotnet add package DSAA.Tree
```

Please refer to the index below for an API documentation and usage examples:

## Index

- [Search](documents/SEARCH.md#search)
- - [Linear Search](documents/SEARCH.md#linear-search)
- - [Hash Table Search](documents/SEARCH.md#hash-table-search)
- - [Binary Search](documents/SEARCH.md#binary-search)
- - [Ternary Search](documents/SEARCH.md#ternary-search)
- - [Jump Search](documents/SEARCH.md#jump-search)
- - [Exponential Search](documents/SEARCH.md#exponential-search)
- - [Fibonacci Search](documents/SEARCH.md#fibonacci-search)
- [Sort](documents/SORT.md#sort)
- - [Bubble Sort](documents/SORT.md#bubble-sort)
- - [Insertion Sort](documents/SORT.md#insertion-sort)
- - [Shell Sort](documents/SORT.md#shell-sort)
- - [Selection Sort](documents/SORT.md#selection-sort)
- - [Quick Sort](documents/SORT.md#quick-sort)
- - [Merge Sort](documents/SORT.md#merge-sort)
- - [Heap Sort](documents/SORT.md#heap-sort)
- [Data Structures](documents/DATASTRUCTURES.md#data-structures)
- - [Min/Max Heap](documents/DATASTRUCTURES.md#min/maxheap)
- - [Tree](documents/DATASTRUCTURES.md#tree)
- - - [Binary Search Tree](documents/DATASTRUCTURES.md#binary-search-tree)
- - - [AVL Tree](documents/DATASTRUCTURES.md#avl-tree)
- - [Graph](documents/DATASTRUCTURES.md#graph)
- - - [Topological Sort](documents/DATASTRUCTURES.md#topological-sort)
- - [Shortest Path](documents/DATASTRUCTURES.md#shortest-path)
- - [Minimum Spanning Tree](documents/DATASTRUCTURES.md#minimum-spanning-tree)

## Future Goals

Project has been created with an idea that over time the number of useful algorithms and data structures will grow, perhaps with the help of the community and that this can be a go to project for computer science students as well as professional software developers when in need of a specific algorithm and/or data strucutre. All implementations have been tested and an effort has been put in to design a reasonably usable API for real life use cases, however any ideas/improvements are very welcome and appreacited.

- Expand the variaty of algorithms offered for Graph problems
- Add implementations for an immutable Red-Black tree
- More ...
