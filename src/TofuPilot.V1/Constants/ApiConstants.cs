namespace TofuPilot.V1.Constants;

/// <summary>
/// API constants for the V1 API.
/// </summary>
public static class ApiConstants
{
    /// <summary>
    /// The default endpoint for the TofuPilot API.
    /// </summary>
    public const string DefaultEndpoint = "https://www.tofupilot.com";

    /// <summary>
    /// The maximum file size for attachments in bytes (50 MB).
    /// </summary>
    public const long MaxFileSize = 50 * 1024 * 1024;

    /// <summary>
    /// The maximum number of attachments per run.
    /// </summary>
    public const int MaxAttachments = 20;

    /// <summary>
    /// The API version path.
    /// </summary>
    public const string ApiPath = "/api/v1";
}
