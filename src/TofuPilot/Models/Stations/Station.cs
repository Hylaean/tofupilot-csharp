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
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the station name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the station description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Gets when the station was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the station.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Gets the linked procedure IDs.
    /// </summary>
    [JsonPropertyName("linkedProcedureIds")]
    public IReadOnlyList<string>? LinkedProcedureIds { get; init; }
}

/// <summary>
/// Request to create a station.
/// </summary>
public record CreateStationRequest
{
    /// <summary>
    /// Gets or sets the station name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the station description.
    /// </summary>
    [JsonPropertyName("description")]
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
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the station description.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to link a procedure to a station.
/// </summary>
public record LinkProcedureRequest
{
    /// <summary>
    /// Gets or sets the procedure ID to link.
    /// </summary>
    [JsonPropertyName("procedureId")]
    public required string ProcedureId { get; init; }
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
