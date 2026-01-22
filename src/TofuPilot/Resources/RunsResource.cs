using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Runs;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing test runs.
/// </summary>
public sealed class RunsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/runs";

    /// <summary>
    /// Initializes a new instance of the <see cref="RunsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public RunsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Lists runs with optional filtering.
    /// </summary>
    /// <param name="request">The list request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of runs.</returns>
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
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created run.</returns>
    public async Task<Run> CreateAsync(CreateRunRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreateRunRequest, Run>(BasePath, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a run by ID.
    /// </summary>
    /// <param name="id">The run ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The run.</returns>
    public async Task<Run> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<Run>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a run.
    /// </summary>
    /// <param name="id">The run ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated run.</returns>
    public async Task<Run> UpdateAsync(string id, UpdateRunRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdateRunRequest, Run>($"{BasePath}/{id}", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes runs by IDs.
    /// </summary>
    /// <param name="ids">The run IDs to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> DeleteAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, object?>
        {
            ["ids"] = ids
        };
        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.DeleteAsync<DeleteResponse>(uri, cancellationToken).ConfigureAwait(false);
    }
}
