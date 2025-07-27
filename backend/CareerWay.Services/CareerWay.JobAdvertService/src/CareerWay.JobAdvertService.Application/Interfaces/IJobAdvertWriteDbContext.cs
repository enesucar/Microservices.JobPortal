using CareerWay.JobAdvertService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobAdvertService.Application.Interfaces;

public interface IJobAdvertWriteDbContext
{
    DbSet<City> Cities { get; }

    DbSet<Departmant> Departmants { get; }

    DbSet<Position> Positions { get; }

    DbSet<Post> Posts { get; }
    
    DbSet<PostEducationLevel> PostEducationLevels { get; }
    
    DbSet<PostLanguageRequirement> PostLanguageRequirements { get; }
    
    DbSet<PostMilitaryStatus> PostMilitaryStatuses { get; }
    
    DbSet<PostSector> PostSectors { get; }
    
    DbSet<PostWorkBenefit> PostWorkBenefits { get; }
    
    DbSet<Question> Questions { get; }
    
    DbSet<QuestionOption> QuestionOptions { get; }
    
    DbSet<Sector> Sectors { get; }
    
    DbSet<Skill> Skills { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
