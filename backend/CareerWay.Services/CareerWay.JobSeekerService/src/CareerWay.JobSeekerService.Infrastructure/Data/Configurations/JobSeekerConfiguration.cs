using CareerWay.JobSeekerService.Domain.Consts;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Configurations;

public class JobSeekerConfiguration : IEntityTypeConfiguration<JobSeeker>
{
    public void Configure(EntityTypeBuilder<JobSeeker> builder)
    {
        builder.ToTable("JobSeekers", "JobSeeker").HasKey(o => o.Id);
        builder.Property(p => p.Id).IsRequired(true).ValueGeneratedNever();
        builder.Property(p => p.FirstName).HasMaxLength(JobSeekerConsts.MaxFirstNameLength).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(JobSeekerConsts.MaxLastNameLength).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(JobSeekerConsts.MaxEmailLength).IsRequired();
        builder.Property(p => p.AboutMe).HasMaxLength(JobSeekerConsts.MaxAboutMeLength).IsRequired(false);
        builder.Property(p => p.Interests).HasMaxLength(JobSeekerConsts.MaxInterestsLength).IsRequired(false);
        builder.Property(p => p.PhoneNumber).HasMaxLength(JobSeekerConsts.MaxPhoneNumberLength).IsRequired(false);
        builder.Property(p => p.CityId).IsRequired(false);
        builder.Property(p => p.Address).HasMaxLength(JobSeekerConsts.MaxAddressLength).IsRequired(false);
        builder.Property(p => p.WebSite).HasMaxLength(JobSeekerConsts.MaxWebSiteLength).IsRequired(false);
        builder.Property(p => p.Instragram).HasMaxLength(JobSeekerConsts.MaxInstragramLength).IsRequired(false);
        builder.Property(p => p.Facebook).HasMaxLength(JobSeekerConsts.MaxFacebookLength).IsRequired(false);
        builder.Property(p => p.Twitter).HasMaxLength(JobSeekerConsts.MaxTwitterLength).IsRequired(false);
        builder.Property(p => p.Linkedin).HasMaxLength(JobSeekerConsts.MaxLinkedinLength).IsRequired(false);
        builder.Property(p => p.Github).HasMaxLength(JobSeekerConsts.MaxGithubLength).IsRequired(false);
        builder.Property(p => p.ProfilePhoto).HasMaxLength(JobSeekerConsts.MaxProfilePhotoLength).IsRequired(false);
        builder.Property(p => p.BirthDate).IsRequired(false);
        builder.Property(p => p.DriverLicenseType).IsRequired(false);
        builder.Property(p => p.GenderType).IsRequired(false);
        builder.Property(p => p.MilitaryStatus).IsRequired(false);
        builder.Property(p => p.IsSmoking).IsRequired(false);
        builder.Property(p => p.ResumeVideo).HasMaxLength(JobSeekerConsts.MaxResumeVideoLength).IsRequired(false);
        builder.Property(p => p.ModificationDate).IsRequired(false);
    }
}
