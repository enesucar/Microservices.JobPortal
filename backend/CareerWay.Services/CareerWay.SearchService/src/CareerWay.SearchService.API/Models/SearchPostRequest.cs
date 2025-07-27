using CareerWay.SearchService.API.Enums;

namespace CareerWay.SearchService.API.Models;

public class SearchPostRequest
{
    public string? Text { get; set; }

    public List<long>? Posts { get; set; }

    public List<int>? Cities { get; set; }

    public long? CompanyId { get; set; }

    public List<WorkingType>? WorkingType { get; set; }

    public List<long>? Sectors { get; set; }

    public List<long>? Positions { get; set; }

    public List<long>? Departmants { get; set; }

    public PostStatus? PostStatus { get; set; }

    public List<PostEducationLevelType>? EducationLevel { get; set; }

    public bool? IsDisabledOnly { get; set; }

    public long? UserId { get; set; }
}
