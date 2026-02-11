using System.Text.Json.Serialization;

namespace TofuPilot.Models.Attachments;

/// <summary>
/// Represents an initialized upload for an attachment.
/// </summary>
public record InitializeUploadResponse
{
    /// <summary>
    /// Gets the attachment ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the presigned URL for uploading.
    /// </summary>
    public string? UploadUrl { get; init; }
}

/// <summary>
/// Request to initialize an upload.
/// </summary>
public record InitializeUploadRequest
{
    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string FileName { get; init; }

    /// <summary>
    /// Gets or sets the content type.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentType { get; init; }

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? FileSize { get; init; }
}

/// <summary>
/// Response for deleting an attachment.
/// </summary>
public record DeleteAttachmentResponse
{
    /// <summary>
    /// Gets whether the deletion was successful.
    /// </summary>
    public bool Success { get; init; }
}
