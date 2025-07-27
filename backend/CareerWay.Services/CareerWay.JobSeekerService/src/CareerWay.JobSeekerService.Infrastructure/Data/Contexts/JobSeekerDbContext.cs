using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CareerWay.JobSeekerService.Infrastructure.Data.Contexts;

public class JobSeekerDbContext : DbContext, IJobSeekerDbContext
{
    public DbSet<City> Cities => Set<City>();

    public DbSet<JobSeeker> JobSeekers => Set<JobSeeker>();

    public DbSet<JobSeekerCertificate> JobSeekerCertificates => Set<JobSeekerCertificate>();

    public DbSet<JobSeekerLanguage> JobSeekerLanguages => Set<JobSeekerLanguage>();

    public DbSet<JobSeekerProject> JobSeekerProjects => Set<JobSeekerProject>();

    public DbSet<JobSeekerReference> JobSeekerReferences => Set<JobSeekerReference>();

    public DbSet<JobSeekerSchool> JobSeekerSchools => Set<JobSeekerSchool>();

    public DbSet<JobSeekerSkill> JobSeekerSkills => Set<JobSeekerSkill>();

    public DbSet<JobSeekerWorkExperience> JobSeekerWorkExperiences => Set<JobSeekerWorkExperience>();

    public DbSet<Position> Positions => Set<Position>();

    public DbSet<Skill> Skills => Set<Skill>();

    public JobSeekerDbContext(DbContextOptions<JobSeekerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
