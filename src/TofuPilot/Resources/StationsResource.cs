using TofuPilot.Models.Common;
using TofuPilot.Models.Stations;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing stations.
/// </summary>
public sealed class StationsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/stations";

    /// <summary>Lists stations with optional filtering.</summary>
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

    /// <summary>Creates a new station.</summary>
    public Task<Station> CreateAsync(CreateStationRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreateStationRequest, Station>(BasePath, request, cancellationToken);

    /// <summary>Gets a station by ID.</summary>
    public Task<Station> GetAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<Station>($"{BasePath}/{id}", cancellationToken);

    /// <summary>Updates a station.</summary>
    public Task<Station> UpdateAsync(string id, UpdateStationRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdateStationRequest, Station>($"{BasePath}/{id}", request, cancellationToken);

    /// <summary>Removes (deletes) a station by ID.</summary>
    public Task<DeleteResponse> RemoveAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken);

    /// <summary>Links a procedure to a station.</summary>
    public Task<Station> LinkProcedureAsync(string stationId, string procedureId, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<LinkProcedureRequest, Station>($"{BasePath}/{stationId}/procedures", new() { ProcedureId = procedureId }, cancellationToken);

    /// <summary>Unlinks a procedure from a station.</summary>
    public Task<Station> UnlinkProcedureAsync(string stationId, string procedureId, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<Station>($"{BasePath}/{stationId}/procedures/{procedureId}", cancellationToken);
}
