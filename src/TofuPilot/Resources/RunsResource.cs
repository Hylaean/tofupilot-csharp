using TofuPilot.Models.Common;
using TofuPilot.Models.Runs;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing test runs.
/// </summary>
public sealed class RunsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/runs";

    /// <summary>
    /// Lists runs with optional filtering.
    /// </summary>
    public async Task<PaginatedResponse<Run>> ListAsync(
        ListRunsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListRunsRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["search_query"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["outcomes"] = request.Outcomes?.Select(o => o.ToString()),
            ["procedure_ids"] = request.ProcedureIds,
            ["procedure_versions"] = request.ProcedureVersions,
            ["serial_numbers"] = request.SerialNumbers,
            ["part_numbers"] = request.PartNumbers,
            ["revision_numbers"] = request.RevisionNumbers,
            ["duration_min"] = request.DurationMin,
            ["duration_max"] = request.DurationMax,
            ["started_after"] = request.StartedAfter,
            ["started_before"] = request.StartedBefore,
            ["ended_after"] = request.EndedAfter,
            ["ended_before"] = request.EndedBefore,
            ["created_after"] = request.CreatedAfter,
            ["created_before"] = request.CreatedBefore,
            ["created_by_user_ids"] = request.CreatedByUserIds,
            ["created_by_station_ids"] = request.CreatedByStationIds,
            ["operated_by_ids"] = request.OperatedByIds,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
            ["sort_by"] = request.SortBy,
            ["sort_order"] = request.SortOrder,
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Run>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new run.
    /// </summary>
    public Task<Run> CreateAsync(CreateRunRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreateRunRequest, Run>(BasePath, request, cancellationToken);

    /// <summary>
    /// Gets a run by ID.
    /// </summary>
    public Task<Run> GetAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<Run>($"{BasePath}/{id}", cancellationToken);

    /// <summary>
    /// Updates a run.
    /// </summary>
    public Task<Run> UpdateAsync(string id, UpdateRunRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdateRunRequest, Run>($"{BasePath}/{id}", request, cancellationToken);

    /// <summary>
    /// Deletes runs by IDs.
    /// </summary>
    public Task<DeleteResponse> DeleteAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        var uri = BuildUriWithArrayParams(BasePath, new Dictionary<string, object?> { ["ids"] = ids });
        return HttpClient.DeleteAsync<DeleteResponse>(uri, cancellationToken);
    }
}
