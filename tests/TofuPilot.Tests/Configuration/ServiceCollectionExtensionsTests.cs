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
}
