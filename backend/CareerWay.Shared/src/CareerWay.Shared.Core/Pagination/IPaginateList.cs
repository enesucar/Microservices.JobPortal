namespace CareerWay.Shared.Core.Pagination;

public interface IPaginateList
{
    int Page { get; set; }

    int Size { get; set; }

    int TotalPages { get; set; }

    int TotalCount { get; set; }

    bool HasPreviousPage { get; set; }

    bool HasNextPage { get; set; }

    bool IsFirstPage { get; set; }

    bool IsLastPage { get; set; }
}

public interface IPaginateList<T> : IPaginateList
{
    IReadOnlyCollection<T> Items { get; set; }
}
