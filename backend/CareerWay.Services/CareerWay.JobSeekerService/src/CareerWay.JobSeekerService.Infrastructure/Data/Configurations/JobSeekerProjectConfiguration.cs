using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerProjectConfiguration : IEntityTypeConfiguration<JobSeekerProject>
{
    public void Configure(EntityTypeBuilder<JobSeekerProject> builder)
    {
        builder.ToTable("JobSeekerProjects", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.Link).HasMaxLength(JobSeekerProjectConsts.MaxLinkLength).IsRequired(false);
        builder.Property(p => p.Description).HasMaxLength(JobSeekerProjectConsts.MaxDescriptionLength).IsRequired(false);
    }
}
