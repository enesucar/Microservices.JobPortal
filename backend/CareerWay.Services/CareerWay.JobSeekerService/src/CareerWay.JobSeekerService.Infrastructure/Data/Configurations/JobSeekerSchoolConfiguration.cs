using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerSchoolConfiguration : IEntityTypeConfiguration<JobSeekerSchool>
{
    public void Configure(EntityTypeBuilder<JobSeekerSchool> builder)
    {
        builder.ToTable("JobSeekerSchools", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.Name).HasMaxLength(JobSeekerSchoolConsts.MaxNameLength);
    }
}
