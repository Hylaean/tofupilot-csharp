namespace TofuPilot.Abstractions.Configuration;

/// <summary>
/// Configuration options for the TofuPilot SDK.
/// </summary>
public class TofuPilotOptions
{
    /// <summary>
    /// The configuration section name for binding.
    /// </summary>
    public const string SectionName = "TofuPilot";

    /// <summary>
    /// Gets or sets the API key for authentication.
    /// Can also be set via TOFUPILOT_API_KEY environment variable.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the base URL for the TofuPilot API.
    /// Defaults to https://www.tofupilot.com.
    /// Can also be set via TOFUPILOT_URL environment variable.
    /// </summary>
    public string BaseUrl { get; set; } = "https://www.tofupilot.com";

    /// <summary>
    /// Gets or sets the timeout for HTTP requests in seconds.
    /// Default is 30 seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Gets or sets the retry configuration.
    /// </summary>
    public RetryOptions Retry { get; set; } = new();
}
