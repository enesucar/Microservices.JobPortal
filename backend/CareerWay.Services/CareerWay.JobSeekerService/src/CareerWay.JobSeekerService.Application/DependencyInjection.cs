using CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;
using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Application.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IJobSeekerService, JobSeekerService>();
        services.AddScoped<IJobSeekerCertificateService, JobSeekerCertificateService>();
        services.AddScoped<IJobSeekerLanguageService, JobSeekerLanguageService>();
        services.AddScoped<IJobSeekerProjectService, JobSeekerProjectService>();
        services.AddScoped<IJobSeekerReferenceService, JobSeekerReferenceService>();
        services.AddScoped<IJobSeekerReferenceService, JobSeekerReferenceService>();
        services.AddScoped<IJobSeekerSchoolService, JobSeekerSchoolService>();
        services.AddScoped<IJobSeekerSkillService, JobSeekerSkillService>();
        services.AddScoped<IJobSeekerWorkExperienceService, JobSeekerWorkExperienceService>();

        services.AddTransient<JobSeekerCreatedIntegrationEventHandler>();
        services.AddTransient<PositionCreatedIntegrationEventHandler>();
        services.AddTransient<PositionEditedIntegrationEventHandler>();
        services.AddTransient<SkillCreatedIntegrationEventHandler>();
        services.AddTransient<SkillEditedIntegrationEventHandler>();

        return services;
    }
}