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
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    [JsonPropertyName("batchNumber")]
    public required string BatchNumber { get; init; }

    /// <summary>
    /// Gets the part ID this batch is associated with.
    /// </summary>
    [JsonPropertyName("partId")]
    public string? PartId { get; init; }

    /// <summary>
    /// Gets when the batch was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the batch.
    /// </summary>
    [JsonPropertyName("url")]
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
    [JsonPropertyName("batchNumber")]
    public required string BatchNumber { get; init; }

    /// <summary>
    /// Gets or sets the part number to associate with.
    /// </summary>
    [JsonPropertyName("partNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }
}

/// <summary>
/// Request to update a batch.
/// </summary>
public record UpdateBatchRequest
{
    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    [JsonPropertyName("batchNumber")]
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
