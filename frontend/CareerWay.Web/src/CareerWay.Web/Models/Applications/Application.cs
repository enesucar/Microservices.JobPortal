using Duende.IdentityModel;

namespace CareerWay.Web.Models.Applications;

public class Application
{
    public Guid Id { get; set; }

    public long JobAdvertId { get; set; }

    public long JobSeekerId { get; set; }

    public string? Message { get; set; } = default!;

    public ApplicationStatus Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
}
