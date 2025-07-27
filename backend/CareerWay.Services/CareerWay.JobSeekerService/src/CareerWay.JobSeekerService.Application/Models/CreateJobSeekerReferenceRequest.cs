namespace CareerWay.JobSeekerService.Application.Models;

public class CreateJobSeekerReferenceRequest
{
    public string FullName { get; set; } = default!;

    public long? PositionId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CompanyName { get; set; }
}
