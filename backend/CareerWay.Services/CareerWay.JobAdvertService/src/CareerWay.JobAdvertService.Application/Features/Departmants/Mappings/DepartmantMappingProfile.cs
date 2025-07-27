using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Edit;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Departmants.Queries.GetList;
using CareerWay.JobAdvertService.Domain.Entities;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Mappings;

public class DepartmantMappingProfile : Profile
{
    public DepartmantMappingProfile()
    {
        CreateMap<Departmant, GetDepartmantListItemResponse>().ReverseMap();
        CreateMap<Departmant, CreateDepartmantCommand>().ReverseMap();
        CreateMap<Departmant, CreateDepartmantResponse>().ReverseMap();
        CreateMap<Departmant, EditDepartmantCommand>().ReverseMap();
        CreateMap<Departmant, EditDepartmantResponse>().ReverseMap();
        CreateMap<Departmant, DepartmantCreatedIntegrationEvent>().ReverseMap();
        CreateMap<Departmant, DepartmantEditedIntegrationEvent>().ReverseMap();
    }
}
