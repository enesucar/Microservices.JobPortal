using Nest;

namespace CareerWay.SearchService.API.Data;

public interface IElasticClientFactory
{
    ElasticClient Create();
}
