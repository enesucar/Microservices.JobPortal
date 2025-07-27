using CareerWay.CompanyService.Application.Interfaces;
using CareerWay.CompanyService.Application.Models;
using CareerWay.CompanyService.Domain.Constants;
using CareerWay.CompanyService.Domain.Entities;

namespace CareerWay.CompanyService.Application.Mappers;

public class CompanyMapper : ICompanyMapper
{
    public Company Map(CreateCompanyRequest request, Company company)
    {
        company.Id = request.Id;
        company.FirstName = request.FirstName;
        company.LastName = request.LastName;
        company.Title = request.Title;
        company.Email = request.Email;
        company.CreationDate = request.CreationDate;
        return company;
    }

    public CompanyDetailResponse Map(Company company, CompanyDetailResponse response)
    {
        response.Id = company.Id;
        response.Title = company.Title;
        response.City = company.City != null ? company.City.Name : null;
        response.ProfilePhoto = company.ProfilePhoto ?? CompanyConsts.DefaultCompanyProfilePhoto;
        response.WebSite = company.WebSite;
        response.Instragram = company.Instragram;
        response.Facebook = company.Facebook;
        response.Twitter = company.Twitter;
        response.Linkedin = company.Linkedin;
        response.EstablishmentYear = company.EstablishmentYear;
        response.Description = company.Description;
        response.Address = company.Address;
        return response;
    }

    public Company Map(EditCompanyRequest request, Company company)
    {
        company.Id = request.Id;
        company.Title = request.Title;
        company.CityId = request.CityId;
        company.WebSite = request.WebSite;
        company.Instragram = request.Instragram;
        company.Facebook = request.Facebook;
        company.Twitter = request.Twitter;
        company.Linkedin = request.Linkedin;
        company.EstablishmentYear = request.EstablishmentYear;
        company.Description = request.Description;
        company.Address = request.Address;
        return company;
    }

    public EditCompanyResponse Map(Company company, EditCompanyResponse response)
    {
        response.Id = company.Id;
        response.Title = company.Title;
        response.City = company.City.Name;
        response.WebSite = company.WebSite;
        response.Instragram = company.Instragram;
        response.Facebook = company.Facebook;
        response.Twitter = company.Twitter;
        response.Linkedin = company.Linkedin;
        response.EstablishmentYear = company.EstablishmentYear;
        response.Description = company.Description;
        response.Address = company.Address;
        return response;
    }
}
