using CareerWay.ApplicationService.API.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.ApplicationService.API.Entities;

public class Application : Entity
{
    public Guid Id { get; set; }

    public long JobAdvertId { get; set; }

    public long JobSeekerId { get; set; }

    public string? Message { get; set; } = default!;

    public ApplicationStatus Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
}
