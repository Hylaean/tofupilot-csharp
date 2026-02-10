using System.Text.Json.Serialization;
using TofuPilot.Abstractions.Models;

namespace TofuPilot.Models.Runs;

/// <summary>
/// Represents a test run.
/// </summary>
public record Run
{
    /// <summary>
    /// Gets the unique identifier of the run.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets the outcome of the run.
    /// </summary>
    [JsonPropertyName("outcome")]
    public RunOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the procedure ID.
    /// </summary>
    [JsonPropertyName("procedureId")]
    public string? ProcedureId { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    [JsonPropertyName("procedureName")]
    public string? ProcedureName { get; init; }

    /// <summary>
    /// Gets the procedure version.
    /// </summary>
    [JsonPropertyName("procedureVersion")]
    public string? ProcedureVersion { get; init; }

    /// <summary>
    /// Gets the serial number of the unit under test.
    /// </summary>
    [JsonPropertyName("serialNumber")]
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    [JsonPropertyName("partNumber")]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets the revision number.
    /// </summary>
    [JsonPropertyName("revisionNumber")]
    public string? RevisionNumber { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    [JsonPropertyName("batchNumber")]
    public string? BatchNumber { get; init; }

    /// <summary>
    /// Gets when the run started.
    /// </summary>
    [JsonPropertyName("startedAt")]
    public DateTimeOffset? StartedAt { get; init; }

    /// <summary>
    /// Gets when the run ended.
    /// </summary>
    [JsonPropertyName("endedAt")]
    public DateTimeOffset? EndedAt { get; init; }

    /// <summary>
    /// Gets when the run was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the duration of the run in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; init; }

    /// <summary>
    /// Gets the URL to view the run.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Gets the phases of the run.
    /// </summary>
    [JsonPropertyName("phases")]
    public IReadOnlyList<RunPhase>? Phases { get; init; }

    /// <summary>
    /// Gets the logs of the run.
    /// </summary>
    [JsonPropertyName("logs")]
    public IReadOnlyList<RunLog>? Logs { get; init; }

    /// <summary>
    /// Gets the attachments of the run.
    /// </summary>
    [JsonPropertyName("attachments")]
    public IReadOnlyList<RunAttachment>? Attachments { get; init; }

    /// <summary>
    /// Gets the docstring/notes for the run.
    /// </summary>
    [JsonPropertyName("docstring")]
    public string? Docstring { get; init; }

    /// <summary>
    /// Gets the operator email.
    /// </summary>
    [JsonPropertyName("operatedBy")]
    public string? OperatedBy { get; init; }
}

/// <summary>
/// Represents a phase within a test run.
/// </summary>
public record RunPhase
{
    /// <summary>
    /// Gets the phase name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the phase outcome.
    /// </summary>
    [JsonPropertyName("outcome")]
    public PhaseOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the start time in milliseconds.
    /// </summary>
    [JsonPropertyName("startTimeMillis")]
    public long StartTimeMillis { get; init; }

    /// <summary>
    /// Gets the end time in milliseconds.
    /// </summary>
    [JsonPropertyName("endTimeMillis")]
    public long EndTimeMillis { get; init; }

    /// <summary>
    /// Gets the measurements in this phase.
    /// </summary>
    [JsonPropertyName("measurements")]
    public IReadOnlyList<RunMeasurement>? Measurements { get; init; }

    /// <summary>
    /// Gets the docstring for this phase.
    /// </summary>
    [JsonPropertyName("docstring")]
    public string? Docstring { get; init; }
}

/// <summary>
/// Represents a measurement within a phase.
/// </summary>
public record RunMeasurement
{
    /// <summary>
    /// Gets the measurement name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the measurement outcome.
    /// </summary>
    [JsonPropertyName("outcome")]
    public MeasurementOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the measured value.
    /// </summary>
    [JsonPropertyName("measuredValue")]
    public object? MeasuredValue { get; init; }

    /// <summary>
    /// Gets the units of measurement.
    /// </summary>
    [JsonPropertyName("units")]
    public string? Units { get; init; }

    /// <summary>
    /// Gets the lower limit.
    /// </summary>
    [JsonPropertyName("lowerLimit")]
    public double? LowerLimit { get; init; }

    /// <summary>
    /// Gets the upper limit.
    /// </summary>
    [JsonPropertyName("upperLimit")]
    public double? UpperLimit { get; init; }

    /// <summary>
    /// Gets the validators.
    /// </summary>
    [JsonPropertyName("validators")]
    public IReadOnlyList<string>? Validators { get; init; }

    /// <summary>
    /// Gets the docstring for this measurement.
    /// </summary>
    [JsonPropertyName("docstring")]
    public string? Docstring { get; init; }
}

/// <summary>
/// Represents a log entry in a run.
/// </summary>
public record RunLog
{
    /// <summary>
    /// Gets the log level.
    /// </summary>
    [JsonPropertyName("level")]
    public Abstractions.Models.LogLevel Level { get; init; }

    /// <summary>
    /// Gets the timestamp.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public string? Timestamp { get; init; }

    /// <summary>
    /// Gets the log message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Gets the source file.
    /// </summary>
    [JsonPropertyName("sourceFile")]
    public string? SourceFile { get; init; }

    /// <summary>
    /// Gets the line number.
    /// </summary>
    [JsonPropertyName("lineNumber")]
    public int? LineNumber { get; init; }
}

/// <summary>
/// Represents an attachment in a run.
/// </summary>
public record RunAttachment
{
    /// <summary>
    /// Gets the attachment ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Gets the file name.
    /// </summary>
    [JsonPropertyName("fileName")]
    public string? FileName { get; init; }

    /// <summary>
    /// Gets the URL to download the attachment.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Gets the content type.
    /// </summary>
    [JsonPropertyName("contentType")]
    public string? ContentType { get; init; }
}
