using CareerWay.Web.Enums;
using Web.Domain.Enums;

namespace CareerWay.Web.Models.Post;

public class PostDetail
{
    public long Id { get; set; }

    public long CompanyId { get; set; }

    public string Title { get; set; } = default!;

    public int PackageId { get; set; }

    public string Slug { get; set; } = default!;

    public string Description { get; set; } = default!;

    public WorkingType WorkingType { get; set; }

    public PositionLevelType PositionLevelType { get; set; }

    public DriverLicenseType? DriverLicenseType { get; set; }

    public GenderType GenderType { get; set; }

    public ExperienceType ExperienceType { get; set; }

    public GetPostDepartmantDetailResponse Departmant { get; set; } = default!;

    public GetPostPositionDetailResponse Position { get; set; } = default!;

    public GetPostCityDetailResponse? City { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public byte? MinAge { get; set; }

    public byte? MaxAge { get; set; }

    public byte? MinExperienceYear { get; set; }

    public byte? MaxExperienceYear { get; set; }

    public bool IsDisabledOnly { get; set; }

    public long ViewCount { get; set; }

    public PostStatus Status { get; set; }

    public DateTime? PublicationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public List<GetPostPostEducationLevelDetailResponse> PostEducationLevels { get; set; } = [];

    public List<GetPostPostLanguageRequirementDetailResponse> PostLanguageRequirements { get; set; } = [];

    public List<GetPostPostMilitaryStatusDetailResponse> PostMilitaryStatuses { get; set; } = [];

    public List<GetPostPostSectorDetailResponse> PostSectors { get; set; } = [];

    public List<GetPostPostSkillDetailResponse> PostSkills { get; set; } = [];

}

public class GetPostDepartmantDetailResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}

public class GetPostPositionDetailResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}

public class GetPostCityDetailResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

public class GetPostPostEducationLevelDetailResponse
{
    public PostEducationLevelType PostEducationLevelType { get; set; }
}

public class GetPostPostLanguageRequirementDetailResponse
{
    public LanguageType LanguageType { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }
}

public class GetPostPostMilitaryStatusDetailResponse
{
    public MilitaryStatus MilitaryStatus { get; set; }
}

public class GetPostPostSectorDetailResponse
{
    public string Name { get; set; } = default!;
}

public class GetPostPostSkillDetailResponse
{
    public string Name { get; set; } = default!;
}