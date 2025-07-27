using CareerWay.JobAdvertService.Domain.Enums;

namespace CareerWay.JobSeekerService.Application.Models;

public class CreateJobSeekerSchoolRequest
{
    public string Name { get; set; } = default!;

    public EducationLevelType EducationLevelType { get; set; }

    public short StartYear { get; set; }

    public short EndYear { get; set; }
}
