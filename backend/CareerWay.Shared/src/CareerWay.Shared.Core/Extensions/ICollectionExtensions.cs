namespace System.Collections.Generic;

public static class ICollectionExtensions
{
    public static void AddIf<T>(this ICollection<T> collection, T item, bool condition)
    {
        List<int> a = new List<int>();
        a.AddIf(5, o => o == 1);
        if (condition)
        {
            collection.Add(item);
        }
    }

    public static void AddIf<T>(this ICollection<T> collection, T item, Func<T, bool> predicate)
    {
        if (predicate(item))
        {
            collection.Add(item);
        }
    }
}
