using System;
using System.Collections.Generic;

namespace DSAA.Graph.Fluent
{
    public interface ISetComparer<T> : IBuildGraph<T>
    {
        IBuildGraph<T> CompareUsing<TComparer>() where TComparer : IEqualityComparer<T>, new();
        IBuildGraph<T> CompareUsing(IEqualityComparer<T> comparer);
        IBuildGraph<T> CompareUsing(Func<T, T, bool> comparer);
    }
}