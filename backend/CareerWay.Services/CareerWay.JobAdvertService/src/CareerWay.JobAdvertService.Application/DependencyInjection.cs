using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Payments.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.IntegrationEvents;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Application.Services;
using CareerWay.Shared.MediatR.Behaviours.Validation;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient<CityCreatedIntegrationEventHandler>();
        services.AddTransient<CityEditedIntegrationEventHandler>();
        services.AddTransient<DepartmantCreatedIntegrationEventHandler>();
        services.AddTransient<DepartmantEditedIntegrationEventHandler>();
        services.AddTransient<PositionCreatedIntegrationEventHandler>();
        services.AddTransient<PositionEditedIntegrationEventHandler>();
        services.AddTransient<SectorCreatedIntegrationEventHandler>();
        services.AddTransient<SectorEditedIntegrationEventHandler>();
        services.AddTransient<SkillCreatedIntegrationEventHandler>();
        services.AddTransient<SkillEditedIntegrationEventHandler>();
        services.AddTransient<PostCreatedIntegrationEventHandler>();
        services.AddTransient<PostEducationLevelCreatedIntegrationEventHandler>();
        services.AddTransient<PostLanguageRequirementCreatedIntegrationEventHandler>();
        services.AddTransient<PostWorkBenefitCreatedIntegrationEventHandler>();
        services.AddTransient<PostSectorCreatedIntegrationEventHandler>();
        services.AddTransient<PostPublishedIntegrationEventHandler>();

        services.AddSingleton<ISlugGenerator, SlugGenerator>();

        return services;
    }
}
