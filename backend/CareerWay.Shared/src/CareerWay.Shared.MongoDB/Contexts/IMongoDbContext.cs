using CareerWay.Shared.MongoDB.Models;
using MongoDB.Driver;

namespace CareerWay.Shared.MongoDB.Contexts;

public interface IMongoDbContext
{
    public MongoClient MongoClient { get; }

    public IMongoDatabase MongoDatabase { get; }

    public MongoDbOptions MongoDbOptions { get; }

    IMongoCollection<T> Set<T>(string collectionName = "");
}
