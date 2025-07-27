using CareerWay.ApplicationService.API.Enums;

namespace CareerWay.ApplicationService.API.Models;

public class ApplicantionsResponse
{
    public List<ApplicantionsJobSeekerResponse> JobSeekers { get; set; } = [];
}

public class ApplicantionsJobSeekerResponse
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ProfilePhoto { get; set; }

    public ApplicationStatus Status { get; set; }

    public DateTime CreationDate { get; set; }
}
