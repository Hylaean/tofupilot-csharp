using System.Text.Json.Serialization;

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
    [JsonPropertyName("data")]
    public required IReadOnlyList<T> Data { get; init; }

    /// <summary>
    /// Gets the cursor for the next page, if available.
    /// </summary>
    [JsonPropertyName("nextCursor")]
    public double? NextCursor { get; init; }

    /// <summary>
    /// Gets whether there are more items available.
    /// </summary>
    [JsonPropertyName("hasMore")]
    public bool HasMore { get; init; }
}
