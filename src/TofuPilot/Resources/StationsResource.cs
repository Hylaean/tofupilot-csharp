using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Stations;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing stations.
/// </summary>
public sealed class StationsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/stations";

    /// <summary>
    /// Initializes a new instance of the <see cref="StationsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public StationsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Lists stations with optional filtering.
    /// </summary>
    /// <param name="request">The list request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of stations.</returns>
    public async Task<PaginatedResponse<Station>> ListAsync(
        ListStationsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListStationsRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["searchQuery"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Station>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new station.
    /// </summary>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created station.</returns>
    public async Task<Station> CreateAsync(CreateStationRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreateStationRequest, Station>(BasePath, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a station by ID.
    /// </summary>
    /// <param name="id">The station ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The station.</returns>
    public async Task<Station> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<Station>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a station.
    /// </summary>
    /// <param name="id">The station ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated station.</returns>
    public async Task<Station> UpdateAsync(string id, UpdateStationRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdateStationRequest, Station>($"{BasePath}/{id}", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Removes (deletes) a station by ID.
    /// </summary>
    /// <param name="id">The station ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Links a procedure to a station.
    /// </summary>
    /// <param name="stationId">The station ID.</param>
    /// <param name="procedureId">The procedure ID to link.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated station.</returns>
    public async Task<Station> LinkProcedureAsync(string stationId, string procedureId, CancellationToken cancellationToken = default)
    {
        var request = new LinkProcedureRequest { ProcedureId = procedureId };
        return await HttpClient.PostAsync<LinkProcedureRequest, Station>($"{BasePath}/{stationId}/procedures", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Unlinks a procedure from a station.
    /// </summary>
    /// <param name="stationId">The station ID.</param>
    /// <param name="procedureId">The procedure ID to unlink.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated station.</returns>
    public async Task<Station> UnlinkProcedureAsync(string stationId, string procedureId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<Station>($"{BasePath}/{stationId}/procedures/{procedureId}", cancellationToken).ConfigureAwait(false);
    }
}
