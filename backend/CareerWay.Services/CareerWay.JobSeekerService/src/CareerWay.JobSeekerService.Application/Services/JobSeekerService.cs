using AutoMapper;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerWay.JobSeekerService.Application.Services;

public class JobSeekerService : IJobSeekerService
{
    private readonly IJobSeekerDbContext _jobSeekerDbContext;
    private readonly IMapper _mapper;

    public JobSeekerService(
        IJobSeekerDbContext jobSeekerDbContext,
        IMapper mapper)
    {
        _jobSeekerDbContext = jobSeekerDbContext;
        _mapper = mapper;
    }

    public async Task<JobSeekersResponse> GetList(JobSeekersRequest request)
    {
        IQueryable<JobSeeker> query = _jobSeekerDbContext.JobSeekers
            .Include(o => o.JobSeekerCertificates)
            .Include(o => o.WorkExperiences)
                .ThenInclude(o => o.City)
            .Include(o => o.WorkExperiences)
                .ThenInclude(o => o.Position)
            .Include(o => o.JobSeekerSchools)
            .Include(o => o.JobSeekerReferences)
                .ThenInclude(o => o.Position)
            .Include(o => o.JobSeekerLanguages)
            .Include(o => o.JobSeekerProjects)
            .Include(o => o.JobSeekerSkills)
                .ThenInclude(o => o.Skill);

        if (request.Ids.Count > 0)
        {
            query = query.Where(o => request.Ids.Contains(o.Id));
        }

        var jobSeekers = await query.ToListAsync();
        var data = _mapper.Map<List<JobSeekerDetailResponse>>(jobSeekers);
        return new JobSeekersResponse()
        {
            Items = data
        };
    }
    public async Task<JobSeekerDetailResponse> GetDetail(long id)
    {
        var jobSeeker = await _jobSeekerDbContext.JobSeekers
            .Include(o => o.JobSeekerCertificates)
            .Include(o => o.WorkExperiences)
                .ThenInclude(o => o.City)
            .Include(o => o.WorkExperiences)
                .ThenInclude(o => o.Position)
            .Include(o => o.JobSeekerSchools)
            .Include(o => o.JobSeekerReferences)
                .ThenInclude(o => o.Position)
            .Include(o => o.JobSeekerLanguages)
            .Include(o => o.JobSeekerProjects)
            .Include(o => o.JobSeekerSkills)
                .ThenInclude(o => o.Skill)
            .Where(o => o.Id == id && o.IsDeleted == 0)
            .FirstOrDefaultAsync();

        return _mapper.Map<JobSeekerDetailResponse>(jobSeeker);
    }

    public async Task Create(CreateJobSeekerRequest request)
    {
        var jobSeeker = _mapper.Map(request, new JobSeeker());
        await _jobSeekerDbContext.JobSeekers.AddAsync(jobSeeker);
        await _jobSeekerDbContext.SaveChangesAsync();
    }

    public async Task Edit(EditJobSeekerRequest request)
    {
        var jobSeeker = await _jobSeekerDbContext.JobSeekers.FirstOrDefaultAsync(o => o.Id == request.Id);
        _mapper.Map(request, jobSeeker);
        await _jobSeekerDbContext.SaveChangesAsync();
    }
}
