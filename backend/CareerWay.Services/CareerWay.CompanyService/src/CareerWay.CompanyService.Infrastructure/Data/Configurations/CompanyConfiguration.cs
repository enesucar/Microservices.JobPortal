using CareerWay.CompanyService.Domain.Constants;
using CareerWay.CompanyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerWay.CompanyService.Infrastructure.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies", "Company").HasKey(o => o.Id);
        builder.Property(p => p.Id).IsRequired(true).ValueGeneratedNever();
        builder.Property(p => p.Title).HasMaxLength(CompanyConsts.MaxTitleLength).IsRequired();
        builder.Property(p => p.FirstName).HasMaxLength(CompanyConsts.MaxFirstNameLength).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(CompanyConsts.MaxLastNameLength).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(CompanyConsts.MaxEmailLength).IsRequired();
        builder.Property(p => p.Description).IsRequired(false);
        builder.Property(p => p.CityId).IsRequired(false);
        builder.Property(p => p.Address).HasMaxLength(CompanyConsts.MaxAddressLength).IsRequired(false);
        builder.Property(p => p.ProfilePhoto).IsRequired(false);
        builder.Property(p => p.WebSite).HasMaxLength(CompanyConsts.MaxWebSiteLength).IsRequired(false);
        builder.Property(p => p.Instragram).HasMaxLength(CompanyConsts.MaxInstragramLength).IsRequired(false);
        builder.Property(p => p.Facebook).HasMaxLength(CompanyConsts.MaxFacebookLength).IsRequired(false);
        builder.Property(p => p.Twitter).HasMaxLength(CompanyConsts.MaxTwitterLength).IsRequired(false);
        builder.Property(p => p.Linkedin).HasMaxLength(CompanyConsts.MaxLinkedinLength).IsRequired(false);
        builder.Property(p => p.EstablishmentYear).IsRequired(false);
        builder.Property(p => p.Status).IsRequired(true);
        builder.Property(p => p.CreationDate).IsRequired(true);
    }
}
