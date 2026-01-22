using TofuPilot.Models.Common;
using TofuPilot.Models.Runs;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing test runs.
/// </summary>
public sealed class RunsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/runs";

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
            ["searchQuery"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["outcomes"] = request.Outcomes?.Select(o => o.ToString()),
            ["procedureIds"] = request.ProcedureIds,
            ["procedureVersions"] = request.ProcedureVersions,
            ["serialNumbers"] = request.SerialNumbers,
            ["partNumbers"] = request.PartNumbers,
            ["revisionNumbers"] = request.RevisionNumbers,
            ["durationMin"] = request.DurationMin,
            ["durationMax"] = request.DurationMax,
            ["startedAfter"] = request.StartedAfter,
            ["startedBefore"] = request.StartedBefore,
            ["endedAfter"] = request.EndedAfter,
            ["endedBefore"] = request.EndedBefore,
            ["createdAfter"] = request.CreatedAfter,
            ["createdBefore"] = request.CreatedBefore,
            ["createdByUserIds"] = request.CreatedByUserIds,
            ["createdByStationIds"] = request.CreatedByStationIds,
            ["operatedByIds"] = request.OperatedByIds,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
            ["sortBy"] = request.SortBy,
            ["sortOrder"] = request.SortOrder,
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
