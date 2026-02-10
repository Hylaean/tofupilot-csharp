using System.Text.Json.Serialization;

namespace TofuPilot.Models.Procedures;

/// <summary>
/// Represents a test procedure.
/// </summary>
public record Procedure
{
    /// <summary>
    /// Gets the unique identifier of the procedure.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the procedure identifier (slug).
    /// </summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; init; }

    /// <summary>
    /// Gets the procedure description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Gets when the procedure was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL to view the procedure.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Gets the versions of this procedure.
    /// </summary>
    [JsonPropertyName("versions")]
    public IReadOnlyList<ProcedureVersion>? Versions { get; init; }
}

/// <summary>
/// Represents a version of a procedure.
/// </summary>
public record ProcedureVersion
{
    /// <summary>
    /// Gets the unique identifier of the version.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the version string.
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; init; }

    /// <summary>
    /// Gets the procedure ID this version belongs to.
    /// </summary>
    [JsonPropertyName("procedureId")]
    public string? ProcedureId { get; init; }

    /// <summary>
    /// Gets when the version was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }
}

/// <summary>
/// Request to create a procedure.
/// </summary>
public record CreateProcedureRequest
{
    /// <summary>
    /// Gets or sets the procedure name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the procedure description.
    /// </summary>
    [JsonPropertyName("description")]
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
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the procedure description.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to create a procedure version.
/// </summary>
public record CreateProcedureVersionRequest
{
    /// <summary>
    /// Gets or sets the version string.
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; init; }
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
