using TofuPilot.Models.Batches;
using TofuPilot.Models.Common;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing batches.
/// </summary>
public sealed class BatchesResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/batches";

    /// <summary>Lists batches with optional filtering.</summary>
    public async Task<PaginatedResponse<Batch>> ListAsync(
        ListBatchesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListBatchesRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["search_query"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Batch>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Creates a new batch.</summary>
    public Task<Batch> CreateAsync(CreateBatchRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreateBatchRequest, Batch>(BasePath, request, cancellationToken);

    /// <summary>Gets a batch by ID.</summary>
    public Task<Batch> GetAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<Batch>($"{BasePath}/{id}", cancellationToken);

    /// <summary>Updates a batch.</summary>
    public Task<Batch> UpdateAsync(string id, UpdateBatchRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdateBatchRequest, Batch>($"{BasePath}/{id}", request, cancellationToken);

    /// <summary>Deletes a batch by ID.</summary>
    public Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken);
}
