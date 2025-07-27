using CareerWay.SearchService.API.Enums;

namespace CareerWay.SearchService.API.Models;

public class SearchPostResponse
{
    public long Id { get; set; }

    public SearchPostCompanyResponse Company { get; set; } = default!;

    public string Title { get; set; } = default!;

    public string Slug { get; set; } = default!;

    public PostStatus PostStatus { get; set; }

    public WorkingType WorkingType { get; set; }

    public PositionLevelType PositionLevelType { get; set; }

    public SearchPostCityResponse? City { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }
}

public class SearchPostCompanyResponse
{
    public long Id { get; set; }

    public string Title { get; set; } = default!;

    public string? ProfilePhoto { get; set; }
}

public class SearchPostCityResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}
