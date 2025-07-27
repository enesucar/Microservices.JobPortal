using CareerWay.CompanyService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.CompanyService.Domain.Entities;

public class Company : Entity
{
    public virtual long Id { get; set; }

    public virtual string Title { get; set; } = default!;

    public virtual string FirstName { get; set; } = default!;

    public virtual string LastName { get; set; } = default!;

    public virtual string Email { get; set; } = default!;

    public virtual string? Description { get; set; }

    public virtual int? CityId { get; set; }

    public virtual string? Address { get; set; }

    public virtual string? ProfilePhoto { get; set; }

    public virtual string? WebSite { get; set; }

    public virtual string? Instragram { get; set; }

    public virtual string? Facebook { get; set; }

    public virtual string? Twitter { get; set; }

    public virtual string? Linkedin { get; set; }

    public virtual short? EstablishmentYear { get; set; }

    public virtual CompanyStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public virtual DateTime CreationDate { get; set; }

    public virtual City City { get; set; } = default!;

    public virtual List<CompanyPackage> CompanyPackages { get; set; } = default!;
}
