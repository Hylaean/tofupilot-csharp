using Hylaean.Tofupilot.Models.Attachments;
using Hylaean.Tofupilot.Models.Common;

namespace Hylaean.Tofupilot.Resources;

/// <summary>
/// Resource for managing attachments.
/// </summary>
public sealed class AttachmentsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    private static readonly HttpClient s_http = new();

    /// <inheritdoc/>
    protected override string BasePath => "v2/attachments";

    /// <summary>
    /// Uploads a file in one call (initialize → PUT to S3 → finalize).
    /// Returns the attachment ID, ready to be linked to a run via <c>Runs.UpdateAsync</c>.
    /// </summary>
    public async Task<string> UploadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fileName = Path.GetFileName(filePath);
        using var stream = File.OpenRead(filePath);
        return await UploadAsync(stream, fileName, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Uploads a stream in one call (initialize → PUT to S3 → finalize).
    /// Returns the attachment ID, ready to be linked to a run via <c>Runs.UpdateAsync</c>.
    /// </summary>
    public async Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        var init = await InitializeAsync(new InitializeUploadRequest { FileName = fileName }, cancellationToken).ConfigureAwait(false);

        using var content = new StreamContent(stream);
        using var response = await s_http.PutAsync(init.UploadUrl, content, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        await FinalizeAsync(init.Id!, cancellationToken).ConfigureAwait(false);
        return init.Id!;
    }

    /// <summary>
    /// Downloads a file from a signed URL to a local path.
    /// </summary>
    public static async Task DownloadAsync(string url, string localPath, CancellationToken cancellationToken = default)
    {
        using var response = await s_http.GetAsync(url, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        using var fileStream = File.Create(localPath);
        await response.Content.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
    }

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
