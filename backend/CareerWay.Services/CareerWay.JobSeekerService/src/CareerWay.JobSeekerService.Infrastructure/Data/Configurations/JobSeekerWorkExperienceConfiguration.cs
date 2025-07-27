using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerWorkExperienceConfiguration : IEntityTypeConfiguration<JobSeekerWorkExperience>
{
    public void Configure(EntityTypeBuilder<JobSeekerWorkExperience> builder)
    {
        builder.ToTable("JobSeekerWorkExperiences", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.JobTitle).HasMaxLength(JobSeekerWorkExperienceConsts.MaxJobTitleLength).IsRequired(true);
        builder.Property(p => p.CompanyTitle).HasMaxLength(JobSeekerWorkExperienceConsts.MaxCompanyTitleLength).IsRequired(true);
        builder.Property(p => p.Description).HasMaxLength(JobSeekerWorkExperienceConsts.MaxDescriptionLength).IsRequired(true);
    }
}
