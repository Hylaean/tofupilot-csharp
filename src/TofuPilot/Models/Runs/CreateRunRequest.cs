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
    public required RunOutcome Outcome { get; init; }

    /// <summary>
    /// Gets or sets the procedure ID.
    /// </summary>
    public required string ProcedureId { get; init; }

    /// <summary>
    /// Gets or sets when the run started.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }

    /// <summary>
    /// Gets or sets when the run ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }

    /// <summary>
    /// Gets or sets the serial number of the unit under test.
    /// </summary>
    public required string SerialNumber { get; init; }

    /// <summary>
    /// Gets or sets the procedure version.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProcedureVersion { get; init; }

    /// <summary>
    /// Gets or sets the operator email.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OperatedBy { get; init; }

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

    /// <summary>
    /// Gets or sets the sub-unit serial numbers.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? SubUnits { get; init; }

    /// <summary>
    /// Gets or sets the docstring/notes for the run.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Docstring { get; init; }

    /// <summary>
    /// Gets or sets the phases of the run.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<CreateRunPhase>? Phases { get; init; }

    /// <summary>
    /// Gets or sets the logs of the run.
    /// </summary>
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
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the phase outcome.
    /// </summary>
    public required PhaseOutcome Outcome { get; init; }

    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }

    /// <summary>
    /// Gets or sets the end time.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }

    /// <summary>
    /// Gets or sets the measurements in this phase.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<CreateRunMeasurement>? Measurements { get; init; }

    /// <summary>
    /// Gets or sets the docstring for this phase.
    /// </summary>
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
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the measurement outcome.
    /// </summary>
    public required MeasurementOutcome Outcome { get; init; }

    /// <summary>
    /// Gets or sets the measured value.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? MeasuredValue { get; init; }

    /// <summary>
    /// Gets or sets the units of measurement.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Units { get; init; }

    /// <summary>
    /// Gets or sets the lower limit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? LowerLimit { get; init; }

    /// <summary>
    /// Gets or sets the upper limit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? UpperLimit { get; init; }

    /// <summary>
    /// Gets or sets the validators.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Validators { get; init; }

    /// <summary>
    /// Gets or sets the docstring for this measurement.
    /// </summary>
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
    public required Abstractions.Models.LogLevel Level { get; init; }

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    public required string Timestamp { get; init; }

    /// <summary>
    /// Gets or sets the log message.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// Gets or sets the source file.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SourceFile { get; init; }

    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? LineNumber { get; init; }
}
