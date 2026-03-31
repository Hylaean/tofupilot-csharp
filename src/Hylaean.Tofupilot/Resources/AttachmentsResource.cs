using Hylaean.Tofupilot.Models.Attachments;
using Hylaean.Tofupilot.Models.Common;

namespace Hylaean.Tofupilot.Resources;

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

    /// <summary>Finalizes an attachment upload.</summary>
    public Task<FinalizeUploadResponse> FinalizeAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<object, FinalizeUploadResponse>($"{BasePath}/{id}/finalize", new { }, cancellationToken);

    /// <summary>Deletes attachments by IDs.</summary>
    public Task<BulkDeleteResponse> DeleteAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        var uri = BuildUriWithArrayParams(BasePath, new Dictionary<string, object?> { ["ids"] = ids });
        return HttpClient.DeleteAsync<BulkDeleteResponse>(uri, cancellationToken);
    }

    /// <summary>Deletes a single attachment by ID.</summary>
    public Task<BulkDeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        DeleteAsync(new[] { id }, cancellationToken);
}
