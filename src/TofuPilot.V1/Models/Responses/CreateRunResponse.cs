using System.Text.Json.Serialization;

namespace TofuPilot.V1.Models.Responses;

/// <summary>
/// Response from creating a run.
/// </summary>
public record CreateRunResponse
{
    /// <summary>
    /// Gets or sets whether the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// Gets or sets the run ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Gets or sets the URL to view the run.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Gets or sets the error information.
    /// </summary>
    [JsonPropertyName("error")]
    public ErrorInfo? Error { get; init; }
}

/// <summary>
/// Error information in a response.
/// </summary>
public record ErrorInfo
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
