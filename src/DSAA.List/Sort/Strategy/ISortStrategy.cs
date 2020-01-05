using System.Collections.Generic;

namespace DSAA.List.Sort.Strategy
{
    public interface ISortStrategy<T>
    {
        IList<T> Sort(IList<T> collection);
    }
}