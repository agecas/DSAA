using System;
using System.Collections.Generic;
using DSAA.List.Search.Strategy;
using DSAA.Shared;

namespace DSAA.List.Search
{
    public static class SearchOptionsExtensions
    {
        /// <summary>
        ///     Performs search using Linear Search algorithm.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(n)
        ///     Average:    O(1)
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseLinearSearch<T>(this SearchOptions<T> options)
        {
            return SearchOptions<T>.Default();
        }

        /// <summary>
        ///     Performs search using HashTable.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(n)
        ///     Average:    O(n)
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseHashTableSearch<T>(this SearchOptions<T> options)
        {
            return new SearchOptions<T>(new HashSearch<T>());
        }

        /// <summary>
        ///     Performs search using BinarySearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(Log(n))
        ///     Average:    O(Log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseBinarySearch<T, TComparer>(this SearchOptions<T> options) where TComparer : IComparer<T>, new()
        {
            return options.UseBinarySearch(new TComparer());
        }

        /// <summary>
        ///     Performs search using BinarySearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(Log(n))
        ///     Average:    O(Log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseBinarySearch<T>(this SearchOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseBinarySearch(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs search using BinarySearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(Log(n))
        ///     Average:    O(Log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseBinarySearch<T>(this SearchOptions<T> options, IComparer<T> comparer) 
        {
            return new SearchOptions<T>(new BinarySearch<T>(comparer));
        }


        /// <summary>
        ///     Performs search using TernarySearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(Log(n))
        ///     Average:    O(Log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseTernarySearch<T, TComparer>(this SearchOptions<T> options) where TComparer : IComparer<T>, new()
        {
            return options.UseTernarySearch(new TComparer());
        }

        /// <summary>
        ///     Performs search using TernarySearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(Log(n))
        ///     Average:    O(Log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseTernarySearch<T>(this SearchOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseTernarySearch(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs search using TernarySearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(Log(n))
        ///     Average:    O(Log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseTernarySearch<T>(this SearchOptions<T> options, IComparer<T> comparer) 
        {
            return new SearchOptions<T>(new TernarySearch<T>(comparer));
        }


        /// <summary>
        ///     Performs search using JumpSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(sqrt(n))
        ///     Average:    O(sqrt(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseJumpSearch<T, TComparer>(this SearchOptions<T> options) where TComparer : IComparer<T>, new()
        {
            return options.UseJumpSearch(new TComparer());
        }

        /// <summary>
        ///     Performs search using JumpSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(sqrt(n))
        ///     Average:    O(sqrt(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseJumpSearch<T>(this SearchOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseJumpSearch(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs search using JumpSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(sqrt(n))
        ///     Average:    O(sqrt(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseJumpSearch<T>(this SearchOptions<T> options, IComparer<T> comparer) 
        {
            return new SearchOptions<T>(new JumpSearch<T>(comparer));
        }


        /// <summary>
        ///     Performs search using ExponentialSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(log(n))
        ///     Average:    O(log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseExponentialSearch<T, TComparer>(this SearchOptions<T> options) where TComparer : IComparer<T>, new()
        {
            return options.UseExponentialSearch(new TComparer());
        }

        /// <summary>
        ///     Performs search using ExponentialSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(log(n))
        ///     Average:    O(log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseExponentialSearch<T>(this SearchOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseExponentialSearch(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs search using ExponentialSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(log(n))
        ///     Average:    O(log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseExponentialSearch<T>(this SearchOptions<T> options, IComparer<T> comparer) 
        {
            return new SearchOptions<T>(new ExponentialSearch<T>(comparer));
        }


        /// <summary>
        ///     Performs search using FibonacciSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(log(n))
        ///     Average:    O(log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <typeparam name="TComparer">IComparer implementation for type T</typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SearchOptions<T> UseFibonacciSearch<T, TComparer>(this SearchOptions<T> options) where TComparer : IComparer<T>, new()
        {
            return options.UseFibonacciSearch(new TComparer());
        }

        /// <summary>
        ///     Performs search using FibonacciSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(log(n))
        ///     Average:    O(log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseFibonacciSearch<T>(this SearchOptions<T> options, Func<T, T, int> comparer)
        {
            return options.UseFibonacciSearch(new LambdaComparer<T>(comparer));
        }

        /// <summary>
        ///     Performs search using FibonacciSearch algorithm. Input list needs to be SORTED.
        ///     Performance:
        ///     Best:       O(1)
        ///     Worst:      O(log(n))
        ///     Average:    O(log(n))
        /// </summary>
        /// <typeparam name="T">Type contained in the collection</typeparam>
        /// <param name="options"></param>
        /// <param name="comparer">Comparer for type T</param>
        /// <returns></returns>
        public static SearchOptions<T> UseFibonacciSearch<T>(this SearchOptions<T> options, IComparer<T> comparer) 
        {
            return new SearchOptions<T>(new FibonacciSearch<T>(comparer));
        }
    }
}