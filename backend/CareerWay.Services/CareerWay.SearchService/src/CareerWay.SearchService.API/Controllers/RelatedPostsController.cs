using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.Enums;
using CareerWay.SearchService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Nest;

namespace CareerWay.SearchService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RelatedPostsController : ControllerBase
{
    private readonly IElasticClient _elasticsearchClient;

    public RelatedPostsController(IElasticClientFactory elasticsearchClientFactory)
    {
        _elasticsearchClient = elasticsearchClientFactory.Create();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Search([FromRoute] long id)
    {
        var posts = await _elasticsearchClient.SearchAsync<Post>(s => s
            .Index(PostConsts.IndexName)
            .Size(4)
            .Query(q => q
                .Bool(b => b
                    .Should(
                        sh => sh.MoreLikeThis(mlt => mlt
                            .Fields(f => f.Field(p => p.Title))
                            .Like(l => l.Document(d => d.Id(id).Index(PostConsts.IndexName)))
                            .MinTermFrequency(1)
                            .MinDocumentFrequency(1)
                        )
                    )
                    .MustNot(mn => mn.Term(t => t.Field(o => o.Id).Value(id)))
                    .MinimumShouldMatch(1)
                )
            )
        );

        var companyIds = posts.Documents
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
        foreach (var post in posts.Documents)
        {
            var company = companies.FirstOrDefault(o => o.Id == post.CompanyId)!;
            var postResponse = new SearchPostResponse()
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                WorkingType = post.WorkingType,
                PositionLevelType = post.PositionLevelType,
                ExpirationDate = post.ExpirationDate,
                Company = new SearchPostCompanyResponse()
                {
                    Id = company.Id,
                    Title = company.Title,
                    ProfilePhoto = company.ProfilePhoto
                },
                CreationDate = post.CreationDate
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
