using CareerWay.JobAdvertService.Domain.Entities;
using MongoDB.Driver;

namespace CareerWay.JobAdvertService.Application.Interfaces;

public interface IJobAdvertReadDbContext
{
    IMongoCollection<City> Cities { get; }

    IMongoCollection<Departmant> Departmants { get; }

    IMongoCollection<Position> Positions { get; }

    IMongoCollection<Post> Posts { get; }

    IMongoCollection<PostEducationLevel> PostEducationLevels { get; }

    IMongoCollection<PostLanguageRequirement> PostLanguageRequirements { get; }

    IMongoCollection<PostMilitaryStatus> PostMilitaryStatuses { get; }

    IMongoCollection<PostSector> PostSectors { get; }

    IMongoCollection<PostWorkBenefit> PostWorkBenefits { get; }

    IMongoCollection<Question> Questions { get; }

    IMongoCollection<QuestionOption> QuestionOptions { get; }

    IMongoCollection<Sector> Sectors { get; }

    IMongoCollection<Skill> Skills { get; }
}
