using System.Text.Json.Serialization;

namespace TofuPilot.Models.Runs;

/// <summary>
/// Request to update a run.
/// </summary>
public record UpdateRunRequest
{
    /// <summary>
    /// Gets or sets the attachment IDs to add to the run.
    /// </summary>
    [JsonPropertyName("attachments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Attachments { get; init; }
}
