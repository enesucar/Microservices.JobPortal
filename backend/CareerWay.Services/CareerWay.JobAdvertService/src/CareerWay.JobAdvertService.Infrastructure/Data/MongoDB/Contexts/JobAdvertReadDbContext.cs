using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.Shared.MongoDB.Contexts;
using CareerWay.Shared.MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CareerWay.JobAdvertService.Infrastructure.Data.MongoDB.Contexts;

public class JobAdvertReadDbContext : MongoDbContext, IJobAdvertReadDbContext
{
    public IMongoCollection<City> Cities => Set<City>("Cities");

    public IMongoCollection<Departmant> Departmants => Set<Departmant>("Departmants");

    public IMongoCollection<Position> Positions => Set<Position>("Positions");

    public IMongoCollection<Post> Posts => Set<Post>("Posts");

    public IMongoCollection<PostEducationLevel> PostEducationLevels => Set<PostEducationLevel>("PostEducationLevels");

    public IMongoCollection<PostLanguageRequirement> PostLanguageRequirements => Set<PostLanguageRequirement>("PostLanguageRequirements");

    public IMongoCollection<PostMilitaryStatus> PostMilitaryStatuses => Set<PostMilitaryStatus>("PostMilitaryStatuses");

    public IMongoCollection<PostSector> PostSectors => Set<PostSector>("PostSectors");

    public IMongoCollection<PostWorkBenefit> PostWorkBenefits => Set<PostWorkBenefit>("PostWorkBenefits");

    public IMongoCollection<Question> Questions => Set<Question>("Questions");

    public IMongoCollection<QuestionOption> QuestionOptions => Set<QuestionOption>("QuestionOptions");

    public IMongoCollection<Sector> Sectors => Set<Sector>("Sectors");

    public IMongoCollection<Skill> Skills => Set<Skill>("Skills");


    public JobAdvertReadDbContext(IOptions<MongoDbOptions> mongoDbOptions) 
        : base(mongoDbOptions)
    {
    }
}
