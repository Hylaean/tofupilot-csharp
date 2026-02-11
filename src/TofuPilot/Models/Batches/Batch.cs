using System.Text.Json.Serialization;

namespace TofuPilot.Models.Batches;

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
    /// Gets the part ID this batch is associated with.
    /// </summary>
    public string? PartId { get; init; }

    /// <summary>
    /// Gets when the batch was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the batch.
    /// </summary>
    public string? Url { get; init; }
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
