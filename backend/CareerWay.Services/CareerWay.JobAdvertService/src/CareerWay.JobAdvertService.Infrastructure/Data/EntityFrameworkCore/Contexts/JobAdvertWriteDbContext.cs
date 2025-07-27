using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CareerWay.JobAdvertService.Infrastructure.Data.EntityFrameworkCore.Contexts;

public class JobAdvertWriteDbContext : DbContext, IJobAdvertWriteDbContext
{
    public DbSet<City> Cities => Set<City>();

    public DbSet<Departmant> Departmants => Set<Departmant>();

    public DbSet<Position> Positions  => Set<Position>();

    public DbSet<Post> Posts  => Set<Post>();

    public DbSet<PostEducationLevel> PostEducationLevels  => Set<PostEducationLevel>();

    public DbSet<PostLanguageRequirement> PostLanguageRequirements  => Set<PostLanguageRequirement>();

    public DbSet<PostMilitaryStatus> PostMilitaryStatuses  => Set<PostMilitaryStatus>();

    public DbSet<PostSector> PostSectors  => Set<PostSector>();

    public DbSet<PostWorkBenefit> PostWorkBenefits  => Set<PostWorkBenefit>();

    public DbSet<Question> Questions  => Set<Question>();

    public DbSet<QuestionOption> QuestionOptions  => Set<QuestionOption>();

    public DbSet<Sector> Sectors  => Set<Sector>();

    public DbSet<Skill> Skills  => Set<Skill>();

    //public DbSet<CompanyPackage> CompanyPackages => Set<CompanyPackage>();

    public JobAdvertWriteDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("JobAdvert");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    } 
}
