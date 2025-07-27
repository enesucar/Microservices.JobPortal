using CareerWay.ApplicationService.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.ApplicationService.API.Data.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Application> Applications => Set<Application>();

    public DbSet<ApplicationHistory> ApplicationHistories => Set<ApplicationHistory>();

    public DbSet<InterviewSchedule> InterviewSchedules => Set<InterviewSchedule>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Application");
        base.OnModelCreating(builder);
    }
}
