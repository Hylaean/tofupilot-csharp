using TofuPilot.Models.Common;
using TofuPilot.Models.Parts;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing parts.
/// </summary>
public sealed class PartsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/parts";

    /// <summary>Gets the revisions sub-resource.</summary>
    public PartRevisionsResource Revisions { get; } = new(httpClient);

    /// <summary>Lists parts with optional filtering.</summary>
    public async Task<PaginatedResponse<Part>> ListAsync(
        ListPartsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListPartsRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["search_query"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Part>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Creates a new part.</summary>
    public Task<Part> CreateAsync(CreatePartRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreatePartRequest, Part>(BasePath, request, cancellationToken);

    /// <summary>Gets a part by ID.</summary>
    public Task<Part> GetAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<Part>($"{BasePath}/{id}", cancellationToken);

    /// <summary>Updates a part.</summary>
    public Task<Part> UpdateAsync(string id, UpdatePartRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdatePartRequest, Part>($"{BasePath}/{id}", request, cancellationToken);

    /// <summary>Deletes a part by part number.</summary>
    public Task<DeleteResponse> DeleteAsync(string partNumber, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{partNumber}", cancellationToken);
}

/// <summary>
/// Resource for managing part revisions.
/// </summary>
public sealed class PartRevisionsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/parts";

    /// <summary>Lists revisions for a part.</summary>
    public Task<PaginatedResponse<PartRevision>> ListAsync(string partNumber, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<PaginatedResponse<PartRevision>>($"{BasePath}/{partNumber}/revisions", cancellationToken);

    /// <summary>Creates a new revision for a part.</summary>
    public Task<PartRevision> CreateAsync(string partNumber, CreatePartRevisionRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreatePartRevisionRequest, PartRevision>($"{BasePath}/{partNumber}/revisions", request, cancellationToken);

    /// <summary>Gets a part revision by number.</summary>
    public Task<PartRevision> GetAsync(string partNumber, string revisionNumber, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<PartRevision>($"{BasePath}/{partNumber}/revisions/{revisionNumber}", cancellationToken);

    /// <summary>Updates a part revision.</summary>
    public Task<PartRevision> UpdateAsync(string partNumber, string revisionNumber, UpdatePartRevisionRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdatePartRevisionRequest, PartRevision>($"{BasePath}/{partNumber}/revisions/{revisionNumber}", request, cancellationToken);

    /// <summary>Deletes a part revision.</summary>
    public Task<DeleteResponse> DeleteAsync(string partNumber, string revisionNumber, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{partNumber}/revisions/{revisionNumber}", cancellationToken);
}
