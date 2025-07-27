using CareerWay.Shared.MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CareerWay.Shared.MongoDB.Contexts;

public class MongoDbContext : IMongoDbContext
{
    public MongoClient MongoClient { get; }

    public IMongoDatabase MongoDatabase { get; }

    public MongoDbOptions MongoDbOptions { get; }

    public MongoDbContext(IOptions<MongoDbOptions> mongoDbOptions)
    {
        MongoDbOptions = mongoDbOptions.Value;
        MongoClient = new MongoClient(MongoDbOptions.ConnectionString);
        MongoDatabase = MongoClient.GetDatabase(MongoDbOptions.Database);
    }

    public IMongoCollection<T> Set<T>(string? collectionName = null)
    {
        return MongoDatabase.GetCollection<T>(GetCollectionName<T>(collectionName ?? string.Empty));
    }

    private string GetCollectionName<T>(string collectionName)
    {
        var collection = collectionName.IsNullOrWhiteSpace()
            ? nameof(T).Pluralize()
            : collectionName;
        var schema = MongoDbOptions.Schema;
        return schema.IsNullOrWhiteSpace() ? collection : $"{schema}.{collection}";
    }
}
