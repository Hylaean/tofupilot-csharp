using System.Text.Json.Serialization;

namespace TofuPilot.Abstractions.Models;

/// <summary>
/// Represents the outcome of a test phase.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<PhaseOutcome>))]
public enum PhaseOutcome
{
    /// <summary>
    /// Phase passed successfully.
    /// </summary>
    PASS,

    /// <summary>
    /// Phase failed.
    /// </summary>
    FAIL,

    /// <summary>
    /// Phase was skipped.
    /// </summary>
    SKIP,

    /// <summary>
    /// Phase encountered an error.
    /// </summary>
    ERROR
}
