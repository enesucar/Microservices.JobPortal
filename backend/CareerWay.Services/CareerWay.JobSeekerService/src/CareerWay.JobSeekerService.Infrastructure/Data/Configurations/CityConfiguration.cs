using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.Id).IsRequired().ValueGeneratedNever();
        builder.Property(p => p.Name).HasMaxLength(CityConsts.MaxNameLength);
    }
}
