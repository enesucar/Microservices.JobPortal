using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerReferenceConfiguration : IEntityTypeConfiguration<JobSeekerReference>
{
    public void Configure(EntityTypeBuilder<JobSeekerReference> builder)
    {
        builder.ToTable("JobSeekerReferences", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.FullName).HasMaxLength(JobSeekerReferenceConsts.MaxFullNameLength).IsRequired(true);
        builder.Property(p => p.PositionId).IsRequired(false);
        builder.Property(p => p.PhoneNumber).HasMaxLength(JobSeekerReferenceConsts.MaxPhoneNumberLength).IsRequired(false);
        builder.Property(p => p.CompanyName).HasMaxLength(JobSeekerReferenceConsts.MaxCompanyNameLength).IsRequired(false);
    }
}
