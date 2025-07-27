using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CareerWay.IdentityService.Domain.Constants;
using CareerWay.IdentityService.Domain.Entities;

namespace CareerWay.IdentityService.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "Identity").HasKey(o => o.Id);
        builder.Property(p => p.Id).IsRequired(true);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.UserName).IsRequired(false);
        builder.Property(p => p.NormalizedUserName).IsRequired(false);
        builder.Property(p => p.Email).IsRequired(true);
        builder.Property(p => p.NormalizedEmail).IsRequired(true);
        builder.Property(p => p.PhoneNumber).IsRequired(false);
        builder.Property(p => p.LastLoginDate).HasColumnType("datetime").IsRequired(true);
        builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired(true);
        builder.Property(p => p.DeletionDate).HasColumnType("datetime").IsRequired(false);
    }
}
