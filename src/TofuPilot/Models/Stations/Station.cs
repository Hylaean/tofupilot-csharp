using System.Text.Json.Serialization;

namespace TofuPilot.Models.Stations;

/// <summary>
/// Represents a test station.
/// </summary>
public record Station
{
    /// <summary>
    /// Gets the unique identifier of the station.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the station name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the station description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets when the station was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the station.
    /// </summary>
    public string? Url { get; init; }

}


/// <summary>
/// Request to create a station.
/// </summary>
public record CreateStationRequest
{
    /// <summary>
    /// Gets or sets the station name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the station description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to update a station.
/// </summary>
public record UpdateStationRequest
{
    /// <summary>
    /// Gets or sets the station name.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the station description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request parameters for listing stations.
/// </summary>
public record ListStationsRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific station IDs to filter by.
    /// </summary>
    public IReadOnlyList<string>? Ids { get; init; }

    /// <summary>
    /// Gets or sets the page size limit.
    /// </summary>
    public int? Limit { get; init; } = 50;

    /// <summary>
    /// Gets or sets the cursor for pagination.
    /// </summary>
    public double? Cursor { get; init; }
}
