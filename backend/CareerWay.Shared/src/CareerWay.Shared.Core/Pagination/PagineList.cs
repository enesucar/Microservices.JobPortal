namespace CareerWay.Shared.Core.Pagination;

public class PaginateList : IPaginateList
{
    public int Page { get; set; }

    public int Size { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public bool IsFirstPage { get; set; }

    public bool IsLastPage { get; set; }

    public PaginateList(
        int page,
        int size,
        int totalCount)
    {
        Page = page;
        Size = size;
        TotalPages = (int)Math.Ceiling(totalCount / (double)Size);
        TotalCount = totalCount;
        HasPreviousPage = page > 1;
        HasNextPage = page < TotalPages;
        IsFirstPage = page == 1;
        IsLastPage = page == TotalPages;
    }
}

public class PaginateList<T> : PaginateList, IPaginateList<T>
{
    public IReadOnlyCollection<T> Items { get; set; }

    public PaginateList(
        IReadOnlyCollection<T> items,
        int page,
        int size,
        int totalCount)
        : base(page, size, totalCount)
    {
        Items = items;
    }
}
