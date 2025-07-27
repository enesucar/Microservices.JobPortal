using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerLangueConfiguration : IEntityTypeConfiguration<JobSeekerLanguage>
{
    public void Configure(EntityTypeBuilder<JobSeekerLanguage> builder)
    {
        builder.ToTable("JobSeekerLanguages", "JobSeeker").HasKey(o => o.Id);
    }
}

