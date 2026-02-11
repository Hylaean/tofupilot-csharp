using TofuPilot.Models.Attachments;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing attachments.
/// </summary>
public sealed class AttachmentsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/attachments";

    /// <summary>Initializes an upload for an attachment.</summary>
    public Task<InitializeUploadResponse> InitializeAsync(InitializeUploadRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<InitializeUploadRequest, InitializeUploadResponse>(BasePath, request, cancellationToken);

    /// <summary>Deletes an attachment.</summary>
    public Task<DeleteAttachmentResponse> DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteAttachmentResponse>($"{BasePath}/{id}", cancellationToken);
}
