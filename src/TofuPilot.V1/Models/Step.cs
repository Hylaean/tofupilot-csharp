using System.Text.Json.Serialization;

namespace TofuPilot.V1.Models;

/// <summary>
/// Represents a test step (deprecated - use Phase instead).
/// </summary>
[Obsolete("Use Phase instead. Steps are deprecated in the TofuPilot API.")]
public record Step
{
    /// <summary>
    /// Gets or sets the step name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets when the step started.
    /// </summary>
    [JsonPropertyName("started_at")]
    public required string StartedAt { get; init; }

    /// <summary>
    /// Gets or sets the duration in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("duration")]
    public required string Duration { get; init; }

    /// <summary>
    /// Gets or sets whether the step passed.
    /// </summary>
    [JsonPropertyName("step_passed")]
    public required bool StepPassed { get; init; }

    /// <summary>
    /// Gets or sets the measurement unit.
    /// </summary>
    [JsonPropertyName("measurement_unit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MeasurementUnit { get; init; }

    /// <summary>
    /// Gets or sets the measurement value.
    /// </summary>
    [JsonPropertyName("measurement_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? MeasurementValue { get; init; }

    /// <summary>
    /// Gets or sets the lower limit.
    /// </summary>
    [JsonPropertyName("limit_low")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? LimitLow { get; init; }

    /// <summary>
    /// Gets or sets the upper limit.
    /// </summary>
    [JsonPropertyName("limit_high")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? LimitHigh { get; init; }
}
