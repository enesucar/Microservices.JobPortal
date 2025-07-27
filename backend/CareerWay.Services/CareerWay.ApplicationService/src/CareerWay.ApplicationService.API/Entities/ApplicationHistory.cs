using CareerWay.ApplicationService.API.Enums;

namespace CareerWay.ApplicationService.API.Entities;

public class ApplicationHistory
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public ApplicationStatus Status { get; set; }

    public string? Note { get; set; }

    public DateTime CreationDate { get; set; }
}
