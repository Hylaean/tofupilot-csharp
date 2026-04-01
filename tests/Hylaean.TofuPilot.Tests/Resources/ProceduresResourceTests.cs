using FluentAssertions;
using Moq;
using Hylaean.TofuPilot.Http;
using Hylaean.TofuPilot.Models.Common;
using Hylaean.TofuPilot.Models.Procedures;
using Hylaean.TofuPilot.Resources;
using Xunit;

namespace Hylaean.TofuPilot.Tests.Resources;

public sealed class ProceduresResourceTests
{
    private readonly Mock<ITofuPilotHttpClient> _mockHttpClient;
    private readonly ProceduresResource _resource;

    public ProceduresResourceTests()
    {
        _mockHttpClient = new Mock<ITofuPilotHttpClient>();
        _resource = new ProceduresResource(_mockHttpClient.Object);
    }

    [Fact]
    public async Task ListAsync_WithDefaultParams_ReturnsPaginatedResponse()
    {
        // Arrange
        var expected = new PaginatedResponse<Procedure>
        {
            Data = new List<Procedure>
            {
                new() { Id = "proc-1", Name = "TCC Validation SCS" }
            },
            Meta = new PaginationMeta { HasMore = false }
        };

        _mockHttpClient
            .Setup(x => x.GetAsync<PaginatedResponse<Procedure>>(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.ListAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data[0].Id.Should().Be("proc-1");
    }

    [Fact]
    public async Task ListAsync_WithSearchQuery_PassesSearchParameter()
    {
        // Arrange
        var expected = new PaginatedResponse<Procedure>
        {
            Data = new List<Procedure>(),
            Meta = new PaginationMeta { HasMore = false }
        };

        _mockHttpClient
            .Setup(x => x.GetAsync<PaginatedResponse<Procedure>>(
                It.Is<string>(s => s.Contains("search_query")),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.ListAsync(new ListProceduresRequest
        {
            SearchQuery = "validation"
        });

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ListAsync_WithIds_PassesIdsParameter()
    {
        // Arrange
        var expected = new PaginatedResponse<Procedure>
        {
            Data = new List<Procedure>
            {
                new() { Id = "proc-1", Name = "Procedure A" }
            },
            Meta = new PaginationMeta { HasMore = false }
        };

        _mockHttpClient
            .Setup(x => x.GetAsync<PaginatedResponse<Procedure>>(
                It.Is<string>(s => s.Contains("ids")),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.ListAsync(new ListProceduresRequest
        {
            Ids = new[] { "proc-1", "proc-2" }
        });

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateAsync_SendsCorrectRequest()
    {
        // Arrange
        var expected = new Procedure { Id = "proc-123", Name = "TCC Validation SCS" };

        _mockHttpClient
            .Setup(x => x.PostAsync<CreateProcedureRequest, Procedure>(
                It.IsAny<string>(),
                It.Is<CreateProcedureRequest>(r => r.Name == "TCC Validation SCS"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.CreateAsync(new CreateProcedureRequest
        {
            Name = "TCC Validation SCS",
        });

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("proc-123");
        result.Name.Should().Be("TCC Validation SCS");
    }

    [Fact]
    public async Task GetAsync_ReturnsProcedure()
    {
        // Arrange
        var expected = new Procedure { Id = "proc-123", Name = "TCC Validation SCS" };

        _mockHttpClient
            .Setup(x => x.GetAsync<Procedure>(
                "v2/procedures/proc-123",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.GetAsync("proc-123");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("proc-123");
        result.Name.Should().Be("TCC Validation SCS");
    }

    [Fact]
    public async Task UpdateAsync_SendsCorrectRequest()
    {
        // Arrange
        var expected = new Procedure { Id = "proc-123", Name = "Updated Name" };

        _mockHttpClient
            .Setup(x => x.PatchAsync<UpdateProcedureRequest, Procedure>(
                "v2/procedures/proc-123",
                It.Is<UpdateProcedureRequest>(r => r.Name == "Updated Name"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.UpdateAsync("proc-123", new UpdateProcedureRequest
        {
            Name = "Updated Name"
        });

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("proc-123");
        result.Name.Should().Be("Updated Name");
    }

    [Fact]
    public async Task DeleteAsync_DeletesProcedure()
    {
        // Arrange
        var expected = new DeleteResponse { Id = "proc-123" };

        _mockHttpClient
            .Setup(x => x.DeleteAsync<DeleteResponse>(
                "v2/procedures/proc-123",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.DeleteAsync("proc-123");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("proc-123");
    }
}

public sealed class ProcedureVersionsResourceTests
{
    private readonly Mock<ITofuPilotHttpClient> _mockHttpClient;
    private readonly ProcedureVersionsResource _resource;

    public ProcedureVersionsResourceTests()
    {
        _mockHttpClient = new Mock<ITofuPilotHttpClient>();
        _resource = new ProcedureVersionsResource(_mockHttpClient.Object);
    }

    [Fact]
    public async Task CreateAsync_SendsCorrectRequest()
    {
        // Arrange
        var expected = new ProcedureVersion { Id = "ver-1", Tag = "v1.0", ProcedureId = "proc-123" };

        _mockHttpClient
            .Setup(x => x.PostAsync<CreateProcedureVersionRequest, ProcedureVersion>(
                "v2/procedures/proc-123/versions",
                It.Is<CreateProcedureVersionRequest>(r => r.Tag == "v1.0"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.CreateAsync("proc-123", new CreateProcedureVersionRequest
        {
            Tag = "v1.0"
        });

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("ver-1");
        result.Tag.Should().Be("v1.0");
        result.ProcedureId.Should().Be("proc-123");
    }

    [Fact]
    public async Task GetAsync_ReturnsProcedureVersion()
    {
        // Arrange
        var expected = new ProcedureVersion { Id = "ver-1", Tag = "v1.0", ProcedureId = "proc-123" };

        _mockHttpClient
            .Setup(x => x.GetAsync<ProcedureVersion>(
                "v2/procedures/proc-123/versions/v1.0",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.GetAsync("proc-123", "v1.0");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("ver-1");
        result.Tag.Should().Be("v1.0");
    }

    [Fact]
    public async Task DeleteAsync_DeletesProcedureVersion()
    {
        // Arrange
        var expected = new DeleteResponse { Id = "ver-1" };

        _mockHttpClient
            .Setup(x => x.DeleteAsync<DeleteResponse>(
                "v2/procedures/proc-123/versions/v1.0",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _resource.DeleteAsync("proc-123", "v1.0");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("ver-1");
    }
}
