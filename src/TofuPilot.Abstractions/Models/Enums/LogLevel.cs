using System.Text.Json.Serialization;

namespace TofuPilot.Abstractions.Models;

/// <summary>
/// Represents the severity level of a log entry.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<LogLevel>))]
public enum LogLevel
{
    /// <summary>
    /// Debug level logging.
    /// </summary>
    DEBUG,

    /// <summary>
    /// Informational logging.
    /// </summary>
    INFO,

    /// <summary>
    /// Warning level logging.
    /// </summary>
    WARNING,

    /// <summary>
    /// Error level logging.
    /// </summary>
    ERROR,

    /// <summary>
    /// Critical error logging.
    /// </summary>
    CRITICAL
}
