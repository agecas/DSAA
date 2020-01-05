using System.Collections.Generic;

namespace DSAA.Shared
{
    public static class ListExtensions
    {
        public static TCollection Swap<T, TCollection>(this TCollection collection, int index1, int index2)
            where TCollection : IList<T>
        {
            var temp = collection[index2];
            collection[index2] = collection[index1];
            collection[index1] = temp;

            return collection;
        }
    }
}