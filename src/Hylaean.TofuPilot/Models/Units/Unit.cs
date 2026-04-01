using System.Text.Json.Serialization;
using Hylaean.TofuPilot.Models.Common;

namespace Hylaean.TofuPilot.Models.Units;

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
    /// Gets when the unit was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the user who created this unit.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the station that created this unit.
    /// </summary>
    public CreatedByStation? CreatedByStation { get; init; }

    /// <summary>
    /// Gets the part information.
    /// </summary>
    public UnitPart? Part { get; init; }

    /// <summary>
    /// Gets the batch information.
    /// </summary>
    public UnitBatch? Batch { get; init; }

    /// <summary>
    /// Gets the parent unit.
    /// </summary>
    public Unit? Parent { get; init; }

    /// <summary>
    /// Gets the child units.
    /// </summary>
    public IReadOnlyList<Unit>? Children { get; init; }

    /// <summary>
    /// Gets the run that created this unit.
    /// </summary>
    public UnitCreatedDuring? CreatedDuring { get; init; }

    /// <summary>
    /// Gets the attachments for this unit.
    /// </summary>
    public IReadOnlyList<UnitAttachment>? Attachments { get; init; }

    /// <summary>
    /// Gets the most recent test run (LIST only).
    /// </summary>
    public UnitLastRun? LastRun { get; init; }
}

/// <summary>
/// Represents the part nested in a unit response.
/// </summary>
public record UnitPart
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
    public UnitPartRevision? Revision { get; init; }
}

/// <summary>
/// Represents the revision nested in a unit part response.
/// </summary>
public record UnitPartRevision
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
/// Represents the batch nested in a unit response.
/// </summary>
public record UnitBatch
{
    /// <summary>
    /// Gets the batch ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    public string? Number { get; init; }
}

/// <summary>
/// Represents the run that created a unit.
/// </summary>
public record UnitCreatedDuring
{
    /// <summary>
    /// Gets the run ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets when the run was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets when the run started.
    /// </summary>
    public DateTimeOffset StartedAt { get; init; }

    /// <summary>
    /// Gets when the run ended.
    /// </summary>
    public DateTimeOffset EndedAt { get; init; }

    /// <summary>
    /// Gets the duration in ISO 8601 format.
    /// </summary>
    public string? Duration { get; init; }

    /// <summary>
    /// Gets the run outcome.
    /// </summary>
    public string? Outcome { get; init; }

    /// <summary>
    /// Gets the procedure information.
    /// </summary>
    public UnitCreatedDuringProcedure? Procedure { get; init; }
}

/// <summary>
/// Represents the procedure nested in a created_during run.
/// </summary>
public record UnitCreatedDuringProcedure
{
    /// <summary>
    /// Gets the procedure ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    public string? Name { get; init; }
}

/// <summary>
/// Represents an attachment on a unit.
/// </summary>
public record UnitAttachment
{
    /// <summary>
    /// Gets the attachment ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the file name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    public long? Size { get; init; }

    /// <summary>
    /// Gets the content type.
    /// </summary>
    public string? ContentType { get; init; }

    /// <summary>
    /// Gets the presigned download URL.
    /// </summary>
    public string? DownloadUrl { get; init; }
}

/// <summary>
/// Represents the most recent run on a unit (LIST response).
/// </summary>
public record UnitLastRun
{
    /// <summary>
    /// Gets the run ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the run outcome.
    /// </summary>
    public string? Outcome { get; init; }

    /// <summary>
    /// Gets when the run started.
    /// </summary>
    public DateTimeOffset? StartedAt { get; init; }

    /// <summary>
    /// Gets when the run ended.
    /// </summary>
    public DateTimeOffset? EndedAt { get; init; }

    /// <summary>
    /// Gets the procedure information.
    /// </summary>
    public UnitLastRunProcedure? Procedure { get; init; }
}

/// <summary>
/// Represents the procedure in a unit's last run.
/// </summary>
public record UnitLastRunProcedure
{
    /// <summary>
    /// Gets the procedure ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    public string? Name { get; init; }
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
