using System.Text.Json.Serialization;

namespace TofuPilot.Abstractions.Models;

/// <summary>
/// Represents the outcome of a measurement.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MeasurementOutcome>))]
public enum MeasurementOutcome
{
    /// <summary>
    /// Measurement passed within limits.
    /// </summary>
    PASS,

    /// <summary>
    /// Measurement failed (out of limits).
    /// </summary>
    FAIL,

    /// <summary>
    /// Measurement outcome not set.
    /// </summary>
    UNSET
}
