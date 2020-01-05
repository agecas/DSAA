using System.Collections.Generic;

namespace DSAA.Graph.Traverse.Fluent
{
    public interface IBuildTraverseStrategy<out TValue>
    {
        IEnumerable<TValue> Build();
    }
}