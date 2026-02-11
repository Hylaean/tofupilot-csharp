namespace TofuPilot.Models.Common;

/// <summary>
/// Represents a paginated response from the API.
/// </summary>
/// <typeparam name="T">The type of items in the response.</typeparam>
public record PaginatedResponse<T>
{
    /// <summary>
    /// Gets the items in the current page.
    /// </summary>
    public required IReadOnlyList<T> Data { get; init; }

    /// <summary>
    /// Gets the pagination metadata.
    /// </summary>
    public PaginationMeta? Meta { get; init; }

    /// <summary>
    /// Gets the cursor for the next page, if available.
    /// </summary>
    public double? NextCursor => Meta?.NextCursor;

    /// <summary>
    /// Gets whether there are more items available.
    /// </summary>
    public bool HasMore => Meta?.HasMore ?? false;
}

/// <summary>
/// Pagination metadata from the API.
/// </summary>
public record PaginationMeta
{
    /// <summary>
    /// Gets whether there are more items available.
    /// </summary>
    public bool HasMore { get; init; }

    /// <summary>
    /// Gets the cursor for the next page.
    /// </summary>
    public double? NextCursor { get; init; }
}
