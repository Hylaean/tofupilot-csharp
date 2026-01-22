using System.Text.Json.Serialization;
using TofuPilot.Abstractions.Models;

namespace TofuPilot.V1.Models;

/// <summary>
/// Represents a test phase.
/// </summary>
public record Phase
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
    [JsonPropertyName("start_time_millis")]
    public required long StartTimeMillis { get; init; }

    /// <summary>
    /// Gets or sets the end time in milliseconds.
    /// </summary>
    [JsonPropertyName("end_time_millis")]
    public required long EndTimeMillis { get; init; }

    /// <summary>
    /// Gets or sets the measurements in this phase.
    /// </summary>
    [JsonPropertyName("measurements")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<Measurement>? Measurements { get; init; }

    /// <summary>
    /// Gets or sets the docstring for this phase.
    /// </summary>
    [JsonPropertyName("docstring")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Docstring { get; init; }
}
