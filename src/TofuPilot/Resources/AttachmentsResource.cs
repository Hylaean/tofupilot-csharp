using TofuPilot.Http;
using TofuPilot.Models.Attachments;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing attachments.
/// </summary>
public sealed class AttachmentsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/attachments";

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public AttachmentsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Initializes an upload for an attachment.
    /// </summary>
    /// <param name="request">The initialize upload request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The upload initialization response with presigned URL.</returns>
    public async Task<InitializeUploadResponse> InitializeAsync(InitializeUploadRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<InitializeUploadRequest, InitializeUploadResponse>($"{BasePath}/initialize", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes an attachment.
    /// </summary>
    /// <param name="id">The attachment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteAttachmentResponse> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteAttachmentResponse>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }
}
