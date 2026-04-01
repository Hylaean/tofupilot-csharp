using System.Text.Json.Serialization;
using Hylaean.TofuPilot.Models.Common;

namespace Hylaean.TofuPilot.Models.Batches;

/// <summary>
/// Represents a production batch.
/// </summary>
public record Batch
{
    /// <summary>
    /// Gets the unique identifier of the batch.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    [JsonPropertyName("number")]
    public string? BatchNumber { get; init; }

    /// <summary>
    /// Gets when the batch was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the user who created this batch.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the station that created this batch.
    /// </summary>
    public CreatedByStation? CreatedByStation { get; init; }

    /// <summary>
    /// Gets the units in this batch (GET only).
    /// </summary>
    public IReadOnlyList<BatchUnit>? Units { get; init; }

    /// <summary>
    /// Gets the total number of units (LIST only).
    /// </summary>
    public int? UnitCount { get; init; }
}

/// <summary>
/// Represents a unit nested in a batch GET response.
/// </summary>
public record BatchUnit
{
    /// <summary>
    /// Gets the unit ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the serial number.
    /// </summary>
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets when the unit was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the part information.
    /// </summary>
    public BatchUnitPart? Part { get; init; }
}

/// <summary>
/// Represents the part nested in a batch unit.
/// </summary>
public record BatchUnitPart
{
    /// <summary>
    /// Gets the part ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public string? Number { get; init; }

    /// <summary>
    /// Gets the part name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the revision information.
    /// </summary>
    public BatchUnitPartRevision? Revision { get; init; }
}

/// <summary>
/// Represents the revision nested in a batch unit part.
/// </summary>
public record BatchUnitPartRevision
{
    /// <summary>
    /// Gets the revision ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the revision number.
    /// </summary>
    public string? Number { get; init; }
}

/// <summary>
/// Request to create a batch.
/// </summary>
public record CreateBatchRequest
{
    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    [JsonPropertyName("number")]
    public required string BatchNumber { get; init; }

    /// <summary>
    /// Gets or sets the part number to associate with.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }
}

/// <summary>
/// Request to update a batch.
/// </summary>
public record UpdateBatchRequest
{
    /// <summary>
    /// Gets or sets the new batch number.
    /// </summary>
    [JsonPropertyName("new_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BatchNumber { get; init; }
}

/// <summary>
/// Request parameters for listing batches.
/// </summary>
public record ListBatchesRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific batch IDs to filter by.
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
