using CareerWay.SearchService.API.Consts;
using Nest;

namespace CareerWay.SearchService.API.Entities;

[ElasticsearchType(IdProperty = "id", RelationName = CompanyConsts.IndexName)]
public class Company
{
    [PropertyName("id")]
    public long Id { get; set; }

    [PropertyName("title")]
    public string Title { get; set; } = default!;

    [PropertyName("profile_photo")]

    public string? ProfilePhoto { get; set; }
}
