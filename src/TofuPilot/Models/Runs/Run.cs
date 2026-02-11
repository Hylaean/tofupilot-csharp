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
    public required string Id { get; init; }

    /// <summary>
    /// Gets the outcome of the run.
    /// </summary>
    public RunOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the procedure ID.
    /// </summary>
    public string? ProcedureId { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    public string? ProcedureName { get; init; }

    /// <summary>
    /// Gets the procedure version.
    /// </summary>
    public string? ProcedureVersion { get; init; }

    /// <summary>
    /// Gets the serial number of the unit under test.
    /// </summary>
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets the revision number.
    /// </summary>
    public string? RevisionNumber { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    public string? BatchNumber { get; init; }

    /// <summary>
    /// Gets when the run started.
    /// </summary>
    public DateTimeOffset? StartedAt { get; init; }

    /// <summary>
    /// Gets when the run ended.
    /// </summary>
    public DateTimeOffset? EndedAt { get; init; }

    /// <summary>
    /// Gets when the run was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets the duration of the run in ISO 8601 format.
    /// </summary>
    public string? Duration { get; init; }

    /// <summary>
    /// Gets the URL to view the run.
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// Gets the phases of the run.
    /// </summary>
    public IReadOnlyList<RunPhase>? Phases { get; init; }

    /// <summary>
    /// Gets the logs of the run.
    /// </summary>
    public IReadOnlyList<RunLog>? Logs { get; init; }

    /// <summary>
    /// Gets the attachments of the run.
    /// </summary>
    public IReadOnlyList<RunAttachment>? Attachments { get; init; }

    /// <summary>
    /// Gets the docstring/notes for the run.
    /// </summary>
    public string? Docstring { get; init; }

    /// <summary>
    /// Gets the operator email.
    /// </summary>
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
    public required string Name { get; init; }

    /// <summary>
    /// Gets the phase outcome.
    /// </summary>
    public PhaseOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the start time.
    /// </summary>
    public DateTimeOffset StartedAt { get; init; }

    /// <summary>
    /// Gets the end time.
    /// </summary>
    public DateTimeOffset EndedAt { get; init; }

    /// <summary>
    /// Gets the measurements in this phase.
    /// </summary>
    public IReadOnlyList<RunMeasurement>? Measurements { get; init; }

    /// <summary>
    /// Gets the docstring for this phase.
    /// </summary>
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
    public required string Name { get; init; }

    /// <summary>
    /// Gets the measurement outcome.
    /// </summary>
    public MeasurementOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the measured value.
    /// </summary>
    public object? MeasuredValue { get; init; }

    /// <summary>
    /// Gets the units of measurement.
    /// </summary>
    public string? Units { get; init; }

    /// <summary>
    /// Gets the lower limit.
    /// </summary>
    public double? LowerLimit { get; init; }

    /// <summary>
    /// Gets the upper limit.
    /// </summary>
    public double? UpperLimit { get; init; }

    /// <summary>
    /// Gets the validators.
    /// </summary>
    public IReadOnlyList<MeasurementValidator>? Validators { get; init; }

    /// <summary>
    /// Gets the docstring for this measurement.
    /// </summary>
    public string? Docstring { get; init; }
}

/// <summary>
/// Represents a validator result for a measurement.
/// </summary>
public record MeasurementValidator
{
    /// <summary>
    /// Gets the validator outcome.
    /// </summary>
    public MeasurementOutcome Outcome { get; init; }

    /// <summary>
    /// Gets the comparison operator.
    /// </summary>
    public string? Operator { get; init; }

    /// <summary>
    /// Gets the expected value.
    /// </summary>
    public double? ExpectedValue { get; init; }

    /// <summary>
    /// Gets the validation expression.
    /// </summary>
    public string? Expression { get; init; }

    /// <summary>
    /// Gets whether this validator is decisive.
    /// </summary>
    public bool IsDecisive { get; init; }

    /// <summary>
    /// Gets whether this is expression-only.
    /// </summary>
    public bool IsExpressionOnly { get; init; }
}

/// <summary>
/// Represents a log entry in a run.
/// </summary>
public record RunLog
{
    /// <summary>
    /// Gets the log level.
    /// </summary>
    public Abstractions.Models.LogLevel Level { get; init; }

    /// <summary>
    /// Gets the timestamp.
    /// </summary>
    public string? Timestamp { get; init; }

    /// <summary>
    /// Gets the log message.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Gets the source file.
    /// </summary>
    public string? SourceFile { get; init; }

    /// <summary>
    /// Gets the line number.
    /// </summary>
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
    public string? Id { get; init; }

    /// <summary>
    /// Gets the file name.
    /// </summary>
    public string? FileName { get; init; }

    /// <summary>
    /// Gets the URL to download the attachment.
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// Gets the content type.
    /// </summary>
    public string? ContentType { get; init; }
}
