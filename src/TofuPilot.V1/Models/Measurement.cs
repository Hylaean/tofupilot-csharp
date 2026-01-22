using System.Text.Json.Serialization;
using TofuPilot.Abstractions.Models;

namespace TofuPilot.V1.Models;

/// <summary>
/// Represents a measurement within a phase.
/// </summary>
public record Measurement
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
    [JsonPropertyName("measured_value")]
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
    [JsonPropertyName("lower_limit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? LowerLimit { get; init; }

    /// <summary>
    /// Gets or sets the upper limit.
    /// </summary>
    [JsonPropertyName("upper_limit")]
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
