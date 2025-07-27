namespace CareerWay.JobSeekerService.Application.Models;

public class CreateJobSeekerCertificateRequest
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public short ObtainedYear { get; set; }

    public short ObtainedMonth { get; set; }
}
