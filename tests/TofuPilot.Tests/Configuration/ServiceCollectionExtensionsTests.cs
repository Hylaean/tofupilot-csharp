using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TofuPilot.Configuration;
using Xunit;

namespace TofuPilot.Tests.Configuration;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddTofuPilot_ResolvesTofuPilotClient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddTofuPilot(options =>
        {
            options.ApiKey = "test-key";
        });

        using var provider = services.BuildServiceProvider();

        // Act — this should not throw due to ambiguous constructor resolution
        using var scope = provider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<TofuPilotClient>();

        // Assert
        client.Should().NotBeNull();
        client.Runs.Should().NotBeNull();
        client.Units.Should().NotBeNull();
        client.Procedures.Should().NotBeNull();
        client.Parts.Should().NotBeNull();
        client.Batches.Should().NotBeNull();
        client.Stations.Should().NotBeNull();
        client.Attachments.Should().NotBeNull();
    }

    [Fact]
    public void AddTofuPilot_CanBeInjectedIntoTransientService()
    {
        var services = new ServiceCollection();
        services.AddTofuPilot(options => { options.ApiKey = "test-key"; });
        services.AddTransient<TransientConsumer>();

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var consumer = scope.ServiceProvider.GetRequiredService<TransientConsumer>();

        consumer.Client.Should().NotBeNull();
    }

    [Fact]
    public void AddTofuPilot_CanBeInjectedIntoScopedService()
    {
        var services = new ServiceCollection();
        services.AddTofuPilot(options => { options.ApiKey = "test-key"; });
        services.AddScoped<ScopedConsumer>();

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var consumer = scope.ServiceProvider.GetRequiredService<ScopedConsumer>();

        consumer.Client.Should().NotBeNull();
    }

    [Fact]
    public void AddTofuPilot_CanBeInjectedIntoSingletonService()
    {
        var services = new ServiceCollection();
        services.AddTofuPilot(options => { options.ApiKey = "test-key"; });
        services.AddSingleton<SingletonConsumer>();

        using var provider = services.BuildServiceProvider();

        var consumer = provider.GetRequiredService<SingletonConsumer>();

        consumer.Client.Should().NotBeNull();
    }

    private class TransientConsumer(TofuPilotClient client)
    {
        public TofuPilotClient Client => client;
    }

    private class ScopedConsumer(TofuPilotClient client)
    {
        public TofuPilotClient Client => client;
    }

    private class SingletonConsumer(TofuPilotClient client)
    {
        public TofuPilotClient Client => client;
    }
}
