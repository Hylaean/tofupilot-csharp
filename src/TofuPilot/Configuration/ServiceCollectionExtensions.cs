using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TofuPilot.Abstractions.Configuration;
using TofuPilot.Http;

namespace TofuPilot.Configuration;

/// <summary>
/// Extension methods for configuring TofuPilot services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds TofuPilot services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">An action to configure the TofuPilot options.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddTofuPilot(
        this IServiceCollection services,
        Action<TofuPilotOptions>? configureOptions = null)
    {
        // Configure options
        if (configureOptions != null)
        {
            services.Configure(configureOptions);
        }
        else
        {
            services.Configure<TofuPilotOptions>(_ => { });
        }

        // Register HTTP handlers
        services.AddTransient<AuthenticationHandler>();
        services.AddTransient<RetryHandler>();

        // Register HTTP client
        services.AddHttpClient<ITofuPilotHttpClient, TofuPilotHttpClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<TofuPilotOptions>>().Value;
            var baseUrl = !string.IsNullOrEmpty(options.BaseUrl)
                ? options.BaseUrl
                : Environment.GetEnvironmentVariable("TOFUPILOT_URL") ?? "https://www.tofupilot.com";

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        })
        .AddHttpMessageHandler<AuthenticationHandler>()
        .AddHttpMessageHandler<RetryHandler>();

        // Register client with explicit constructor to avoid ambiguity
        services.AddScoped<TofuPilotClient>(sp =>
            new TofuPilotClient(sp.GetRequiredService<ITofuPilotHttpClient>()));

        return services;
    }

    /// <summary>
    /// Adds TofuPilot services to the service collection with configuration binding.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration section to bind.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddTofuPilot(
        this IServiceCollection services,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        services.Configure<TofuPilotOptions>(configuration.GetSection(TofuPilotOptions.SectionName));
        return services.AddTofuPilot();
    }
}
