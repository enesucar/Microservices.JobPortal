namespace System.Collections.Generic;

public static class ListExtensions
{
    public static T? Previous<T>(this List<T> source, T item)
    {
        //Guard.Against.Null(source, nameof(source));

        var index = source.FindIndex(o => EqualityComparer<T>.Default.Equals(o, item));
        return index == -1
            ? default
            : index == 0
                ? item
                : source[index - 1];
    }
}
