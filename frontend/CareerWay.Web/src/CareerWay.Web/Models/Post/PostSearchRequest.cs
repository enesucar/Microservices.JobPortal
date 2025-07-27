using Newtonsoft.Json;
using Web.Domain.Enums;

namespace CareerWay.Web.Models.Post;

public class PostSearchRequest
{
    public string? Text { get; set; }

    public long? CompanyId { get; set; }

    public List<long> Positions { get; set; } = [];

    public List<long> Departmants { get; set; } = [];

    public PostStatus? PostStatus { get; set; }

    public long? UserId { get; set; }

    [JsonIgnore]
    public List<SearchPostResponse> Data { get; set; } = [];
}
