# Search
## Index

- [Home](../README.md)
- [Search API](#api)
- - [Linear Search](#linear-search)
- - [Hash Table Search](#hash-table-search)
- - [Binary Search](#binary-search)
- - [Ternary Search](#ternary-search)
- - [Jump Search](#jump-search)
- - [Exponential Search](#exponential-search)
- - [Fibonacci Search](#fibonacci-search)

## API

The search API is implemented using [Extension methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) technique, which means you can use one of the following methods on any collection that implements _[IList<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1?view=netframework-4.8)_:

- FindIndex - will find index for a given _KEY_ or return **-1** if none found.
- FindAllIndexes - will find all indexes for a given _KEY_ or return an empty collection if none found.
- Find - will return an [Optional<<T>>](https://app.pluralsight.com/library/courses/making-functional-csharp/table-of-contents) result with or without a value for a given _KEY_. This is a technique widely used in Functional Programming to [avoid returning NULLS](https://www.lucidchart.com/techblog/2015/08/31/the-worst-mistake-of-computer-science/).
- FindAll - will find all values for a given _KEY_ or return an empty collection if none found.

This is a full example of using Search API:

```c#
internal class Program
{
    private static void Main(string[] args)
    {
        var valueToFind = 5;
        var collection = new List<int> {2, 5, 3, 1, 4};
        var result = collection.Find(valueToFind, o => o.UseLinearSearch());
        Console.WriteLine($"Result: {result}");
    }
}
```

Some search algorithms require comparisons, we did not want to force custom types to implement **[IComparable<>](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1?view=netframework-4.8)** as this may not always be possible and this makes this API easy to adopt. Given this limitation we have 3 options how to deal with comparisons:

1.  Provide a custom implementation of **[IComparer<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1?view=netframework-4.8)** interface for the give type _T_. Comparer must have a default _ctor_. Below is the example of such comparer for type _int_ and its usage in [Binary Search](#binary-search):

```c#
internal class Program
{
    private static void Main(string[] args)
    {
        var valueToFind = 5;
        var collection = new List<int> {1, 2, 3, 4, 5};
        var result = collection.Find(valueToFind, o => o.UseBinarySearch<int, IntComparer>());
        Console.WriteLine($"Result: {result}");
    }

    public sealed class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}
```

The rest of the samples will OMIT the redundant code.

2. Pass an instance of a custom comparer implementation. This may be choosen for multiple reasons, such as not wanting to create a new instance per each call or need to inject services via _ctor_:

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Find(valueToFind, o => o.UseBinarySearch<int>(comparer));
// Omited for clarity
```

3. Pass in a lambda that performs the comparison. This is probably less useful, but enables us to provide a comparison function inline with the call:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseBinarySearch<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
```

## Linear Search

A linear search sequentially checks each element of the list until it finds an element that matches the target value. If the algorithm reaches the end of the list, the search terminates unsuccessfully.

### Use Case:

Linear search is rarely practical because other search algorithms and schemes, such as the [Binary Search](#binary-search) algorithm and hash tables, allow significantly faster searching for all but short lists.

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(n)**
- Worst Case: **O(n)**
- Space: **O(1)**

### Code Sample:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseLinearSearch());
// Omited for clarity
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/linear-search/)
- [Wikipedia](https://en.wikipedia.org/wiki/Linear_search)

## Hash Table Search

Powerful technique for searching larger collections that are not necessarily ordered. One of the most common approaches is to use a hash function to transform one or more characteristics of the searched-for item into a value that is used to index into an indexed hash table.

### Use Case:

Can be used for searching through very large collections, at which point this algorithm can outperform [Binary Search](#binary-search).

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(1)**
- Worst Case: **O(n)**
- Space: **O(n)**

### Code Sample:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseHashTableSearch());
// Omited for clarity
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/hashing-data-structure/)
- [Oreilly](https://www.oreilly.com/library/view/algorithms-in-a/9780596516246/ch05s04.html)

## Binary Search

In computer science, binary search is a search algorithm that finds the position of a target value within a sorted array. Binary search compares the target value to the middle element of the array. If they are not equal, the half in which the target cannot lie is eliminated and the search continues on the remaining half, again taking the middle element to compare to the target value, and repeating this until the target value is found. If the search ends with the remaining half being empty, the target is not in the array.

### Use Case:

Binary search can be used to solve a wider range of problems, such as finding the next-smallest or next-largest element in the array relative to the target even if it is absent from the array.

### Requirements:

Input collection needs to be **sorted** in an ascending order.

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(log(n))**
- Worst Case: **O(log(n))**
- Space: **O(1)**

### Code Sample:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseBinarySearch<int, IntComparer>());
// Omited for clarity
}
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Find(valueToFind, o => o.UseBinarySearch<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseBinarySearch<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/binary-search/)
- [Wikipedia](https://en.wikipedia.org/wiki/Binary_search_algorithm)

## Ternary Search

Ternary search is a divide and conquer algorithm that can be used to find an element in an array. It is similar to [Binary Search](#binary-search) where we divide the array into two parts but in this algorithm. In this, we divide the given array into three parts and determine which has the key (searched element). We can divide the array into three parts by taking mid1 and mid2 which can be calculated as shown below. Initially, l and r will be equal to 0 and n-1 respectively, where n is the length of the array.

### Use Case:

It seems that [Binary Search](#binary-search) is better overall than Ternary Search, therefore it's use cases are limited.

### Requirements:

Input collection needs to be **sorted** in an ascending order.

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(log3(n))**
- Worst Case: **O(log3(n))**
- Space: **O(1)**

### Code Sample:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseTernarySearch<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Find(valueToFind, o => o.UseTernarySearch<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseTernarySearch<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/ternary-search/)
- [Wikipedia](https://en.wikipedia.org/wiki/Ternary_search)

## Jump Search

Like [Binary Search](#binary-search), Jump Search is a searching algorithm for sorted arrays. The basic idea is to check fewer elements (than linear search) by jumping ahead by fixed steps or skipping some elements in place of searching all elements.

### Use Case:

[Binary Search](#binary-search) is better than Jump Search, but Jump search has an advantage that we traverse back only once ([Binary Search](#binary-search) may require up to O(Log n) jumps, consider a situation where the element to be searched is the smallest element or smaller than the smallest). So in a system where [Binary Search](#binary-search) is costly, we use Jump Search.

### Requirements:

Input collection needs to be **sorted** in an ascending order.

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(sqrt(n))**
- Worst Case: **O(sqrt(n))**
- Space: **O(1)**

### Code Sample:

Code sample contains 2 classes:

- Main - this is an example usage of the actual search algorithm with custom comparer for int type, but it can be implemented for any custom type. Comparer must implement **[IComparer<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1?view=netframework-4.8)** interface.
- **IntComparer** - a sample comparer for integer values implementation.

This approach will NOT require you to change any of your existing types, instead you will need to implement a type specific comparer.

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseJumpSearch<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Find(valueToFind, o => o.UseJumpSearch<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseJumpSearch<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/jump-search/)
- [Wikipedia](https://en.wikipedia.org/wiki/Jump_search)

## Exponential Search

In computer science, an exponential search (also called doubling search or galloping search or Struzik search) is an algorithm for searching sorted, unbounded/infinite lists. There are numerous ways to implement this with the most common being to determine a range that the search key resides in and performing a [Binary Search](#binary-search) within that range. This takes O(log i) where i is the position of the search key in the list, if the search key is in the list, or the position where the search key should be, if the search key is not in the list.

### Use Case:

Search sorted, bounded/unbounded/infinite lists

### Requirements:

Input collection needs to be **sorted** in an ascending order.

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(log(i))** - where i is the position of the search key in the list
- Worst Case: **O(log(i))** - where i is the position of the search key in the list
- Space: **O(1)**

### Code Sample:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseExponentialSearch<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Find(valueToFind, o => o.UseExponentialSearch<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseExponentialSearch<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/exponential-search/)
- [Wikipedia](https://en.wikipedia.org/wiki/Exponential_search)

## Fibonacci Search

In computer science, the Fibonacci search technique is a method of searching a sorted array using a divide and conquer algorithm that narrows down possible locations with the aid of Fibonacci numbers. Compared to [Binary Search](#binary-search) where the sorted array is divided into two equal-sized parts, one of which is examined further, Fibonacci search divides the array into two parts that have sizes that are consecutive Fibonacci numbers. On average, this leads to about 4% more comparisons to be executed, but it has the advantage that one only needs addition and subtraction to calculate the indices of the accessed array elements, while classical [Binary Search](#binary-search) needs bit-shift, division or multiplication, operations that were less common at the time Fibonacci search was first published

### Use Case:

If the elements being searched have non-uniform access memory storage (i. e., the time needed to access a storage location varies depending on the location accessed), the Fibonacci search may have the advantage over [Binary Search](#binary-search) in slightly reducing the average time needed to access a storage location.

### Requirements:

Input collection needs to be **sorted** in an ascending order.

### Complexity:

- Best Case: **O(1)**
- Average Case: **O(log(n))**
- Worst Case: **O(log(n))**
- Space: **O(1)**

### Code Sample:

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseFibonacciSearch<int, IntComparer>());
// Omited for clarity
```

```c#
// Omited for clarity
var comparer = new IntComparer();
var result = collection.Find(valueToFind, o => o.UseFibonacciSearch<int>(comparer));
// Omited for clarity
}
```

```c#
// Omited for clarity
var result = collection.Find(valueToFind, o => o.UseFibonacciSearch<int>((x, y) => x.CompareTo(y)));
// Omited for clarity
}
```

### More info:

- [GeeksForGeeks](https://www.geeksforgeeks.org/fibonacci-search/)
- [Wikipedia](https://en.wikipedia.org/wiki/Fibonacci_search_technique)