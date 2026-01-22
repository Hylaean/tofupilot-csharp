using System.Text.Json.Serialization;

namespace TofuPilot.V1.Models.Responses;

/// <summary>
/// Response from getting runs.
/// </summary>
public record GetRunsResponse
{
    /// <summary>
    /// Gets or sets whether the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// Gets or sets the runs data.
    /// </summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<RunData>? Data { get; init; }

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
/// Run data returned from the API.
/// </summary>
public record RunData
{
    /// <summary>
    /// Gets or sets the run ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Gets or sets the procedure name.
    /// </summary>
    [JsonPropertyName("procedure_name")]
    public string? ProcedureName { get; init; }

    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    [JsonPropertyName("serial_number")]
    public string? SerialNumber { get; init; }

    /// <summary>
    /// Gets or sets whether the run passed.
    /// </summary>
    [JsonPropertyName("run_passed")]
    public bool? RunPassed { get; init; }

    /// <summary>
    /// Gets or sets when the run was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the URL to view the run.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}
