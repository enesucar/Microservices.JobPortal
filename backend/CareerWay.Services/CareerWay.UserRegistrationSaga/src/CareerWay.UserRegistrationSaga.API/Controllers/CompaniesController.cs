using Asp.Versioning;
using CareerWay.Shared.AspNetCore.Models;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.TimeProviders;
using CareerWay.UserRegistrationSaga.API.IntegrationEvents.Events;
using CareerWay.UserRegistrationSaga.API.Interfaces;
using CareerWay.UserRegistrationSaga.API.Models;
using CareerWay.UserRegistrationSaga.Consts;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.UserRegistrationSaga.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyClient _companyClient;
    private readonly IIdentityClient _userClient;
    private readonly IEventBus _eventBus;
    private readonly ICorrelationId _correlationId;
    private readonly IDateTime _dateTime;

    public CompaniesController(
        ICompanyClient companyClient,
        IIdentityClient userClient,
        IEventBus eventBus,
        ICorrelationId correlationId,
        IDateTime dateTime)
    {
        _companyClient = companyClient;
        _userClient = userClient;
        _eventBus = eventBus;
        _correlationId = correlationId;
        _dateTime = dateTime;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
    {
        var user = new CreateUserHttpRequest()
        {
            Email = request.Email,
            Password = request.Password,
            Role = RoleConsts.Company
        };

        var createUserResponse = await _userClient.Register(user);
        if (!createUserResponse.Success)
        {
            var error = (ErrorApiJsonResponse)createUserResponse;
            return new ContentResult()
            {
                Content = error.JsonData,
                ContentType = "application/json",
                StatusCode = error.Status
            };
        }

        var createdUser = (SuccessResponse<CreateUserHttpResponse>)createUserResponse;
        var company = new CreateCompanyHttpRequest()
        {
            Id = createdUser.Data!.Id,
            Email = request.Email,
            Title = request.Title,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreationDate = createdUser.Data.CreationDate,
        };

        var createCompanyResponse = await _companyClient.Register(company);
        if (!createCompanyResponse.Success)
        {
            //Rollback user

            var error = (ErrorApiJsonResponse)createCompanyResponse;
            return new ContentResult()
            {
                Content = error.JsonData,
                ContentType = "application/json",
                StatusCode = error.Status
            };
        }
        
        var response = new SuccessResponse<CreateUserResponse>();
        response.Data = new CreateUserResponse()
        {
            Id = createdUser.Data.Id
        };

        var companyCreatedIntegrationEvent = new CompanyCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now)
        {
            Id = createdUser.Data.Id,
            Title = request.Title,
            ProfilePhoto = null
        };
        await _eventBus.PublishAsync(companyCreatedIntegrationEvent);

        return Created(string.Empty, response);
    }
}
