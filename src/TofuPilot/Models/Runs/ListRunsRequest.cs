using TofuPilot.Abstractions.Models;

namespace TofuPilot.Models.Runs;

/// <summary>
/// Request parameters for listing runs.
/// </summary>
public record ListRunsRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific run IDs to filter by.
    /// </summary>
    public IReadOnlyList<string>? Ids { get; init; }

    /// <summary>
    /// Gets or sets the outcomes to filter by.
    /// </summary>
    public IReadOnlyList<RunOutcome>? Outcomes { get; init; }

    /// <summary>
    /// Gets or sets the procedure IDs to filter by.
    /// </summary>
    public IReadOnlyList<string>? ProcedureIds { get; init; }

    /// <summary>
    /// Gets or sets the procedure versions to filter by.
    /// </summary>
    public IReadOnlyList<string>? ProcedureVersions { get; init; }

    /// <summary>
    /// Gets or sets the serial numbers to filter by.
    /// </summary>
    public IReadOnlyList<string>? SerialNumbers { get; init; }

    /// <summary>
    /// Gets or sets the part numbers to filter by.
    /// </summary>
    public IReadOnlyList<string>? PartNumbers { get; init; }

    /// <summary>
    /// Gets or sets the revision numbers to filter by.
    /// </summary>
    public IReadOnlyList<string>? RevisionNumbers { get; init; }

    /// <summary>
    /// Gets or sets the minimum duration filter.
    /// </summary>
    public string? DurationMin { get; init; }

    /// <summary>
    /// Gets or sets the maximum duration filter.
    /// </summary>
    public string? DurationMax { get; init; }

    /// <summary>
    /// Gets or sets the started after filter.
    /// </summary>
    public DateTimeOffset? StartedAfter { get; init; }

    /// <summary>
    /// Gets or sets the started before filter.
    /// </summary>
    public DateTimeOffset? StartedBefore { get; init; }

    /// <summary>
    /// Gets or sets the ended after filter.
    /// </summary>
    public DateTimeOffset? EndedAfter { get; init; }

    /// <summary>
    /// Gets or sets the ended before filter.
    /// </summary>
    public DateTimeOffset? EndedBefore { get; init; }

    /// <summary>
    /// Gets or sets the created after filter.
    /// </summary>
    public DateTimeOffset? CreatedAfter { get; init; }

    /// <summary>
    /// Gets or sets the created before filter.
    /// </summary>
    public DateTimeOffset? CreatedBefore { get; init; }

    /// <summary>
    /// Gets or sets the user IDs who created the runs.
    /// </summary>
    public IReadOnlyList<string>? CreatedByUserIds { get; init; }

    /// <summary>
    /// Gets or sets the station IDs that created the runs.
    /// </summary>
    public IReadOnlyList<string>? CreatedByStationIds { get; init; }

    /// <summary>
    /// Gets or sets the operator IDs.
    /// </summary>
    public IReadOnlyList<string>? OperatedByIds { get; init; }

    /// <summary>
    /// Gets or sets the page size limit.
    /// </summary>
    public int? Limit { get; init; } = 50;

    /// <summary>
    /// Gets or sets the cursor for pagination.
    /// </summary>
    public double? Cursor { get; init; }

    /// <summary>
    /// Gets or sets the field to sort by.
    /// </summary>
    public string? SortBy { get; init; } = "started_at";

    /// <summary>
    /// Gets or sets the sort order.
    /// </summary>
    public string? SortOrder { get; init; } = "desc";
}
