namespace CareerWay.JobSeekerService.Application.Models;

public class CreateJobSeekerProjectRequest
{
    public short ProjectYear { get; set; }

    public short ProjectMonth { get; set; }

    public string? Link { get; set; }

    public string? Description { get; set; }
}
