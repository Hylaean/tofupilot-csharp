using System.Text.Json.Serialization;
using Hylaean.TofuPilot.Models.Common;

namespace Hylaean.TofuPilot.Models.Parts;

/// <summary>
/// Represents a part/component.
/// </summary>
public record Part
{
    /// <summary>
    /// Gets the unique identifier of the part.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    [JsonPropertyName("number")]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets the part name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets when the part was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the user who created this part.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the station that created this part.
    /// </summary>
    public CreatedByStation? CreatedByStation { get; init; }

    /// <summary>
    /// Gets the revisions of this part.
    /// </summary>
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
    public required string Id { get; init; }

    /// <summary>
    /// Gets the revision number.
    /// </summary>
    [JsonPropertyName("number")]
    public string? RevisionNumber { get; init; }

    /// <summary>
    /// Gets when the revision was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the user who created this revision.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the station that created this revision.
    /// </summary>
    public CreatedByStation? CreatedByStation { get; init; }

    /// <summary>
    /// Gets the part this revision belongs to (GET only).
    /// </summary>
    public PartRevisionPart? Part { get; init; }

    /// <summary>
    /// Gets the units built with this revision (GET only).
    /// </summary>
    public IReadOnlyList<PartRevisionUnit>? Units { get; init; }
}

/// <summary>
/// Represents the part nested in a revision GET response.
/// </summary>
public record PartRevisionPart
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
}

/// <summary>
/// Represents a unit nested in a revision GET response.
/// </summary>
public record PartRevisionUnit
{
    /// <summary>
    /// Gets the unit ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the serial number.
    /// </summary>
    public string? SerialNumber { get; init; }
}

/// <summary>
/// Request to create a part.
/// </summary>
public record CreatePartRequest
{
    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    [JsonPropertyName("number")]
    public required string PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to update a part.
/// </summary>
public record UpdatePartRequest
{
    /// <summary>
    /// Gets or sets the new part number.
    /// </summary>
    [JsonPropertyName("new_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
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
    [JsonPropertyName("number")]
    public required string RevisionNumber { get; init; }
}

/// <summary>
/// Request to update a part revision.
/// </summary>
public record UpdatePartRevisionRequest
{
    /// <summary>
    /// Gets or sets the new revision number.
    /// </summary>
    [JsonPropertyName("number")]
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
