using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.JobSeekerService.Domain.Entities;
using CareerWay.JobSeekerService.Domain.Enums;

namespace CareerWay.JobSeekerService.Application.Models;

public class JobSeekerDetailResponse
{
    public long Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string? AboutMe { get; set; }

    public string? Interests { get; set; }

    public string? PhoneNumber { get; set; }

    public JobSeekerCityDetailResponse? City { get; set; }

    public string? Address { get; set; }

    public string? WebSite { get; set; }

    public string? Instragram { get; set; }

    public string? Facebook { get; set; }

    public string? Twitter { get; set; }

    public string? Linkedin { get; set; }

    public string? Github { get; set; }

    public string? ProfilePhoto { get; set; }

    public DateTime? BirthDate { get; set; }

    public DriverLicenseType? DriverLicenseType { get; set; }

    public GenderType? GenderType { get; set; }

    public MilitaryStatus? MilitaryStatus { get; set; }

    public bool? IsSmoking { get; set; }

    public string? ResumeVideo { get; set; }

    public DateTime? ModificationDate { get; set; }

    public int IsDeleted { get; set; }

    public ICollection<JobSeekerJobSeekerCertificateItemDetailResponse> JobSeekerCertificates { get; set; } = [];

    public ICollection<JobSeekerJobSeekerWorkExperienceItemDetailResponse> WorkExperiences { get; set; } = [];

    public ICollection<JobSeekerJobSeekerSchoolItemDetailResponse> JobSeekerSchools { get; set; } = [];

    public ICollection<JobSeekerJobSeekerReferenceItemDetailResponse> JobSeekerReferences { get; set; } = [];

    public ICollection<JobSeekerJobSeekerLanguageItemDetailResponse> JobSeekerLanguages { get; set; } = [];

    public ICollection<JobSeekerJobSeekerProjectItemDetailResponse> JobSeekerProjects { get; set; } = [];

    public ICollection<JobSeekerJobSeekerSkillItemDetailResponse> JobSeekerSkills { get; set; } = [];
}

public class JobSeekerCityDetailResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

public class JobSeekerJobSeekerCertificateItemDetailResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public int ObtainedYear { get; set; }

    public int ObtainedMonth { get; set; }
}

public class JobSeekerJobSeekerWorkExperienceItemDetailResponse
{
    public Guid Id { get; set; }

    public JobSeekerJobSeekerWorkExperienceCityItemDetailResponse City { get; set; } = default!;

    public JobSeekerJobSeekerWorkExperiencePositionItemDetailResponse Position { get; set; } = default!;

    public string JobTitle { get; set; } = default!;

    public string CompanyTitle { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int StartMonth { get; set; }

    public int StartYear { get; set; }

    public int EndMonth { get; set; }

    public int EndYear { get; set; }

    public bool StillWorking { get; set; }
}

public class JobSeekerJobSeekerWorkExperienceCityItemDetailResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

public class JobSeekerJobSeekerWorkExperiencePositionItemDetailResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}


public class JobSeekerJobSeekerSchoolItemDetailResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public EducationLevelType EducationLevelType { get; set; }

    public int StartYear { get; set; }

    public int EndYear { get; set; }
}

public class JobSeekerJobSeekerLanguageItemDetailResponse
{
    public Guid Id { get; set; }

    public LanguageType LanguageType { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }
}

public class JobSeekerJobSeekerReferenceItemDetailResponse
{
    public Guid Id { get; set; }

    public long JobSeekerId { get; set; }

    public string FullName { get; set; } = default!;

    public JobSeekerJobSeekerReferencePositionItemDetailResponse? Position { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CompanyName { get; set; }
}

public class JobSeekerJobSeekerReferencePositionItemDetailResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}

public class JobSeekerJobSeekerProjectItemDetailResponse
{
    public Guid Id { get; set; }

    public int ProjectYear { get; set; }

    public int ProjectMonth { get; set; }

    public string? Link { get; set; }

    public string? Description { get; set; }
}

public class JobSeekerJobSeekerSkillItemDetailResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
