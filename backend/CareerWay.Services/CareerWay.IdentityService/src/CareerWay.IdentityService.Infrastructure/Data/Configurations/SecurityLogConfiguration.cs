using CareerWay.IdentityService.Domain.Constants;
using CareerWay.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerWay.IdentityService.Infrastructure.Data.Configurations;

public class SecurityLogConfiguration : IEntityTypeConfiguration<SecurityLog>
{
    public void Configure(EntityTypeBuilder<SecurityLog> builder)
    {
        builder.ToTable("SecurityLogs", "Identity");
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Action).HasMaxLength(SecurityLogConsts.MaxActionLength).IsRequired();
        builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();
    }
}
