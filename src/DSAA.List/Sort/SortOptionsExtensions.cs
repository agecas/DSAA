using System;
using System.Collections.Generic;
using DSAA.List.Sort.Strategy;
using DSAA.Shared;

namespace DSAA.List.Sort
{
    public static class SortOptionsExtensions
    {
        /// <summary>
        ///     Performs sort using Bubble Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseBubbleSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseBubbleSort(new TComparer());
        }

        /// <summary>
        ///     Performs sort using Bubble Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Lambda based comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseBubbleSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseBubbleSort(new LambdaComparer<T>(comparer));
        }  

        /// <summary>
        ///     Performs sort using Bubble Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseBubbleSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return new SortOptions<T>(new BubbleSortStrategy<T>(comparer));
        }


        /// <summary>
        ///     Performs sort using Insertion Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseInsertionSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseInsertionSort(new TComparer());
        }

        /// <summary>
        ///     Performs sort using Insertion Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Lambda based comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseInsertionSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseInsertionSort(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs sort using Insertion Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseInsertionSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return new SortOptions<T>(new InsertionSortStrategy<T>(comparer));
        }


        /// <summary>
        ///     Performs sort using Shell Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseShellSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseShellSort(new TComparer(), collection => collection.Count / 2);
        }

        /// <summary>
        ///     Performs sort using Shell Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <param name="incrementFactory">Factory function for calculating custom Increment Step to optimize sorting</param>
        /// <returns></returns>
        public static SortOptions<T> UseShellSort<T, TComparer>(this SortOptions<T> options, Func<IList<T>, int> incrementFactory)
            where TComparer : IComparer<T>, new()
        {
            return options.UseShellSort(new TComparer(), incrementFactory);
        }

        /// <summary>
        ///     Performs sort using Shell Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Lambda based comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseShellSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseShellSort(new LambdaComparer<T>(comparer), collection => collection.Count / 2);
        }

        /// <summary>
        ///     Performs sort using Shell Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Lambda based comparer for type T</param>
        /// <param name="incrementFactory">Factory function for calculating custom Increment Step to optimize sorting</param>
        /// <returns></returns>
        public static SortOptions<T> UseShellSort<T>(this SortOptions<T> options, Func<T, T, int> comparer, Func<IList<T>, int> incrementFactory)
        {
            return options.UseShellSort(new LambdaComparer<T>(comparer), incrementFactory);
        }

        /// <summary>
        ///     Performs sort using Shell Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseShellSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return options.UseShellSort(comparer, collection => collection.Count / 2);
        }

        /// <summary>
        ///     Performs sort using Shell Sort algorithm.
        ///     Performance:
        ///     Best:       O(n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <param name="incrementFactory">Factory function for calculating custom Increment Step to optimize sorting</param>
        /// <returns></returns>
        public static SortOptions<T> UseShellSort<T>(this SortOptions<T> options, IComparer<T> comparer, Func<IList<T>, int> incrementFactory)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (incrementFactory == null) throw new ArgumentNullException(nameof(incrementFactory));
            return new SortOptions<T>(new ShellSortStrategy<T>(comparer, incrementFactory));
        }


        /// <summary>
        ///     Performs sort using Selection Sort algorithm.
        ///     Performance:
        ///     Best:       O(n*n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseSelectionSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseSelectionSort(new TComparer());
        }

        /// <summary>
        ///     Performs sort using Selection Sort algorithm.
        ///     Performance:
        ///     Best:       O(n*n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Lambda based comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseSelectionSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseSelectionSort(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs sort using Selection Sort algorithm.
        ///     Performance:
        ///     Best:       O(n*n)
        ///     Worst:      O(n*n)
        ///     Average:    O(n*n)
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseSelectionSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return new SortOptions<T>(new SelectionSortStrategy<T>(comparer));
        }


        /// <summary>
        ///     Performs sort using Quick Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n*n)
        ///     Average:    O(n * Log(n))
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseQuickSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseQuickSort(new TComparer());
        }

        /// <summary>
        ///     Performs sort using Quick Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n*n)
        ///     Average:    O(n * Log(n))
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseQuickSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseQuickSort(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs sort using Quick Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n*n)
        ///     Average:    O(n * Log(n))
        ///     In Place
        ///     Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseQuickSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return new SortOptions<T>(new QuickSortStrategy<T>(comparer));
        }


        /// <summary>
        ///     Performs sort using Merge Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n * Log(n))
        ///     Average:    O(n * Log(n))
        ///     Not In Place
        ///     Not Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseMergeSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseMergeSort(new TComparer());
        }

        /// <summary>
        ///     Performs sort using Merge Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n * Log(n))
        ///     Average:    O(n * Log(n))
        ///     Not In Place
        ///     Not Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseMergeSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseMergeSort(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs sort using Merge Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n * Log(n))
        ///     Average:    O(n * Log(n))
        ///     Not In Place
        ///     Not Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseMergeSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return new SortOptions<T>(new MergeSortStrategy<T>(comparer));
        }


        /// <summary>
        ///     Performs sort using Heap Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n * Log(n))
        ///     Average:    O(n * Log(n))
        ///     Not In Place
        ///     Not Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SortOptions<T> UseHeapSort<T, TComparer>(this SortOptions<T> options)
            where TComparer : IComparer<T>, new()
        {
            return options.UseHeapSort(new TComparer());
        }

        /// <summary>
        ///     Performs sort using Heap Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n * Log(n))
        ///     Average:    O(n * Log(n))
        ///     Not In Place
        ///     Not Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseHeapSort<T>(this SortOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseHeapSort(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs sort using Heap Sort algorithm.
        ///     Performance:
        ///     Best:       O(n * Log(n))
        ///     Worst:      O(n * Log(n))
        ///     Average:    O(n * Log(n))
        ///     Not In Place
        ///     Not Stable
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SortOptions<T> UseHeapSort<T>(this SortOptions<T> options, IComparer<T> comparer)
        {
            return new SortOptions<T>(new HeapSortStrategy<T>(comparer));
        }
    }
}