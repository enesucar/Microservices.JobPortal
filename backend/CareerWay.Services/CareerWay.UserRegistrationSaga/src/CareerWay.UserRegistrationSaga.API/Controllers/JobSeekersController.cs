using Asp.Versioning;
using CareerWay.UserRegistrationSaga.API.Interfaces;
using CareerWay.UserRegistrationSaga.API.Models;
using CareerWay.UserRegistrationSaga.Consts;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.UserRegistrationSaga.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekersController : ControllerBase
{
    private readonly IJobSeekerClient _jobSeekerClient;
    private readonly IIdentityClient _userClient;

    public JobSeekersController(
        IJobSeekerClient jobSeekerClient,
        IIdentityClient userClient)
    {
        _jobSeekerClient = jobSeekerClient;
        _userClient = userClient;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobSeekerRequest request)
    {
        var user = new CreateUserHttpRequest()
        {
            Email = request.Email,
            Password = request.Password,
            Role = RoleConsts.JobSeeker
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
        var jobSeeker = new CreateJobSeekerHttpRequest()
        {
            Id = createdUser.Data!.Id,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreationDate = createdUser.Data.CreationDate,
        };

        var createJobSeekerResponse = await _jobSeekerClient.Register(jobSeeker);
        if (!createJobSeekerResponse.Success)
        {
            //Rollback user

            var error = (ErrorApiJsonResponse)createJobSeekerResponse;
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

        return Created(string.Empty, response);
    }
}
