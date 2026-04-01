using System.Text.Json.Serialization;
using Hylaean.TofuPilot.Models.Common;

namespace Hylaean.TofuPilot.Models.Procedures;

/// <summary>
/// Represents a test procedure.
/// </summary>
public record Procedure
{
    /// <summary>
    /// Gets the unique identifier of the procedure.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the procedure identifier (slug).
    /// </summary>
    public string? Identifier { get; init; }

    /// <summary>
    /// Gets when the procedure was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the user who created this procedure.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the total number of runs (GET only).
    /// </summary>
    public int? RunsCount { get; init; }

    /// <summary>
    /// Gets recent runs (GET returns full objects, LIST returns summary).
    /// </summary>
    public IReadOnlyList<ProcedureRun>? RecentRuns { get; init; }

    /// <summary>
    /// Gets recent runs (LIST response field name).
    /// </summary>
    public IReadOnlyList<ProcedureRun>? Runs { get; init; }

    /// <summary>
    /// Gets the stations linked to this procedure (GET only).
    /// </summary>
    public IReadOnlyList<ProcedureStation>? Stations { get; init; }

    /// <summary>
    /// Gets the linked repository (LIST only).
    /// </summary>
    public ProcedureLinkedRepository? LinkedRepository { get; init; }
}

/// <summary>
/// Represents a run summary nested in a procedure response.
/// </summary>
public record ProcedureRun
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
    /// Gets the unit information.
    /// </summary>
    public ProcedureRunUnit? Unit { get; init; }
}

/// <summary>
/// Represents a unit nested in a procedure run.
/// </summary>
public record ProcedureRunUnit
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
/// Represents a station linked to a procedure.
/// </summary>
public record ProcedureStation
{
    /// <summary>
    /// Gets the station ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the station name.
    /// </summary>
    public string? Name { get; init; }
}

/// <summary>
/// Represents a linked repository on a procedure.
/// </summary>
public record ProcedureLinkedRepository
{
    /// <summary>
    /// Gets the repository ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the repository name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the full repository name (owner/repo).
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// Gets the git provider.
    /// </summary>
    public string? Provider { get; init; }
}

/// <summary>
/// Represents a version of a procedure.
/// </summary>
public record ProcedureVersion
{
    /// <summary>
    /// Gets the unique identifier of the version.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the version tag string.
    /// </summary>
    public string? Tag { get; init; }

    /// <summary>
    /// Gets when the version was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the user who created this version.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the station that created this version.
    /// </summary>
    public CreatedByStation? CreatedByStation { get; init; }

    /// <summary>
    /// Gets the procedure this version belongs to.
    /// </summary>
    public ProcedureVersionProcedure? Procedure { get; init; }

    /// <summary>
    /// Gets the number of runs using this version.
    /// </summary>
    public int? RunCount { get; init; }
}

/// <summary>
/// Represents the procedure nested in a version response.
/// </summary>
public record ProcedureVersionProcedure
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
/// Request to create a procedure.
/// </summary>
public record CreateProcedureRequest
{
    /// <summary>
    /// Gets or sets the procedure name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the procedure description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to update a procedure.
/// </summary>
public record UpdateProcedureRequest
{
    /// <summary>
    /// Gets or sets the procedure name.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the procedure description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to create a procedure version.
/// </summary>
public record CreateProcedureVersionRequest
{
    /// <summary>
    /// Gets or sets the version tag string.
    /// </summary>
    public required string Tag { get; init; }
}

/// <summary>
/// Request parameters for listing procedures.
/// </summary>
public record ListProceduresRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific procedure IDs to filter by.
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
