using CareerWay.SearchService.API.Consts;
using CareerWay.SearchService.API.Entities;
using Elasticsearch.Net;
using Nest;
using System.Diagnostics;

namespace CareerWay.SearchService.API.Data;

public class ElasticClientFactory : IElasticClientFactory
{
    private readonly ConnectionSettings _connectionSettings;

    public ElasticClientFactory()
    {
        _connectionSettings = new ConnectionSettings()
            .DefaultIndex(PostConsts.IndexName)
            .PrettyJson()  // JSON'u okunabilir yapar
            .DisableDirectStreaming() // İstek/yanıt akışını kapatma, log almayı sağlar
            
            .OnRequestCompleted(apiCallDetails =>
            {
                // İstek JSON'u
                if (apiCallDetails.RequestBodyInBytes != null)
                {
                    string requestJson = System.Text.Encoding.UTF8.GetString(apiCallDetails.RequestBodyInBytes);
                    Debug.WriteLine("Request JSON:\n" + requestJson);
                }

                // Yanıt JSON'u
                if (apiCallDetails.ResponseBodyInBytes != null)
                {
                    string responseJson = System.Text.Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes);
                    Debug.WriteLine("Response JSON:\n" + responseJson);
                }
                string responseeJson = System.Text.Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes);
                Debug.WriteLine("Response JSON:\n" + responseeJson);

            })
            .MemoryStreamFactory(new MemoryStreamFactory())
            .EnableDebugMode();
        _connectionSettings.EnableDebugMode();
    }

    public ElasticClient Create()
    {
        return new ElasticClient(_connectionSettings);
    }
}
