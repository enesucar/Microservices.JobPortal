using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Queries.GetList;
using CareerWay.JobAdvertService.Application.Features.Posts.Queries.GetById;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Caching;
using CareerWay.Shared.Caching.Redis;
using IdGen;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StackExchange.Redis;

namespace CareerWay.JobAdvertService.Application.Features.Posts.Queries.GetDetail;

public class GetPostDetailQueryHandler : IRequestHandler<GetPostDetailQuery, GetPostDetailResponse>
{
    private readonly IJobAdvertReadDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDatabase _redisCacheService;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyGenerator _cacheKeyGenerator;

    public GetPostDetailQueryHandler(
        IJobAdvertReadDbContext context,
        IMapper mapper,
        IConnectionMultiplexer redisCacheService,
        ICacheService cacheService,
        ICacheKeyGenerator cacheKeyGenerator)
    {
        _context = context;
        _mapper = mapper;
        _redisCacheService = redisCacheService.GetDatabase(1);
        _cacheService = cacheService;
        _cacheKeyGenerator = cacheKeyGenerator;
    }

    public async Task<GetPostDetailResponse> Handle(GetPostDetailQuery request, CancellationToken cancellationToken)
    {
        var viewCountCacheKey = _cacheKeyGenerator.Generate("PostViewCount", request.Id);
        await _redisCacheService.StringIncrementAsync(viewCountCacheKey);

        var dataCacheKey = _cacheKeyGenerator.Generate("Post", request.Id);
        var dataCacheData = await _cacheService.GetAsync<GetPostDetailResponse>(dataCacheKey);
        if (dataCacheData != null)
        {
            return dataCacheData;
        }

        var bsonDocument = await _context.Posts.Aggregate()
            .Match(o => o.Id == request.Id)
            .Lookup("JobAdvert.Departmants", "DepartmantId", "_id", "DepartmentDetail")
            .Lookup("JobAdvert.Cities", "CityId", "_id", "CityDetail")
            .Lookup("JobAdvert.Positions", "PositionId", "_id", "PositionDetail")
            .Lookup("JobAdvert.PostEducationLevels", "_id", "PostId", "EducationLevels")
            .Lookup("JobAdvert.PostLanguageRequirements", "_id", "PostId", "LanguageRequirements")
            .Lookup("JobAdvert.PostWorkBenefits", "_id", "PostId", "WorkBenefits")
            .Lookup("JobAdvert.PostSectors", "_id", "PostId", "PostSectorLinks")
            .FirstOrDefaultAsync();

        var postSectorLinks = await _context.PostSectors.Find(x => x.PostId == request.Id).ToListAsync();
        var sectorIds = postSectorLinks.Select(x => x.SectorId).ToList();
        var sectors = await _context.Sectors.Find(x => sectorIds.Contains(x.Id)).ToListAsync();

        var coverPhoto = bsonDocument.GetValue("CoverPhoto");
        var driverLicenseType = bsonDocument.GetValue("DriverLicenseType");
        var minSalary = bsonDocument.GetValue("MinSalary");
        var MaxSalary = bsonDocument.GetValue("MaxSalary");
        var minAge = bsonDocument.GetValue("MinAge");
        var maxAge = bsonDocument.GetValue("MaxAge");
        var minExperienceYear = bsonDocument.GetValue("MinExperienceYear");
        var maxExperienceYear = bsonDocument.GetValue("MaxExperienceYear");
        var departmant = bsonDocument.GetValue("DepartmentDetail").AsBsonArray.FirstOrDefault()!;
        var city = bsonDocument.GetValue("CityDetail").AsBsonArray.FirstOrDefault()!;
        var position = bsonDocument.GetValue("PositionDetail").AsBsonArray.FirstOrDefault()!;
        var educationLevels = bsonDocument.GetValue("EducationLevels").AsBsonArray;
        var languageRequirements = bsonDocument.GetValue("LanguageRequirements").AsBsonArray;
        var workBenefits = bsonDocument.GetValue("WorkBenefits").AsBsonArray;

        var response = new GetPostDetailResponse()
        {
            Id = bsonDocument.GetValue("_id").AsBsonValue.AsInt64,
            CompanyId = bsonDocument.GetValue("CompanyId").AsBsonValue.AsInt64,
            Title = bsonDocument.GetValue("Title").AsBsonValue.AsString,
            PackageId = bsonDocument.GetValue("PackageId").AsBsonValue.AsInt32,
            Slug = bsonDocument.GetValue("Slug").AsBsonValue.AsString,
            Description = bsonDocument.GetValue("Description").AsBsonValue.AsString,
            CoverPhoto = coverPhoto.IsBsonNull ? null : coverPhoto.AsString,
            WorkingType = (WorkingType)bsonDocument.GetValue("WorkingType").AsBsonValue.AsInt32,
            PositionLevelType = (PositionLevelType)bsonDocument.GetValue("PositionLevelType").AsBsonValue.AsInt32,
            DriverLicenseType = driverLicenseType.IsBsonNull ? null : (DriverLicenseType)driverLicenseType.AsInt32,
            GenderType = (GenderType)bsonDocument.GetValue("GenderType").AsBsonValue.AsInt32,
            ExperienceType = (ExperienceType)bsonDocument.GetValue("ExperienceType").AsBsonValue.AsInt32,
            MinSalary = minSalary.IsBsonNull ? null : minSalary.AsInt32,
            MaxSalary = MaxSalary.IsBsonNull ? null : MaxSalary.AsInt32,
            MinAge = minAge.IsBsonNull ? null : (byte)minAge.AsInt32,
            MaxAge = maxAge.IsBsonNull ? null : (byte)maxAge.AsInt32,
            MinExperienceYear = minExperienceYear.IsBsonNull ? null : (byte)minExperienceYear.AsInt32,
            MaxExperienceYear = maxExperienceYear.IsBsonNull ? null : (byte)maxExperienceYear.AsInt32,
            IsDisabledOnly = bsonDocument.GetValue("IsDisabledOnly").AsBsonValue.AsBoolean,
            ViewCount = bsonDocument.GetValue("ViewCount").AsBsonValue.AsInt64,
            Status = (PostStatus)bsonDocument.GetValue("Status").AsBsonValue.AsInt32,
            City = new GetPostCityDetailResponse()
            {
                Id = city.AsBsonDocument.GetValue("_id").AsInt32,
                Name = city.AsBsonDocument.GetValue("Name").AsString
            },
            Departmant = new GetPostDepartmantDetailResponse()
            {
                Id = departmant.AsBsonDocument.GetValue("_id").AsInt64,
                Name = departmant.AsBsonDocument.GetValue("Name").AsString
            },
            Position = new GetPostPositionDetailResponse()
            {
                Id = position.AsBsonDocument.GetValue("_id").AsInt64,
                Name = position.AsBsonDocument.GetValue("Name").AsString
            },
            EducationLevels = educationLevels.Select(o => new GetPostPostEducationLevelDetailResponse()
            {
                Id = o.AsBsonDocument.GetValue("_id").AsInt64,
                PostEducationLevelType = (PostEducationLevelType)o.AsBsonDocument.GetValue("EducationLevelType").AsInt32
            }).ToList(),
            LanguageRequirements = languageRequirements.Select(o => new GetPostPostLanguageRequirementDetailResponse()
            {
                Id = o.AsBsonDocument.GetValue("_id").AsInt64,
                LanguageType = (LanguageType)o.AsBsonDocument.GetValue("LanguageType").AsInt32,
                ReadingLevelType = (ReadingLevelType)o.AsBsonDocument.GetValue("ReadingLevelType").AsInt32,
                WritingLevelType = (WritingLevelType)o.AsBsonDocument.GetValue("WritingLevelType").AsInt32,
                SpeakingLevelType = (SpeakingLevelType)o.AsBsonDocument.GetValue("SpeakingLevelType").AsInt32
            }).ToList(),
            WorkBenefits = workBenefits.Select(o => new GetPostWorkBenefitDetailResponse()
            {
                Id = o.AsBsonDocument.GetValue("_id").AsInt64,
                Name = o.AsBsonDocument.GetValue("Name").AsString
            }).ToList(),
            Sectors = sectors.Select(o => new GetPostPostSectorDetailResponse()
            {
                Id = o.Id,
                Name = o.Name
            }).ToList(),
        };

        var viewCountCacheData = await _cacheService.GetAsync<long>(viewCountCacheKey);
        var filter = Builders<Post>.Filter.Eq(j => j.Id, request.Id);
        var update = Builders<Post>.Update.Inc(j => j.ViewCount, viewCountCacheData);

        await _context.Posts.UpdateOneAsync(filter, update);

        response.ViewCount = viewCountCacheData;

        await _cacheService.SetAsync(dataCacheKey, response, TimeSpan.FromMinutes(60));

        return response;
    }
}
