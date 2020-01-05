using System.Collections.Generic;

namespace DSAA.Tree.Traverse.Fluent
{
    public interface IBuildTraverseStrategy<out TValue>
    {
        IEnumerable<TValue> Build();
    }
}