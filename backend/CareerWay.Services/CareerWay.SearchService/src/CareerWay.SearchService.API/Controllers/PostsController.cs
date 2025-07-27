using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Data;
using CareerWay.SearchService.API.Entities;
using CareerWay.SearchService.API.Enums;
using CareerWay.SearchService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CareerWay.SearchService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IElasticClient _elasticsearchClient;

    public PostsController(IElasticClientFactory elasticsearchClientFactory)
    {
        _elasticsearchClient = elasticsearchClientFactory.Create();
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] SearchPostRequest request)
    {
        var searchRequest = new SearchRequest<Post>(PostConsts.IndexName)
        {
            Size = 100
        };
        var mustQuery = new List<QueryContainer>();
        var filterQuery = new List<QueryContainer>();
        var shouldQuery = new List<QueryContainer>();

        if (!string.IsNullOrWhiteSpace(request.Text))
        {
            mustQuery.Add(new MultiMatchQuery
            {
                Query = request.Text,
                Fields = Infer.Fields<Post>(
                    p => p.Title,
                    p => p.Position.Name,
                    p => p.Departmant.Name,
                    p => p.Sectors.First().Name,
                    p => p.City!.Name),
                Type = TextQueryType.CrossFields,
                Operator = Operator.And
            });
        }

        if (request.CompanyId.HasValue)
        {
            filterQuery.Add(new TermQuery
            {
                Field = Infer.Field<Post>(p => p.CompanyId),
                Value = request.CompanyId.Value
            });
        }

        if (request.PostStatus != null && request.PostStatus == PostStatus.Approved)
        {
            filterQuery.Add(new TermQuery
            {
                Field = Infer.Field<Post>(p => p.Status),
                Value = PostStatus.Approved
            });
        }

        if (request.PostStatus != null && request.PostStatus == PostStatus.Approved)
        {
            //filterQuery.Add(new DateRangeQuery
            //{
            //    Field = Infer.Field<Post>(p => p.PublicationDate),
            //    GreaterThanOrEqualTo = DateTime.Now
            //});

            //filterQuery.Add(new DateRangeQuery
            //{
            //    Field = Infer.Field<Post>(p => p.ExpirationDate),
            //    LessThanOrEqualTo = DateMath.Now
            //});
        }

        if (request.Posts != null && request.Posts.Any())
        {
            filterQuery.Add(new TermsQuery
            {
                Field = Infer.Field<Post>(p => p.Id),
                Terms = request.Posts.Cast<object>()
            });
        }

        if (request.Cities != null && request.Cities.Any())
        {
            filterQuery.Add(new TermsQuery
            {
                Field = Infer.Field<Post>(p => p.City!.Id),
                Terms = request.Cities.Cast<object>()
            });
        }

        if (request.Sectors != null && request.Sectors.Any())
        {
            filterQuery.Add(new NestedQuery
            {
                Path = Infer.Field<Post>(p => p.Sectors),
                Query = new TermsQuery
                {
                    Field = Infer.Field<Post>(p => p.Sectors.First().Id),
                    Terms = request.Sectors.Cast<object>()
                }
            });
        }

        if (request.Positions != null && request.Positions.Any())
        {
            filterQuery.Add(new TermsQuery
            {
                Field = Infer.Field<Post>(p => p.Position.Id),
                Terms = request.Positions.Cast<object>()
            });
        }

        if (request.WorkingType != null && request.WorkingType.Any())
        {
            filterQuery.Add(new TermsQuery
            {
                Field = Infer.Field<Post>(p => p.WorkingType),
                Terms = request.WorkingType.Cast<object>()
            });
        }

        if (request.Departmants != null && request.Departmants.Any())
        {
            filterQuery.Add(new TermsQuery
            {
                Field = Infer.Field<Post>(p => p.Departmant.Id),
                Terms = request.Departmants.Cast<object>()
            });
        }

        if (request.EducationLevel != null && request.EducationLevel.Any())
        {
            filterQuery.Add(new TermsQuery
            {
                Field = Infer.Field<Post>(p => p.EducationLevelTypes),
                Terms = request.EducationLevel.Cast<object>()
            });
        }

        if (request.IsDisabledOnly.HasValue)
        {
            filterQuery.Add(new TermQuery
            {
                Field = Infer.Field<Post>(p => p.IsDisabledOnly),
                Value = request.IsDisabledOnly.Value
            });
        }

        searchRequest.Query = new BoolQuery()
        {
            Must = mustQuery,
            Filter = filterQuery,
            Should = shouldQuery,
        };

        if (request.UserId != null)
        {
            var applications = (await _elasticsearchClient
               .SearchAsync<Application>(selector => selector
                   .Index("careerway_search_applications")
                   .Query(query => query
                       .Term(t => t
                           .Field(f => f.UserId)
                           .Value(request.UserId.ToString()) // userId burada long olmalı
                       )
                   )
                   .Size(100) // Gerekirse limit koy
               )).Documents;

            var appliedPostIds = applications
                .Select(a => a.PostId.ToString())
                .Distinct()
                .ToList();

            if (appliedPostIds.Any())
            {
                mustQuery.Add(new BoolQuery
                {
                    MustNot = new List<QueryContainer>
                    {
                        new TermsQuery
                        {
                            Field = Infer.Field<Post>(p => p.Id),
                            Terms = appliedPostIds.Cast<object>()
                        }
                    }
                });

                var likeDocuments = appliedPostIds.Select(id => (Like)new LikeDocument<Post>(id)).ToList();

                shouldQuery.Add(new MoreLikeThisQuery
                {
                    Fields = Infer.Fields<Post>(p => p.Title),
                    Like = likeDocuments,
                    MinTermFrequency = 1,
                    MinDocumentFrequency = 1
                });
            }
        }

        var posts = await _elasticsearchClient.SearchAsync<Post>(searchRequest);

        var companyIds = posts.Documents
           .Select(o => o.CompanyId.ToString())
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
