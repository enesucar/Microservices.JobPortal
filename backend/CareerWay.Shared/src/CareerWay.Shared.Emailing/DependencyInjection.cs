using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Emailing;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddFakeEmailSender(
        this IServiceCollection services,
        Action<EmailOptions> options)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(options, nameof(options));

        return services
            .Configure(options)
            .AddSingleton<IEmailSender, FakeEmailSender>();
    }
}
