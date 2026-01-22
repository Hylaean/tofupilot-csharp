using System.Text.Json.Serialization;
using TofuPilot.Abstractions.Models;

namespace TofuPilot.V1.Models;

/// <summary>
/// Represents a log entry.
/// </summary>
public record Log
{
    /// <summary>
    /// Gets or sets the log level.
    /// </summary>
    [JsonPropertyName("level")]
    public required LogLevel Level { get; init; }

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public required string Timestamp { get; init; }

    /// <summary>
    /// Gets or sets the log message.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    /// <summary>
    /// Gets or sets the source file.
    /// </summary>
    [JsonPropertyName("source_file")]
    public required string SourceFile { get; init; }

    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    [JsonPropertyName("line_number")]
    public required int LineNumber { get; init; }
}
