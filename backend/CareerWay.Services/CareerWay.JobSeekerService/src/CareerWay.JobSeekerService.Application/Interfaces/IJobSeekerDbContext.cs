using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Interfaces;

public interface IJobSeekerDbContext
{
    DbSet<City> Cities { get; }

    DbSet<JobSeeker> JobSeekers { get; }

    DbSet<JobSeekerCertificate> JobSeekerCertificates { get; }

    DbSet<JobSeekerLanguage> JobSeekerLanguages { get; }

    DbSet<JobSeekerProject> JobSeekerProjects { get; }

    DbSet<JobSeekerReference> JobSeekerReferences { get; }

    DbSet<JobSeekerSchool> JobSeekerSchools { get; }

    DbSet<JobSeekerSkill> JobSeekerSkills { get; }

    DbSet<JobSeekerWorkExperience> JobSeekerWorkExperiences { get; }

    DbSet<Position> Positions { get; }

    DbSet<Skill> Skills { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
