using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CareerWay.IdentityService.Domain.Entities;

namespace CareerWay.IdentityService.Infrastructure.Data.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("RoleClaims", "Identity");
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}
