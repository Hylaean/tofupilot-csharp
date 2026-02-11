using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TofuPilot.Abstractions.Models;
using TofuPilot.Models.Runs;
using Xunit;

namespace TofuPilot.IntegrationTests;

/// <summary>
/// Integration tests for the TofuPilot client.
/// These tests require environment variables to be set:
/// - TOFUPILOT_URL: The TofuPilot server URL
/// - TOFUPILOT_API_KEY: A valid API key
/// - TOFUPILOT_PROCEDURE_ID: A valid procedure ID for testing
/// </summary>
[Trait("Category", "Integration")]
public class TofuPilotClientIntegrationTests : IDisposable
{
    private readonly TofuPilotClient? _client;
    private readonly string? _procedureId;
    private readonly bool _canRunTests;

    public TofuPilotClientIntegrationTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<TofuPilotClientIntegrationTests>()
            .AddEnvironmentVariables()
            .Build();

        var apiKey = configuration["TOFUPILOT_API_KEY"];
        var baseUrl = configuration["TOFUPILOT_URL"];
        _procedureId = configuration["TOFUPILOT_PROCEDURE_ID"];

        _canRunTests = !string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(_procedureId);

        if (_canRunTests)
        {
            _client = new TofuPilotClient(apiKey, baseUrl);
        }
    }

    [Fact(Skip = "Requires environment configuration")]
    public async Task ListRuns_ReturnsRuns()
    {
        if (!_canRunTests)
        {
            return;
        }

        // Act
        var result = await _client!.Runs.ListAsync(new ListRunsRequest { Limit = 5 });

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
    }

    [Fact(Skip = "Requires environment configuration")]
    public async Task CreateAndGetRun_WorksCorrectly()
    {
        if (!_canRunTests)
        {
            return;
        }

        // Arrange
        var serialNumber = $"TEST-{Guid.NewGuid():N}";
        var createRequest = new CreateRunRequest
        {
            Outcome = RunOutcome.PASS,
            ProcedureId = _procedureId!,
            StartedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
            EndedAt = DateTimeOffset.UtcNow,
            SerialNumber = serialNumber,
            Phases = new List<CreateRunPhase>
            {
                new()
                {
                    Name = "Test Phase",
                    Outcome = PhaseOutcome.PASS,
                    StartedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
                    EndedAt = DateTimeOffset.UtcNow,
                    Measurements = new List<CreateRunMeasurement>
                    {
                        new()
                        {
                            Name = "Test Measurement",
                            Outcome = MeasurementOutcome.PASS,
                            MeasuredValue = 42.5,
                            Units = "V",
                            LowerLimit = 40.0,
                            UpperLimit = 45.0
                        }
                    }
                }
            }
        };

        // Act - Create
        var createdRun = await _client!.Runs.CreateAsync(createRequest);

        // Assert - Create
        createdRun.Should().NotBeNull();
        createdRun.Id.Should().NotBeNullOrEmpty();

        // Act - Get
        var retrievedRun = await _client.Runs.GetAsync(createdRun.Id);

        // Assert - Get
        retrievedRun.Should().NotBeNull();
        retrievedRun.Id.Should().Be(createdRun.Id);
        retrievedRun.SerialNumber.Should().Be(serialNumber);

        // Cleanup
        await _client.Runs.DeleteAsync(new[] { createdRun.Id });
    }

    public void Dispose()
    {
        _client?.Dispose();
    }
}
