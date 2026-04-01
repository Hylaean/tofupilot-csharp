using Hylaean.TofuPilot.Abstractions.Models;
using Hylaean.TofuPilot.Models.Common;

namespace Hylaean.TofuPilot.Models.Runs;

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
    /// Gets the procedure associated with this run.
    /// </summary>
    public RunProcedure? Procedure { get; init; }

    /// <summary>
    /// Gets the unit associated with this run.
    /// </summary>
    public RunUnit? Unit { get; init; }

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
    /// Gets the docstring/notes for the run.
    /// </summary>
    public string? Docstring { get; init; }

    /// <summary>
    /// Gets the user who created this run.
    /// </summary>
    public CreatedByUser? CreatedByUser { get; init; }

    /// <summary>
    /// Gets the station that created this run.
    /// </summary>
    public CreatedByStation? CreatedByStation { get; init; }

    /// <summary>
    /// Gets the operator of this run.
    /// </summary>
    public CreatedByUser? OperatedBy { get; init; }

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
    /// Gets the sub-units that had parent changes during this run.
    /// </summary>
    public IReadOnlyList<RunSubUnit>? SubUnits { get; init; }
}

/// <summary>
/// Represents a phase within a test run.
/// </summary>
public record RunPhase
{
    /// <summary>
    /// Gets the phase ID.
    /// </summary>
    public string? Id { get; init; }

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
    /// Gets the duration in ISO 8601 format.
    /// </summary>
    public string? Duration { get; init; }

    /// <summary>
    /// Gets the retry count (0 = first attempt).
    /// </summary>
    public int RetryCount { get; init; }

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
    /// Gets the measurement ID.
    /// </summary>
    public string? Id { get; init; }

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
    /// Gets the validators.
    /// </summary>
    public IReadOnlyList<MeasurementValidator>? Validators { get; init; }

    /// <summary>
    /// Gets the aggregations computed over this measurement.
    /// </summary>
    public IReadOnlyList<object>? Aggregations { get; init; }

    /// <summary>
    /// Gets the multi-dimensional measurement data series.
    /// </summary>
    public IReadOnlyList<MeasurementDataSeries>? DataSeries { get; init; }
}

/// <summary>
/// Represents a data series entry for a measurement.
/// </summary>
public record MeasurementDataSeries
{
    /// <summary>
    /// Gets the data values.
    /// </summary>
    public IReadOnlyList<double>? Data { get; init; }

    /// <summary>
    /// Gets the units.
    /// </summary>
    public string? Units { get; init; }

    /// <summary>
    /// Gets the validators.
    /// </summary>
    public IReadOnlyList<object>? Validators { get; init; }

    /// <summary>
    /// Gets the aggregations.
    /// </summary>
    public IReadOnlyList<object>? Aggregations { get; init; }
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
    /// Gets the log ID.
    /// </summary>
    public string? Id { get; init; }

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
    /// Gets whether this attachment is a test report.
    /// </summary>
    public bool IsReport { get; init; }

    /// <summary>
    /// Gets the presigned download URL.
    /// </summary>
    public string? DownloadUrl { get; init; }
}

/// <summary>
/// Represents a sub-unit in a run response.
/// </summary>
public record RunSubUnit
{
    /// <summary>
    /// Gets the unit ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the serial number.
    /// </summary>
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets the part name.
    /// </summary>
    public string? PartName { get; init; }
}

/// <summary>
/// Represents the unit nested in a run response.
/// </summary>
public record RunUnit
{
    /// <summary>
    /// Gets the unit ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the serial number.
    /// </summary>
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets the part information.
    /// </summary>
    public RunUnitPart? Part { get; init; }

    /// <summary>
    /// Gets the batch information.
    /// </summary>
    public RunUnitBatch? Batch { get; init; }
}

/// <summary>
/// Represents the part nested in a run unit response.
/// </summary>
public record RunUnitPart
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
    public RunUnitPartRevision? Revision { get; init; }
}

/// <summary>
/// Represents the revision nested in a run unit part response.
/// </summary>
public record RunUnitPartRevision
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
/// Represents the batch nested in a run unit response.
/// </summary>
public record RunUnitBatch
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
/// Represents the procedure nested in a run response.
/// </summary>
public record RunProcedure
{
    /// <summary>
    /// Gets the procedure ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the procedure version.
    /// </summary>
    public RunProcedureVersion? Version { get; init; }
}

/// <summary>
/// Represents the version nested in a run procedure response.
/// </summary>
public record RunProcedureVersion
{
    /// <summary>
    /// Gets the version ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the version tag.
    /// </summary>
    public string? Tag { get; init; }
}
