using CareerWay.CompanyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerWay.CompanyService.Infrastructure.Data.Configurations;

public class CompanyPackageConfiguration : IEntityTypeConfiguration<CompanyPackage>
{
    public void Configure(EntityTypeBuilder<CompanyPackage> builder)
    {
        builder.ToTable("CompanyPackage", "Company").HasKey(o => o.Id);
        builder.Property(p => p.Id).IsRequired(true).ValueGeneratedNever();
    }
}
