using CareerWay.Web.Models.Post;

namespace CareerWay.Web.Interfaces;

public interface ISearchClient
{
    Task<List<SearchPostResponse>> Search(PostSearchRequest request);

    Task<List<SearchPostResponse>> RelatedPosts(long postId);

    Task<List<SearchPostResponse>> Applications(long userId);
}
