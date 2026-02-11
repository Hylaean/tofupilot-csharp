using System.Text.Json.Serialization;

namespace TofuPilot.Models.Units;

/// <summary>
/// Represents a unit under test.
/// </summary>
public record Unit
{
    /// <summary>
    /// Gets the unique identifier of the unit.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the serial number.
    /// </summary>
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets the part ID.
    /// </summary>
    public string? PartId { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets the revision ID.
    /// </summary>
    public string? RevisionId { get; init; }

    /// <summary>
    /// Gets the revision number.
    /// </summary>
    public string? RevisionNumber { get; init; }

    /// <summary>
    /// Gets the batch ID.
    /// </summary>
    public string? BatchId { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    public string? BatchNumber { get; init; }

    /// <summary>
    /// Gets when the unit was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the unit.
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// Gets the child units.
    /// </summary>
    public IReadOnlyList<Unit>? Children { get; init; }

    /// <summary>
    /// Gets the parent units.
    /// </summary>
    public IReadOnlyList<Unit>? Parents { get; init; }
}

/// <summary>
/// Request to create a unit.
/// </summary>
public record CreateUnitRequest
{
    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    public required string SerialNumber { get; init; }

    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    public required string PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the revision number.
    /// </summary>
    public required string RevisionNumber { get; init; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BatchNumber { get; init; }
}

/// <summary>
/// Request to update a unit.
/// </summary>
public record UpdateUnitRequest
{
    /// <summary>
    /// Gets or sets the new serial number.
    /// </summary>
    [JsonPropertyName("new_serial_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the revision number.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RevisionNumber { get; init; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BatchNumber { get; init; }
}

/// <summary>
/// Request to add a child unit.
/// </summary>
public record AddChildRequest
{
    /// <summary>
    /// Gets or sets the child serial number.
    /// </summary>
    public required string ChildSerialNumber { get; init; }
}

/// <summary>
/// Request parameters for listing units.
/// </summary>
public record ListUnitsRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific unit IDs to filter by.
    /// </summary>
    public IReadOnlyList<string>? Ids { get; init; }

    /// <summary>
    /// Gets or sets the serial numbers to filter by.
    /// </summary>
    public IReadOnlyList<string>? SerialNumbers { get; init; }

    /// <summary>
    /// Gets or sets the part numbers to filter by.
    /// </summary>
    public IReadOnlyList<string>? PartNumbers { get; init; }

    /// <summary>
    /// Gets or sets the page size limit.
    /// </summary>
    public int? Limit { get; init; } = 50;

    /// <summary>
    /// Gets or sets the cursor for pagination.
    /// </summary>
    public double? Cursor { get; init; }
}
