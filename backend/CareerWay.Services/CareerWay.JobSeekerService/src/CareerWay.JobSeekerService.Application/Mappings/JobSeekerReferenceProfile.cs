using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerReferenceProfile : Profile
{
    public JobSeekerReferenceProfile()
    {
        CreateMap<JobSeekerReference, CreateJobSeekerReferenceRequest>().ReverseMap();
    }
}