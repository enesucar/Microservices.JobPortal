namespace CareerWay.Web.Models.Post;

public class PostDetailViewModel
{
    public int ApplicationCount { get; set; }

    public bool IsApplied { get; set; }

    public PostDetail PostDetail { get; set; }

    public List<SearchPostResponse> RelatedJobs { get; set; } = [];
}
