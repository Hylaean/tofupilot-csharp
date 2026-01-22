using System.Text.Json.Serialization;

namespace TofuPilot.V1.Models;

/// <summary>
/// Represents a unit under test.
/// </summary>
public record UnitUnderTest
{
    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    [JsonPropertyName("serial_number")]
    public required string SerialNumber { get; init; }

    /// <summary>
    /// Gets or sets the part number.
    /// </summary>
    [JsonPropertyName("part_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    [JsonPropertyName("part_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartName { get; init; }

    /// <summary>
    /// Gets or sets the revision.
    /// </summary>
    [JsonPropertyName("revision")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Revision { get; init; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    [JsonPropertyName("batch_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BatchNumber { get; init; }
}
