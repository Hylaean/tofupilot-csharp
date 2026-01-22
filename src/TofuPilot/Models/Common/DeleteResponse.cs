using System.Text.Json.Serialization;

namespace TofuPilot.Models.Common;

/// <summary>
/// Represents a delete operation response.
/// </summary>
public record DeleteResponse
{
    /// <summary>
    /// Gets the number of items deleted.
    /// </summary>
    [JsonPropertyName("deleted")]
    public int Deleted { get; init; }
}
