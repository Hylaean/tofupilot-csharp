using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Units;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing units.
/// </summary>
public sealed class UnitsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/units";

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public UnitsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Lists units with optional filtering.
    /// </summary>
    /// <param name="request">The list request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of units.</returns>
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
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created unit.</returns>
    public async Task<Unit> CreateAsync(CreateUnitRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreateUnitRequest, Unit>(BasePath, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a unit by ID.
    /// </summary>
    /// <param name="id">The unit ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The unit.</returns>
    public async Task<Unit> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<Unit>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a unit.
    /// </summary>
    /// <param name="id">The unit ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated unit.</returns>
    public async Task<Unit> UpdateAsync(string id, UpdateUnitRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdateUnitRequest, Unit>($"{BasePath}/{id}", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a unit by ID.
    /// </summary>
    /// <param name="id">The unit ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Adds a child unit to a parent unit.
    /// </summary>
    /// <param name="parentId">The parent unit ID.</param>
    /// <param name="childId">The child unit ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated parent unit.</returns>
    public async Task<Unit> AddChildAsync(string parentId, string childId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<object, Unit>($"{BasePath}/{parentId}/children/{childId}", new { }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Removes a child unit from a parent unit.
    /// </summary>
    /// <param name="parentId">The parent unit ID.</param>
    /// <param name="childId">The child unit ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated parent unit.</returns>
    public async Task<Unit> RemoveChildAsync(string parentId, string childId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<Unit>($"{BasePath}/{parentId}/children/{childId}", cancellationToken).ConfigureAwait(false);
    }
}
