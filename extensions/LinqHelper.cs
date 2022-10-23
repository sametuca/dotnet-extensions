using System;
using System.Collections.Generic;
using System.Linq;

namespace Marti.Core.Extensions;

public static class LinqHelper
{
    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}