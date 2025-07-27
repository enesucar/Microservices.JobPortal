using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerCertificateConfiguration : IEntityTypeConfiguration<JobSeekerCertificate>
{
    public void Configure(EntityTypeBuilder<JobSeekerCertificate> builder)
    {
        builder.ToTable("JobSeekerCertificates", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.Name).HasMaxLength(JobSeekerCertificateConsts.MaxNameLength).IsRequired(true);
        builder.Property(p => p.Description).HasMaxLength(JobSeekerCertificateConsts.MaxDescriptionLength).IsRequired(false);
    }
}
