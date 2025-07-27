using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace CareerWay.SearchService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ApplicationsController : Controller
{
    private readonly IElasticClient _elasticsearchClient;

    public ApplicationsController(IElasticClientFactory elasticsearchClientFactory)
    {
        _elasticsearchClient = elasticsearchClientFactory.Create();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Index([FromRoute] long userId)
    {
        var applications = await _elasticsearchClient.SearchAsync<Application>(s => s
             .Index(ApplicationConsts.IndexName)
             .Query(q => q
                 .Term(t => t.UserId, userId)
             )
             .Size(1000)
         );

        var postIds = applications.Documents.Select(a => a.PostId).Distinct().ToList();

        if (postIds == null || postIds.Count == 0)
        {
            return Ok(new List<SearchPostResponse>());
        }


        var postsResponse = await _elasticsearchClient.SearchAsync<Post>(s => s
            .Index(PostConsts.IndexName)
            .Query(q => q
                .Terms(t => t
                    .Field(f => f.Id)
                    .Terms(postIds.Cast<object>())
                )
            )
            .Size(1000)
        );

        var companyIds = postsResponse.Documents
           .Select(o => o.CompanyId)
           .Distinct()
           .ToArray();

        var companies = (await _elasticsearchClient
          .SearchAsync<Company>(selector => selector
              .Index(CompanyConsts.IndexName)
              .Query(query => query
                  .Ids(i => i
                      .Values(companyIds))
              )
          )).Documents;

        var response = new List<SearchPostResponse>();
        foreach (var post in postsResponse.Documents)
        {
            var company = companies.FirstOrDefault(o => o.Id == post.CompanyId)!;
            var postResponse = new SearchPostResponse()
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                WorkingType = post.WorkingType,
                PositionLevelType = post.PositionLevelType,
                Company = new SearchPostCompanyResponse()
                {
                    Id = company.Id,
                    Title = company.Title,
                    ProfilePhoto = company.ProfilePhoto
                },
                CreationDate = post.CreationDate,
                ExpirationDate = post.ExpirationDate,
                PostStatus = post.Status
            };

            if (post.City != null)
            {
                postResponse.City = new SearchPostCityResponse()
                {
                    Id = post.City.Id,
                    Name = post.City.Name,
                };
            }

            response.Add(postResponse);
        }

        return Ok(response);
    }
}
