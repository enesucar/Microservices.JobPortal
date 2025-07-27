using AutoMapper;
using CareerWay.JobSeekerService.Application.Models;
using CareerWay.JobSeekerService.Domain.Entities;

namespace CareerWay.JobSeekerService.Application.Mappings;

public class JobSeekerProfile : Profile
{
    public JobSeekerProfile()
    {
        CreateMap<JobSeeker, CreateJobSeekerRequest>().ReverseMap();
        CreateMap<JobSeeker, JobSeekerDetailResponse>().ReverseMap();
        CreateMap<City, JobSeekerCityDetailResponse>().ReverseMap();
        CreateMap<JobSeekerCertificate, JobSeekerJobSeekerCertificateItemDetailResponse>().ReverseMap();
        CreateMap<JobSeekerWorkExperience, JobSeekerJobSeekerWorkExperienceItemDetailResponse>().ReverseMap();
        CreateMap<City, JobSeekerJobSeekerWorkExperienceCityItemDetailResponse>().ReverseMap();
        CreateMap<Position, JobSeekerJobSeekerWorkExperiencePositionItemDetailResponse>().ReverseMap();
        CreateMap<JobSeekerSchool, JobSeekerJobSeekerSchoolItemDetailResponse>().ReverseMap();
        CreateMap<JobSeekerLanguage, JobSeekerJobSeekerLanguageItemDetailResponse>().ReverseMap();
        CreateMap<JobSeekerReference, JobSeekerJobSeekerReferenceItemDetailResponse>().ReverseMap();
        CreateMap<Position, JobSeekerJobSeekerReferencePositionItemDetailResponse>().ReverseMap();
        CreateMap<JobSeekerProject, JobSeekerJobSeekerProjectItemDetailResponse>().ReverseMap();
        CreateMap<JobSeeker, EditJobSeekerRequest>().ReverseMap();

        CreateMap<JobSeekerSkill, JobSeekerJobSeekerSkillItemDetailResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Skill.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Skill.Name))
            .ReverseMap();
    }
}
