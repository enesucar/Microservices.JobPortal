using CareerWay.Web.Enums;

namespace CareerWay.Web.Models.JobSeekers;

public class CreateJobSeekerSchoolRequest
{
    public string Name { get; set; } = default!;

    public EducationLevelType EducationLevelType { get; set; }

    public int StartYear { get; set; }

    public int EndYear { get; set; }
}
