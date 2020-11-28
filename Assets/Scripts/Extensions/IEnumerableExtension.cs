using System.Collections.Generic;

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
}
