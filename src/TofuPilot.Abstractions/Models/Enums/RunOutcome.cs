using System.Text.Json.Serialization;

namespace TofuPilot.Abstractions.Models;

/// <summary>
/// Represents the outcome of a test run.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<RunOutcome>))]
public enum RunOutcome
{
    /// <summary>
    /// Test is currently running.
    /// </summary>
    RUNNING,

    /// <summary>
    /// Test passed successfully.
    /// </summary>
    PASS,

    /// <summary>
    /// Test failed but script execution completed.
    /// </summary>
    FAIL,

    /// <summary>
    /// Script execution failed.
    /// </summary>
    ERROR,

    /// <summary>
    /// Test exceeded time limit.
    /// </summary>
    TIMEOUT,

    /// <summary>
    /// Test was manually aborted.
    /// </summary>
    ABORTED
}
