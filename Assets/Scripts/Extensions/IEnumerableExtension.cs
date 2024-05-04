using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtension
{
    /// <summary>
    /// Wraps this object instance into an IEnumerable<T>;
    /// consisting of a single item.
    /// </summary>
    public static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }

    public static T2 FirstOfTypeOrDefault<T1, T2>(this IEnumerable<T1> collection)
        where T2 : T1
    {
        foreach (var item in collection)
        {
            if (item is T2)
                return (T2)item;
        }

        return default(T2);
    }

    public static T FirstOfTypeOrDefault<T>(this IEnumerable<T> collection, Type type)
    {
        foreach (T item in collection)
        {
            if (typeof(T) == type)
                return item;
        }

        return default;
    }
}
