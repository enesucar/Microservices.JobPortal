using CareerWay.Shared.Core.Guards;
using System.Linq.Dynamic.Core;

namespace CareerWay.Shared.Core.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TSource> Order<TSource>(
        this IQueryable<TSource> query,
        string ordering)
    {
        Guard.Against.Null(query, nameof(query));
        return string.IsNullOrWhiteSpace(ordering)
            ? query
            : query.OrderBy(ordering);
    }

    public static IQueryable<TSource> Page<TSource>(
        this IQueryable<TSource> query,
        int page,
        int size)
    {
        Guard.Against.Null(query, nameof(query));
        return query.Page(page: page, pageSize: size);
    }
}
