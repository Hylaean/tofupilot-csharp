using System.Text.Json.Serialization;
using TofuPilot.Abstractions.Models;

namespace TofuPilot.Models.Runs;

/// <summary>
/// Request to create a new run.
/// </summary>
public record CreateRunRequest
{
    /// <summary>
    /// Gets or sets the outcome of the run.
    /// </summary>
    [JsonPropertyName("outcome")]
    public required RunOutcome Outcome { get; init; }

    /// <summary>
    /// Gets or sets the procedure ID.
    /// </summary>
    [JsonPropertyName("procedureId")]
    public required string ProcedureId { get; init; }

    /// <summary>
    /// Gets or sets when the run started.
    /// </summary>
    [JsonPropertyName("startedAt")]
    public required DateTimeOffset StartedAt { get; init; }

    /// <summary>
    /// Gets or sets when the run ended.
    /// </summary>
    [JsonPropertyName("endedAt")]
    public required DateTimeOffset EndedAt { get; init; }

    /// <summary>
    /// Gets or sets the serial number of the unit under test.
    /// </summary>
    [JsonPropertyName("serialNumber")]
    public required string SerialNumber { get; init; }

    /// <summary>
    /// Gets or sets the procedure version.
    /// </summary>
    [JsonPropertyName("procedureVersion")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProcedureVersion { get; init; }

    /// <summary>
    /// Gets or sets the operator email.
    /// </summary>
    [JsonPropertyName("operatedBy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OperatedBy { get; init; }

    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    [JsonPropertyName("partNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the revision number.
    /// </summary>
    [JsonPropertyName("revisionNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RevisionNumber { get; init; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    [JsonPropertyName("batchNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BatchNumber { get; init; }

    /// <summary>
    /// Gets or sets the sub-unit serial numbers.
    /// </summary>
    [JsonPropertyName("subUnits")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? SubUnits { get; init; }

    /// <summary>
    /// Gets or sets the docstring/notes for the run.
    /// </summary>
    [JsonPropertyName("docstring")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Docstring { get; init; }

    /// <summary>
    /// Gets or sets the phases of the run.
    /// </summary>
    [JsonPropertyName("phases")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<CreateRunPhase>? Phases { get; init; }

    /// <summary>
    /// Gets or sets the logs of the run.
    /// </summary>
    [JsonPropertyName("logs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<CreateRunLog>? Logs { get; init; }
}

/// <summary>
/// Request to create a phase within a run.
/// </summary>
public record CreateRunPhase
{
    /// <summary>
    /// Gets or sets the phase name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the phase outcome.
    /// </summary>
    [JsonPropertyName("outcome")]
    public required PhaseOutcome Outcome { get; init; }

    /// <summary>
    /// Gets or sets the start time in milliseconds.
    /// </summary>
    [JsonPropertyName("startTimeMillis")]
    public required long StartTimeMillis { get; init; }

    /// <summary>
    /// Gets or sets the end time in milliseconds.
    /// </summary>
    [JsonPropertyName("endTimeMillis")]
    public required long EndTimeMillis { get; init; }

    /// <summary>
    /// Gets or sets the measurements in this phase.
    /// </summary>
    [JsonPropertyName("measurements")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<CreateRunMeasurement>? Measurements { get; init; }

    /// <summary>
    /// Gets or sets the docstring for this phase.
    /// </summary>
    [JsonPropertyName("docstring")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Docstring { get; init; }
}

/// <summary>
/// Request to create a measurement within a phase.
/// </summary>
public record CreateRunMeasurement
{
    /// <summary>
    /// Gets or sets the measurement name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the measurement outcome.
    /// </summary>
    [JsonPropertyName("outcome")]
    public required MeasurementOutcome Outcome { get; init; }

    /// <summary>
    /// Gets or sets the measured value.
    /// </summary>
    [JsonPropertyName("measuredValue")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? MeasuredValue { get; init; }

    /// <summary>
    /// Gets or sets the units of measurement.
    /// </summary>
    [JsonPropertyName("units")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Units { get; init; }

    /// <summary>
    /// Gets or sets the lower limit.
    /// </summary>
    [JsonPropertyName("lowerLimit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? LowerLimit { get; init; }

    /// <summary>
    /// Gets or sets the upper limit.
    /// </summary>
    [JsonPropertyName("upperLimit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? UpperLimit { get; init; }

    /// <summary>
    /// Gets or sets the validators.
    /// </summary>
    [JsonPropertyName("validators")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Validators { get; init; }

    /// <summary>
    /// Gets or sets the docstring for this measurement.
    /// </summary>
    [JsonPropertyName("docstring")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Docstring { get; init; }
}

/// <summary>
/// Request to create a log entry within a run.
/// </summary>
public record CreateRunLog
{
    /// <summary>
    /// Gets or sets the log level.
    /// </summary>
    [JsonPropertyName("level")]
    public required Abstractions.Models.LogLevel Level { get; init; }

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public required string Timestamp { get; init; }

    /// <summary>
    /// Gets or sets the log message.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    /// <summary>
    /// Gets or sets the source file.
    /// </summary>
    [JsonPropertyName("sourceFile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SourceFile { get; init; }

    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    [JsonPropertyName("lineNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? LineNumber { get; init; }
}
