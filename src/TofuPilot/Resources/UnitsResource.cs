using TofuPilot.Models.Common;
using TofuPilot.Models.Units;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing units.
/// </summary>
public sealed class UnitsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/units";

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
            ["search_query"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["serial_numbers"] = request.SerialNumbers,
            ["part_numbers"] = request.PartNumbers,
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
    /// Deletes units by serial numbers.
    /// </summary>
    public Task<DeleteResponse> DeleteAsync(IEnumerable<string> serialNumbers, CancellationToken cancellationToken = default)
    {
        var uri = BuildUriWithArrayParams(BasePath, new Dictionary<string, object?> { ["serial_numbers"] = serialNumbers });
        return HttpClient.DeleteAsync<DeleteResponse>(uri, cancellationToken);
    }

    /// <summary>
    /// Deletes a single unit by serial number.
    /// </summary>
    public Task<DeleteResponse> DeleteAsync(string serialNumber, CancellationToken cancellationToken = default) =>
        DeleteAsync(new[] { serialNumber }, cancellationToken);

    /// <summary>
    /// Adds a child unit to a parent unit.
    /// </summary>
    public Task<Unit> AddChildAsync(string parentSerialNumber, string childSerialNumber, CancellationToken cancellationToken = default) =>
        HttpClient.PutAsync<AddChildRequest, Unit>($"{BasePath}/{parentSerialNumber}/children", new AddChildRequest { ChildSerialNumber = childSerialNumber }, cancellationToken);

    /// <summary>
    /// Removes a child unit from a parent unit.
    /// </summary>
    public Task<Unit> RemoveChildAsync(string parentSerialNumber, string childSerialNumber, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<Unit>($"{BasePath}/{parentSerialNumber}/children?child_serial_number={Uri.EscapeDataString(childSerialNumber)}", cancellationToken);
}
