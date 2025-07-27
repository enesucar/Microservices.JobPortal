using CareerWay.JobSeekerService.Application.Interfaces;
using CareerWay.JobSeekerService.Infrastructure.Consts;
using CareerWay.JobSeekerService.Infrastructure.Data.Contexts;
using CareerWay.JobSeekerService.Infrastructure.Services;
using CareerWay.Shared.Guids;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JobSeekerDbContext>(options =>
        {
            options.UseNpgsql(configuration[ConfigKeys.PostgreSQL.ConnectionString]!);
        });

        services.AddScoped<IJobSeekerDbContext, JobSeekerDbContext>();

        services.AddScoped<IJobSeekerGrpcClient, JobSeekerGrpcClient>();

        services.AddRedisCache(cacheOptions =>
        {
            cacheOptions.KeyPrefix = configuration[ConfigKeys.Redis.KeyPrefix]!;
        }, redisCacheOptions =>
        {
            redisCacheOptions.ConnectionString = configuration[ConfigKeys.Redis.ConnectionString]!;
            redisCacheOptions.DefaultDatabase = int.Parse(configuration[ConfigKeys.Redis.DefaultDatabase]!);
        });

        services.AddNewtonsoftJsonSerializer(o => { });

        services.AddMachineTimeProvider();

        services.AddCustomCorrelationId();

        services.AddSequentialGuidGenerator(o => o.SequentialGuidType = SequentialGuidType.SequentialAsString);

        services.AddScoped<IUser, User>();

        services.AddSecurity(options =>
        {
            options.Issuer = configuration["AccessTokenOptions:Issuer"]!;
            options.Audience = configuration["AccessTokenOptions:Audience"]!;
            options.Expiration = Convert.ToInt32(configuration["AccessTokenOptions:Expiration"]);
            options.SecurityKey = configuration["AccessTokenSecurityKey"]!;
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

        return services;
    }
}
