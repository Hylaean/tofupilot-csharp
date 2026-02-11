namespace TofuPilot.Models.Common;

/// <summary>
/// Represents a delete operation response.
/// </summary>
public record DeleteResponse
{
    /// <summary>
    /// Gets the ID of the deleted resource.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the IDs of deleted resources (for bulk delete).
    /// </summary>
    public IReadOnlyList<string>? Ids { get; init; }
}
