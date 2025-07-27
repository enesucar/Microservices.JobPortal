using CareerWay.JobSeekerService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeeker : Entity
{
    public virtual long Id { get; set; }

    public virtual string FirstName { get; set; } = default!;

    public virtual string LastName { get; set; } = default!;

    public virtual string Email { get; set; } = default!;

    public virtual string? AboutMe { get; set; }

    public virtual string? Interests { get; set; }

    public virtual string? PhoneNumber { get; set; }

    public virtual int? CityId { get; set; }

    public virtual string? Address { get; set; }

    public virtual string? WebSite { get; set; }

    public virtual string? Instragram { get; set; }

    public virtual string? Facebook { get; set; }

    public virtual string? Twitter { get; set; }

    public virtual string? Linkedin { get; set; }

    public virtual string? Github { get; set; }

    public virtual string? ProfilePhoto { get; set; }

    public virtual DateTime? BirthDate { get; set; }

    public virtual DriverLicenseType? DriverLicenseType { get; set; }

    public virtual GenderType? GenderType { get; set; }

    public virtual MilitaryStatus? MilitaryStatus { get; set; }

    public virtual bool? IsSmoking { get; set; }

    public virtual string? ResumeVideo { get; set; }

    public virtual DateTime CreationDate { get; set; }

    public virtual DateTime? ModificationDate { get; set; }

    public virtual int IsDeleted { get; set; }

    public virtual City City { get; set; } = default!;

    public virtual ICollection<JobSeekerCertificate> JobSeekerCertificates { get; set; } = [];

    public virtual ICollection<JobSeekerWorkExperience> WorkExperiences { get; set; } = [];

    public virtual ICollection<JobSeekerSchool> JobSeekerSchools { get; set; } = [];

    public virtual ICollection<JobSeekerReference> JobSeekerReferences { get; set; } = [];

    public virtual ICollection<JobSeekerLanguage> JobSeekerLanguages { get; set; } = [];

    public virtual ICollection<JobSeekerProject> JobSeekerProjects { get; set; } = [];

    public virtual ICollection<JobSeekerSkill> JobSeekerSkills { get; set; } = [];
}
