using System.Text.Json.Serialization;

namespace TofuPilot.Models.Parts;

/// <summary>
/// Represents a part/component.
/// </summary>
public record Part
{
    /// <summary>
    /// Gets the unique identifier of the part.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    [JsonPropertyName("partNumber")]
    public required string PartNumber { get; init; }

    /// <summary>
    /// Gets the part name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets the part description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Gets when the part was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the part.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Gets the revisions of this part.
    /// </summary>
    [JsonPropertyName("revisions")]
    public IReadOnlyList<PartRevision>? Revisions { get; init; }
}

/// <summary>
/// Represents a revision of a part.
/// </summary>
public record PartRevision
{
    /// <summary>
    /// Gets the unique identifier of the revision.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the revision number.
    /// </summary>
    [JsonPropertyName("revisionNumber")]
    public required string RevisionNumber { get; init; }

    /// <summary>
    /// Gets the part ID this revision belongs to.
    /// </summary>
    [JsonPropertyName("partId")]
    public string? PartId { get; init; }

    /// <summary>
    /// Gets when the revision was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }
}

/// <summary>
/// Request to create a part.
/// </summary>
public record CreatePartRequest
{
    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    [JsonPropertyName("partNumber")]
    public required string PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to update a part.
/// </summary>
public record UpdatePartRequest
{
    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    [JsonPropertyName("partNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to create a part revision.
/// </summary>
public record CreatePartRevisionRequest
{
    /// <summary>
    /// Gets or sets the revision number.
    /// </summary>
    [JsonPropertyName("revisionNumber")]
    public required string RevisionNumber { get; init; }
}

/// <summary>
/// Request to update a part revision.
/// </summary>
public record UpdatePartRevisionRequest
{
    /// <summary>
    /// Gets or sets the revision number.
    /// </summary>
    [JsonPropertyName("revisionNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RevisionNumber { get; init; }
}

/// <summary>
/// Request parameters for listing parts.
/// </summary>
public record ListPartsRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific part IDs to filter by.
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
