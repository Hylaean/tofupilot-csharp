using TofuPilot.Http;
using TofuPilot.Models.Batches;
using TofuPilot.Models.Common;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing batches.
/// </summary>
public sealed class BatchesResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/batches";

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchesResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public BatchesResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Lists batches with optional filtering.
    /// </summary>
    /// <param name="request">The list request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of batches.</returns>
    public async Task<PaginatedResponse<Batch>> ListAsync(
        ListBatchesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListBatchesRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["searchQuery"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Batch>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new batch.
    /// </summary>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created batch.</returns>
    public async Task<Batch> CreateAsync(CreateBatchRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreateBatchRequest, Batch>(BasePath, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a batch by ID.
    /// </summary>
    /// <param name="id">The batch ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The batch.</returns>
    public async Task<Batch> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<Batch>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a batch.
    /// </summary>
    /// <param name="id">The batch ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated batch.</returns>
    public async Task<Batch> UpdateAsync(string id, UpdateBatchRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdateBatchRequest, Batch>($"{BasePath}/{id}", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a batch by ID.
    /// </summary>
    /// <param name="id">The batch ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }
}
