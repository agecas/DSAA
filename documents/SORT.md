# Sort

## Index

- [Home](../README.md)
- [Sort API](#api)
- - [Bubble Sort](#bubble-sort)
- - [Insertion Sort](#insertion-sort)
- - [Shell Sort](#shell-sort)
- - [Selection Sort](#selection-sort)
- - [Quick Sort](#quick-sort)
- - [Merge Sort](#merge-sort)
- - [Heap Sort](#heap-sort)

## API

The sort API is implemented using [Extension methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) technique, same as [Search API](search.md#api), which means you can use the following method on any collection that implements _[IList<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1?view=netframework-4.8)_:

- Sort - allows to select sorting strategy and returns a sorted collection.

This is a full example of using Sort API:

```c#
    internal class Program
    {
        private static void Main(string[] args)
        {
            var collection = new List<int> {2, 5, 3, 1, 4};
            var sorted = collection.Sort(o => o.UseBubbleSort<int, IntComparer>());

            Console.WriteLine($"Sorted collection: {string.Join(", ", sorted)}");
        }
    }

    public sealed class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
```

All sort algorithms require comparisons, the approach is the same as described in Search API, we have 3 options:

1.  Provide a custom implementation of **[IComparer<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1?view=netframework-4.8)** interface for the give type _T_. Comparer must have a default _ctor_. Below is the example of such comparer for type _int_ and its usage in [Bubble Sort](#bubble-sort):

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseBubbleSort<int, IntComparer>());
// Omited for clarity
```

2. Pass an instance of a custom comparer implementation. This may be choosen for multiple reasons, such as not wanting to create a new instance per each call or need to inject services via _ctor_:

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseBubbleSort<int>(comparer));
// Omited for clarity
```

3. Pass in a lambda that performs the comparison. This is probably less useful, but enables us to provide a comparison function inline with the call:

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseBubbleSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
```

## Bubble Sort

Bubble sort, sometimes referred to as sinking sort, is a simple sorting algorithm that repeatedly steps through the list, compares adjacent elements and swaps them if they are in the wrong order. The pass through the list is repeated until the list is sorted. The algorithm, which is a comparison sort, is named for the way smaller or larger elements "bubble" to the top of the list.

### Use Case:

Can be used as an introduction for studying sort algorithms, due to it's simplicity.

### Complexity:

- Best Case: **O(n)**
- Average Case: **O(n \* n)**
- Worst Case: **O(n \* n)**
- Space: **O(1)**
- In Place
- Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseBubbleSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseBubbleSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseBubbleSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/bubble-sort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Bubble_sort)

## Insertion Sort

Insertion sort is a simple sorting algorithm that builds the final sorted array (or list) one item at a time. It is much less efficient on large lists than more advanced algorithms such as [Quick](#quick-sort), [Heap](#heap-sort), or [Merge Sort](#merge-sort).

### Use Case:

Efficient for (quite) small and/or partially sorted data sets. More efficient than [Selection Sort](#selection-sort) or [Bubble Sort](#bubble-sort). Can be used for sorting data as it arrives.

### Complexity:

- Best Case: **O(n)**
- Average Case: **O(n \* n)**
- Worst Case: **O(n \* n)**
- Space: **O(1)**
- In Place
- Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseInsertionSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseInsertionSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseInsertionSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/insertion-sort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Insertion_sort)

## Shell Sort

Shellsort, also known as Shell sort or Shell's method, is an in-place comparison sort. It can be seen as either a generalization of sorting by exchange ([Bubble Sort](#bubble-sort)) or sorting by insertion ([Insertion Sort](#insertion-sort)). The method starts by sorting pairs of elements far apart from each other, then progressively reducing the gap between elements to be compared. Starting with far apart elements, it can move some out-of-place elements into position faster than a simple nearest neighbor exchange.

### Use Case:

It is a sort of optimized version of [Insertion Sort](#insertion-sort). The running time of Shellsort is heavily dependent on the gap sequence it uses. For many practical variants, determining their time complexity remains an open problem.

### Complexity:

- Best Case: **O(n)**
- Average Case: **O(n \* n)**
- Worst Case: **O(n \* n)**
- Space: **O(1)**
- In Place
- Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseShellSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseShellSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseShellSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

Or alternatively we can provide a custom _Increment Factory Method_ like so:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseShellSort<int, IntComparer>(collection => collection.Count / 2);
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseShellSort<int>(comparer, collection => collection.Count / 2));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseShellSort<int>((x, y) => x.CompareTo(y), collection => collection.Count / 2));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/shellsort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Shellsort)

## Selection Sort

In computer science, selection sort is a sorting algorithm, specifically an in-place comparison sort. It has O(n2) time complexity, making it inefficient on large lists, and generally performs worse than the similar [Insertion Sort](#insertion-sort).

### Use Case:

Selection sort is noted for its simplicity, and it has performance advantages over more complicated algorithms in certain situations, particularly where auxiliary memory is limited.

### Complexity:

- Best Case: **O(n \* n)**
- Average Case: **O(n \* n)**
- Worst Case: **O(n \* n)**
- Space: **O(1)**
- In Place
- Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseSelectionSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseSelectionSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseSelectionSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/selection-sort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Selection_sort)

## Quick Sort

Quicksort (sometimes called partition-exchange sort) is an efficient sorting algorithm, serving as a systematic method for placing the elements of a random access file or an array in order. It is still a commonly used algorithm for sorting. When implemented well, it can be about two or three times faster than its main competitors, [Merge Sort](#merge-sort) and [Heap Sort](#heap-sort).

### Use Case:

Selection sort is noted for its simplicity, and it has performance advantages over more complicated algorithms in certain situations, particularly where auxiliary memory is limited.

### Complexity:

- Best Case: **O(n \* log(n))**
- Average Case: **O(n \* log(n))**
- Worst Case: **O(n \* log(n))**
- Space: **O(n)**
- Not In Place
- Not Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseQuickSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseQuickSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseQuickSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/quick-sort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Quicksort)

## Merge Sort

In computer science, merge sort (also commonly spelled mergesort) is an efficient, general-purpose, comparison-based sorting algorithm. Most implementations produce a stable sort, which means that the order of equal elements is the same in the input and output. Merge sort is a divide and conquer algorithm.

### Use Case:

Although [Heap Sort](#heap-sort) has the same time bounds as merge sort, it requires only Θ(1) auxiliary space instead of merge sort's Θ(n). On typical modern architectures, efficient [Quick Sort](#quick-sort) implementations generally outperform mergesort for sorting RAM-based arrays. On the other hand, merge sort is a stable sort and is more efficient at handling slow-to-access sequential media. Merge sort is often the best choice for sorting a _[linked list](https://en.wikipedia.org/wiki/Linked_list)_: in this situation it is relatively easy to implement a merge sort in such a way that it requires only Θ(1) extra space, and the slow random-access performance of a linked list makes some other algorithms (such as [Quick Sort](#quick-sort)) perform poorly, and others (such as [Heap Sort](#heap-sort)) completely impossible.

### Complexity:

- Best Case: **O(n \* log(n))**
- Average Case: **O(n \* log(n))**
- Worst Case: **O(n \* n)**
- Space: **O(n)**
- Not In Place
- Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseMergeSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseMergeSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseMergeSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/merge-sort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Merge_sort)

## Heap Sort

In computer science, heapsort is a comparison-based sorting algorithm. Heapsort can be thought of as an improved [Selection Sort](#selection-sort): like that algorithm, it divides its input into a sorted and an unsorted region, and it iteratively shrinks the unsorted region by extracting the largest element and moving that to the sorted region. The improvement consists of the use of a heap data structure rather than a linear-time search to find the maximum.

### Use Case:

Heap sort algorithm has limited uses because [Quick Sort](#quick-sort) and [Merge Sort](#merge-sort) are better in practice. Nevertheless, the [Heap](DATASTRUCTURES#heap) data structure itself is enormously used.

### Complexity:

- Best Case: **O(n \* log(n))**
- Average Case: **O(n \* log(n))**
- Worst Case: **O(n \* n)**
- Space: **O(n)**
- In Place
- Not Stable

### Code Sample:

```c#
// Omited for clarity
var sorted = collection.Sort(o => o.UseHeapSort<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Sort(o => o.UseHeapSort<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Sort(o => o.UseHeapSort<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/heap-sort/)
- [Wikipedia](https://en.wikipedia.org/wiki/Heapsort)
