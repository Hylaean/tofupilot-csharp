using System.Text.Json;
using System.Text.Json.Serialization;

namespace TofuPilot.Serialization;

/// <summary>
/// Converts DateTimeOffset to/from UTC ISO 8601 strings with 'Z' suffix.
/// The TofuPilot API requires the 'Z' suffix and does not accept '+00:00'.
/// </summary>
public sealed class UtcDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    /// <inheritdoc/>
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetDateTimeOffset();

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
}
