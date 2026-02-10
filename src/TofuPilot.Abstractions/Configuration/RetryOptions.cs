namespace TofuPilot.Abstractions.Configuration;

/// <summary>
/// Configuration options for retry behavior.
/// </summary>
public class RetryOptions
{
    /// <summary>
    /// Gets or sets whether retry is enabled.
    /// Default is true.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// Default is 3.
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Gets or sets the initial delay between retries in milliseconds.
    /// Default is 1000ms (1 second).
    /// </summary>
    public int InitialDelayMs { get; set; } = 1000;

    /// <summary>
    /// Gets or sets the maximum delay between retries in milliseconds.
    /// Default is 30000ms (30 seconds).
    /// </summary>
    public int MaxDelayMs { get; set; } = 30000;

    /// <summary>
    /// Gets or sets the backoff multiplier for exponential backoff.
    /// Default is 2.0.
    /// </summary>
    public double BackoffMultiplier { get; set; } = 2.0;

    /// <summary>
    /// Gets or sets the HTTP status codes that should trigger a retry.
    /// Default includes 429, 500, 502, 503, 504.
    /// </summary>
    public int[] RetryableStatusCodes { get; set; } = [429, 500, 502, 503, 504];
}
