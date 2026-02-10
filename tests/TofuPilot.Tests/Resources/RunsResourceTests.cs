using FluentAssertions;
using Moq;
using TofuPilot.Abstractions.Models;
using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Runs;
using TofuPilot.Resources;
using Xunit;

namespace TofuPilot.Tests.Resources;

public class RunsResourceTests
{
    private readonly Mock<ITofuPilotHttpClient> _mockHttpClient;
    private readonly RunsResource _runsResource;

    public RunsResourceTests()
    {
        _mockHttpClient = new Mock<ITofuPilotHttpClient>();
        _runsResource = new RunsResource(_mockHttpClient.Object);
    }

    [Fact]
    public async Task ListAsync_ReturnsRunsList()
    {
        // Arrange
        var expectedRuns = new PaginatedResponse<Run>
        {
            Data = new List<Run>
            {
                new()
                {
                    Id = "run-123",
                    Outcome = RunOutcome.PASS,
                    SerialNumber = "SN001"
                }
            },
            HasMore = false
        };

        _mockHttpClient
            .Setup(x => x.GetAsync<PaginatedResponse<Run>>(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedRuns);

        // Act
        var result = await _runsResource.ListAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data[0].Id.Should().Be("run-123");
    }

    [Fact]
    public async Task CreateAsync_CreatesRun()
    {
        // Arrange
        var request = new CreateRunRequest
        {
            Outcome = RunOutcome.PASS,
            ProcedureId = "proc-123",
            StartedAt = DateTimeOffset.UtcNow.AddMinutes(-5),
            EndedAt = DateTimeOffset.UtcNow,
            SerialNumber = "SN001"
        };

        var expectedRun = new Run
        {
            Id = "run-123",
            Outcome = RunOutcome.PASS,
            SerialNumber = "SN001"
        };

        _mockHttpClient
            .Setup(x => x.PostAsync<CreateRunRequest, Run>(
                It.IsAny<string>(),
                It.Is<CreateRunRequest>(r => r.SerialNumber == "SN001"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedRun);

        // Act
        var result = await _runsResource.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("run-123");
        result.Outcome.Should().Be(RunOutcome.PASS);
    }

    [Fact]
    public async Task GetAsync_ReturnsRun()
    {
        // Arrange
        var expectedRun = new Run
        {
            Id = "run-123",
            Outcome = RunOutcome.FAIL,
            SerialNumber = "SN001"
        };

        _mockHttpClient
            .Setup(x => x.GetAsync<Run>(
                "/v2/runs/run-123",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedRun);

        // Act
        var result = await _runsResource.GetAsync("run-123");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("run-123");
        result.Outcome.Should().Be(RunOutcome.FAIL);
    }

    [Fact]
    public async Task DeleteAsync_DeletesRuns()
    {
        // Arrange
        var expectedResponse = new DeleteResponse { Deleted = 2 };

        _mockHttpClient
            .Setup(x => x.DeleteAsync<DeleteResponse>(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _runsResource.DeleteAsync(new[] { "run-1", "run-2" });

        // Assert
        result.Should().NotBeNull();
        result.Deleted.Should().Be(2);
    }
}
