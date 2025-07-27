using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Enums;
using Nest;

namespace CareerWay.SearchService.API.Entities;

[ElasticsearchType(IdProperty = "id", RelationName = PostConsts.IndexName)]
public class Post
{
    [PropertyName("id")]
    public long Id { get; set; }

    [PropertyName("company_id")]
    public long CompanyId { get; set; }

    [PropertyName("title")]
    public string Title { get; set; } = default!;

    [PropertyName("slug")]
    public string Slug { get; set; } = default!;

    [PropertyName("description")]
    public string Description { get; set; } = default!;

    [PropertyName("working_type")]
    public WorkingType WorkingType { get; set; }

    [PropertyName("position_level_type")]
    public PositionLevelType PositionLevelType { get; set; }

    [PropertyName("education_level_types")]
    public List<PostEducationLevelType> EducationLevelTypes { get; set; } = [];

    [PropertyName("departmant")]
    public Departmant Departmant { get; set; } = default!;

    [PropertyName("position")]
    public Position Position { get; set; } = default!;

    [PropertyName("sectors")]
    public List<Sector> Sectors { get; set; } = [];

    [PropertyName("city")]
    public City? City { get; set; }

    [PropertyName("is_disabled_only")]
    public bool IsDisabledOnly { get; set; }

    [PropertyName("status")]
    public PostStatus Status { get; set; }

    [PropertyName("publication_date")]
    public DateTime? PublicationDate { get; set; }

    [PropertyName("expiration_date")]
    public DateTime? ExpirationDate { get; set; }

    [PropertyName("creation_date")]
    public DateTime CreationDate { get; set; }
}
