using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills", "JobSeeker").HasKey(o => o.Id);
        builder.Property(o => o.Id).IsRequired(true).ValueGeneratedNever();
        builder.Property(p => p.Name).HasMaxLength(SkillConsts.MaxNameLength);
    }
}
