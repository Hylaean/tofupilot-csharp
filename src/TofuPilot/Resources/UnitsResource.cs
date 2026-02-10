using TofuPilot.Models.Common;
using TofuPilot.Models.Units;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing units.
/// </summary>
public sealed class UnitsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/units";

    /// <summary>
    /// Lists units with optional filtering.
    /// </summary>
    public async Task<PaginatedResponse<Unit>> ListAsync(
        ListUnitsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListUnitsRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["searchQuery"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["serialNumbers"] = request.SerialNumbers,
            ["partNumbers"] = request.PartNumbers,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Unit>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new unit.
    /// </summary>
    public Task<Unit> CreateAsync(CreateUnitRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreateUnitRequest, Unit>(BasePath, request, cancellationToken);

    /// <summary>
    /// Gets a unit by ID.
    /// </summary>
    public Task<Unit> GetAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<Unit>($"{BasePath}/{id}", cancellationToken);

    /// <summary>
    /// Updates a unit.
    /// </summary>
    public Task<Unit> UpdateAsync(string id, UpdateUnitRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdateUnitRequest, Unit>($"{BasePath}/{id}", request, cancellationToken);

    /// <summary>
    /// Deletes a unit by ID.
    /// </summary>
    public Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken);

    /// <summary>
    /// Adds a child unit to a parent unit.
    /// </summary>
    public Task<Unit> AddChildAsync(string parentId, string childId, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<object, Unit>($"{BasePath}/{parentId}/children/{childId}", new { }, cancellationToken);

    /// <summary>
    /// Removes a child unit from a parent unit.
    /// </summary>
    public Task<Unit> RemoveChildAsync(string parentId, string childId, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<Unit>($"{BasePath}/{parentId}/children/{childId}", cancellationToken);
}
