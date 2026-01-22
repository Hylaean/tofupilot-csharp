using System.Text.Json.Serialization;

namespace TofuPilot.V1.Models;

/// <summary>
/// Represents a sub-unit within a main unit.
/// </summary>
public record SubUnit
{
    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    [JsonPropertyName("serial_number")]
    public required string SerialNumber { get; init; }
}
