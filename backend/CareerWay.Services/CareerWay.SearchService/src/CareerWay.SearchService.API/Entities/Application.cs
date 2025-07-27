using CareerWay.SearchService.API.Consts;
using Nest;

namespace CareerWay.SearchService.API.Entities;

[ElasticsearchType(IdProperty = "id", RelationName = ApplicationConsts.IndexName)]
public class Application
{
    [PropertyName("id")]
    public Guid Id { get; set; }

    [PropertyName("post_id")]
    public long PostId { get; set; }

    [PropertyName("user_id")]
    public long UserId { get; set; }
}
