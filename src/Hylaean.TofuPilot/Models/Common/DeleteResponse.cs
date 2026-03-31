namespace Hylaean.TofuPilot.Models.Common;

/// <summary>
/// Response for a single-resource delete operation.
/// </summary>
public record DeleteResponse
{
    /// <summary>
    /// Gets the ID of the deleted resource.
    /// </summary>
    public string? Id { get; init; }
}

/// <summary>
/// Response for a bulk delete operation where the API returns an array of IDs.
/// </summary>
public record BulkDeleteResponse
{
    /// <summary>
    /// Gets the IDs of deleted resources.
    /// </summary>
    public IReadOnlyList<string>? Id { get; init; }
}

/// <summary>
/// Response for deleting a part (includes cascade-deleted revision IDs).
/// </summary>
public record DeletePartResponse
{
    /// <summary>
    /// Gets the ID of the deleted part.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the IDs of revisions that were deleted with the part.
    /// </summary>
    public IReadOnlyList<string>? DeletedRevisionIds { get; init; }
}
