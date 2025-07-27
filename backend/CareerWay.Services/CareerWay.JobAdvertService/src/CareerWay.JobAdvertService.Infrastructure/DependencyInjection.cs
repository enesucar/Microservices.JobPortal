using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Infrastructure.Consts;
using CareerWay.JobAdvertService.Infrastructure.Data.EntityFrameworkCore.Contexts;
using CareerWay.JobAdvertService.Infrastructure.Data.MongoDB.Contexts;
using CareerWay.JobAdvertService.Infrastructure.Services;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JobAdvertWriteDbContext>(options =>
        {
            options.UseAzureSql(configuration[ConfigKeys.AzureSQL.ConnectionString]);
        });

        services.AddMongoDB<JobAdvertReadDbContext>(options =>
        {
            options.ConnectionString = configuration[ConfigKeys.MongoDB.ConnectionString]!;
            options.Schema = configuration[ConfigKeys.MongoDB.Schema]!;
            options.Database = configuration[ConfigKeys.MongoDB.Database]!;
        });

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.AddScoped<IJobAdvertWriteDbContext, JobAdvertWriteDbContext>();

        services.AddScoped<IJobAdvertReadDbContext, JobAdvertReadDbContext>();

        var redis = configuration[ConfigKeys.Redis.ConnectionString];

        services.AddRedisCache(cacheOptions =>
        {
            cacheOptions.KeyPrefix = configuration[ConfigKeys.Redis.KeyPrefix]!;
        }, redisCacheOptions =>
        {
            redisCacheOptions.ConnectionString = "localhost";
            redisCacheOptions.DefaultDatabase = int.Parse(configuration[ConfigKeys.Redis.DefaultDatabase]!);
        });

        services.AddNewtonsoftJsonSerializer(o => { });

        services.AddMachineTimeProvider();

        services.AddCustomCorrelationId();

        services.AddSecurity(options =>
        {
            options.Issuer = configuration[ConfigKeys.Security.Issuer]!;
            options.Audience = configuration[ConfigKeys.Security.Audience]!;
            options.Expiration = int.Parse(configuration[ConfigKeys.Security.Expiration]!);
            options.SecurityKey = configuration[ConfigKeys.Security.SecurityKey]!;
        });
        
        services.AddSnowflakeIdGenerator(o =>
        {
            o.GeneratorId = int.Parse(configuration[ConfigKeys.SnowflakeId.GeneratorId]!);
        });

        services.AddKafka(eventBusOptions =>
        {
            eventBusOptions.ServiceName = configuration[ConfigKeys.Kafka.ServiceName]!;
            eventBusOptions.ProjectName = configuration[ConfigKeys.Kafka.ProjectName]!;
        }, kafkaOptions =>
        {
            kafkaOptions.ProducerConfig.BootstrapServers = configuration[ConfigKeys.Kafka.ProducerBootstrapServers];
            kafkaOptions.ProducerConfig.SaslUsername = configuration[ConfigKeys.Kafka.ProducerSaslUsername];
            kafkaOptions.ProducerConfig.SaslPassword = configuration[ConfigKeys.Kafka.ProducerSaslPassword];
            kafkaOptions.ProducerConfig.SaslMechanism = SaslMechanism.Plain;
            kafkaOptions.ProducerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            kafkaOptions.ProducerConfig.ClientId = ConfigKeys.Kafka.ClientId;
            kafkaOptions.ProducerConfig.AllowAutoCreateTopics = true;
            kafkaOptions.ProducerConfig.EnableMetricsPush = false;
             
            kafkaOptions.ConsumerConfig.BootstrapServers = configuration[ConfigKeys.Kafka.ConsumerBootstrapServers];
            kafkaOptions.ConsumerConfig.SaslUsername = configuration[ConfigKeys.Kafka.ConsumerSaslUsername];
            kafkaOptions.ConsumerConfig.SaslPassword = configuration[ConfigKeys.Kafka.ConsumerSaslPassword];
            kafkaOptions.ConsumerConfig.SaslMechanism = SaslMechanism.Plain;
            kafkaOptions.ConsumerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            kafkaOptions.ConsumerConfig.GroupId = configuration[ConfigKeys.Kafka.GroupId];
            kafkaOptions.ConsumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            kafkaOptions.ConsumerConfig.ClientId = configuration[ConfigKeys.Kafka.ClientId];
            kafkaOptions.ConsumerConfig.AllowAutoCreateTopics = true;
            kafkaOptions.ConsumerConfig.PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin;
            kafkaOptions.ConsumerConfig.EnableMetricsPush = false;

            kafkaOptions.AdminClientConfig.BootstrapServers = configuration[ConfigKeys.Kafka.AdminClientBootstrapServers];
            kafkaOptions.AdminClientConfig.SaslUsername = configuration[ConfigKeys.Kafka.AdminClientSaslUsername];
            kafkaOptions.AdminClientConfig.SaslPassword = configuration[ConfigKeys.Kafka.AdminClientPassword];
            kafkaOptions.AdminClientConfig.SaslMechanism = SaslMechanism.Plain;
            kafkaOptions.AdminClientConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
            kafkaOptions.AdminClientConfig.ClientId = configuration[ConfigKeys.Kafka.ClientId];
            kafkaOptions.AdminClientConfig.AllowAutoCreateTopics = true;
            kafkaOptions.AdminClientConfig.EnableMetricsPush = false;
        });

        services.AddScoped<IUser, User>();

        return services;
    }
}