using CareerWay.IdentityService.Application.Interfaces;
using CareerWay.IdentityService.Domain.Entities;
using CareerWay.IdentityService.Infrastructure.Consts;
using CareerWay.IdentityService.Infrastructure.Data.Contexts;
using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Localization.Json;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configuration, nameof(configuration));

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseAzureSql(configuration[ConfigKeys.AzureSQL.ConnectionString]);
        });

        services.AddScoped<IdentityDbContextInitializer>();

        services.AddSequentialGuidGenerator(options =>
        {
            options.SequentialGuidType = SequentialGuidType.SequentialAtEnd;
        });

        services.AddMachineTimeProvider();

        services.AddNewtonsoftJsonSerializer(options => { });

        services.AddCustomCorrelationId();

        services.AddSnowflakeIdGenerator(o => o.GeneratorId = 1);

        services.AddDefaultIdentity<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        })
            .AddDefaultTokenProviders()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        services.AddSecurity(options =>
        {
            options.Issuer = configuration[ConfigKeys.Security.Issuer]!;
            options.Audience = configuration[ConfigKeys.Security.Audience]!;
            options.Expiration = int.Parse(configuration[ConfigKeys.Security.Expiration]!);
            options.SecurityKey = configuration[ConfigKeys.Security.SecurityKey]!;
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
