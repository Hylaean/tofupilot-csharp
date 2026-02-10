using System.Text.Json.Serialization;

namespace TofuPilot.Models.Attachments;

/// <summary>
/// Represents an initialized upload for an attachment.
/// </summary>
public record InitializeUploadResponse
{
    /// <summary>
    /// Gets the upload ID.
    /// </summary>
    [JsonPropertyName("uploadId")]
    public required string UploadId { get; init; }

    /// <summary>
    /// Gets the presigned URL for uploading.
    /// </summary>
    [JsonPropertyName("presignedUrl")]
    public required string PresignedUrl { get; init; }
}

/// <summary>
/// Request to initialize an upload.
/// </summary>
public record InitializeUploadRequest
{
    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    [JsonPropertyName("fileName")]
    public required string FileName { get; init; }

    /// <summary>
    /// Gets or sets the content type.
    /// </summary>
    [JsonPropertyName("contentType")]
    public required string ContentType { get; init; }

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    [JsonPropertyName("fileSize")]
    public required long FileSize { get; init; }
}

/// <summary>
/// Response for deleting an attachment.
/// </summary>
public record DeleteAttachmentResponse
{
    /// <summary>
    /// Gets whether the deletion was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }
}
