namespace CareerWay.JobSeekerService.Application.Models;

public class CreateJobSeekerWorkExperienceRequest
{
    public int CityId { get; set; }

    public long PositionId { get; set; }

    public string JobTitle { get; set; } = default!;

    public string CompanyTitle { get; set; } = default!;

    public string Description { get; set; } = default!;

    public short StartMonth { get; set; }

    public short StartYear { get; set; }

    public short EndMonth { get; set; }

    public short EndYear { get; set; }

    public bool StillWorking { get; set; }
}
