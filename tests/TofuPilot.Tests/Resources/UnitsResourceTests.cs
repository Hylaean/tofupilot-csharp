using FluentAssertions;
using Moq;
using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Units;
using TofuPilot.Resources;
using Xunit;

namespace TofuPilot.Tests.Resources;

public class UnitsResourceTests
{
    private readonly Mock<ITofuPilotHttpClient> _mockHttpClient;
    private readonly UnitsResource _unitsResource;

    public UnitsResourceTests()
    {
        _mockHttpClient = new Mock<ITofuPilotHttpClient>();
        _unitsResource = new UnitsResource(_mockHttpClient.Object);
    }

    [Fact]
    public async Task CreateAsync_CreatesUnit()
    {
        // Arrange
        var request = new CreateUnitRequest
        {
            SerialNumber = "SN001",
            PartNumber = "PN001",
            RevisionNumber = "R1"
        };

        var expectedUnit = new Unit
        {
            Id = "unit-123",
            SerialNumber = "SN001",
            PartNumber = "PN001"
        };

        _mockHttpClient
            .Setup(x => x.PostAsync<CreateUnitRequest, Unit>(
                It.IsAny<string>(),
                It.Is<CreateUnitRequest>(r => r.SerialNumber == "SN001"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUnit);

        // Act
        var result = await _unitsResource.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("unit-123");
        result.SerialNumber.Should().Be("SN001");
    }

    [Fact]
    public async Task ListAsync_WithFilters_ReturnsFilteredUnits()
    {
        // Arrange
        var expectedUnits = new PaginatedResponse<Unit>
        {
            Data = new List<Unit>
            {
                new() { Id = "unit-1", SerialNumber = "SN001" }
            },
            Meta = new PaginationMeta { HasMore = false }
        };

        _mockHttpClient
            .Setup(x => x.GetAsync<PaginatedResponse<Unit>>(
                It.Is<string>(s => s.Contains("serial_numbers")),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUnits);

        // Act
        var result = await _unitsResource.ListAsync(new ListUnitsRequest
        {
            SerialNumbers = new[] { "SN001" }
        });

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
    }
}
