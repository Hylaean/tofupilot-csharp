using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Parts;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing parts.
/// </summary>
public sealed class PartsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/parts";

    /// <summary>
    /// Gets the revisions sub-resource.
    /// </summary>
    public PartRevisionsResource Revisions { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PartsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public PartsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
        Revisions = new PartRevisionsResource(httpClient);
    }

    /// <summary>
    /// Lists parts with optional filtering.
    /// </summary>
    /// <param name="request">The list request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of parts.</returns>
    public async Task<PaginatedResponse<Part>> ListAsync(
        ListPartsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListPartsRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["searchQuery"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Part>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new part.
    /// </summary>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created part.</returns>
    public async Task<Part> CreateAsync(CreatePartRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreatePartRequest, Part>(BasePath, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a part by ID.
    /// </summary>
    /// <param name="id">The part ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The part.</returns>
    public async Task<Part> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<Part>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a part.
    /// </summary>
    /// <param name="id">The part ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated part.</returns>
    public async Task<Part> UpdateAsync(string id, UpdatePartRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdatePartRequest, Part>($"{BasePath}/{id}", request, cancellationToken).ConfigureAwait(false);
    }
}

/// <summary>
/// Resource for managing part revisions.
/// </summary>
public sealed class PartRevisionsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/parts";

    /// <summary>
    /// Initializes a new instance of the <see cref="PartRevisionsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public PartRevisionsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Lists revisions for a part.
    /// </summary>
    /// <param name="partId">The part ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of part revisions.</returns>
    public async Task<PaginatedResponse<PartRevision>> ListAsync(string partId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<PaginatedResponse<PartRevision>>($"{BasePath}/{partId}/revisions", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new revision for a part.
    /// </summary>
    /// <param name="partId">The part ID.</param>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created part revision.</returns>
    public async Task<PartRevision> CreateAsync(string partId, CreatePartRevisionRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreatePartRevisionRequest, PartRevision>($"{BasePath}/{partId}/revisions", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a part revision by ID.
    /// </summary>
    /// <param name="partId">The part ID.</param>
    /// <param name="revisionId">The revision ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The part revision.</returns>
    public async Task<PartRevision> GetAsync(string partId, string revisionId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<PartRevision>($"{BasePath}/{partId}/revisions/{revisionId}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a part revision.
    /// </summary>
    /// <param name="partId">The part ID.</param>
    /// <param name="revisionId">The revision ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated part revision.</returns>
    public async Task<PartRevision> UpdateAsync(string partId, string revisionId, UpdatePartRevisionRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdatePartRevisionRequest, PartRevision>($"{BasePath}/{partId}/revisions/{revisionId}", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a part revision.
    /// </summary>
    /// <param name="partId">The part ID.</param>
    /// <param name="revisionId">The revision ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> DeleteAsync(string partId, string revisionId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{partId}/revisions/{revisionId}", cancellationToken).ConfigureAwait(false);
    }
}
