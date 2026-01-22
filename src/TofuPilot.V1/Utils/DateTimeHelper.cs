using System.Xml;

namespace TofuPilot.V1.Utils;

/// <summary>
/// Utility class for date/time operations.
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// Converts a DateTime to ISO 8601 format.
    /// </summary>
    /// <param name="dateTime">The datetime to convert.</param>
    /// <returns>ISO 8601 formatted string.</returns>
    public static string ToIso8601(DateTime dateTime)
    {
        return dateTime.ToString("o");
    }

    /// <summary>
    /// Converts a DateTimeOffset to ISO 8601 format.
    /// </summary>
    /// <param name="dateTimeOffset">The datetime offset to convert.</param>
    /// <returns>ISO 8601 formatted string.</returns>
    public static string ToIso8601(DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToString("o");
    }

    /// <summary>
    /// Converts a TimeSpan to ISO 8601 duration format.
    /// </summary>
    /// <param name="duration">The duration to convert.</param>
    /// <returns>ISO 8601 duration formatted string.</returns>
    public static string ToIso8601Duration(TimeSpan duration)
    {
        return XmlConvert.ToString(duration);
    }

    /// <summary>
    /// Converts milliseconds since epoch to DateTimeOffset.
    /// </summary>
    /// <param name="milliseconds">Milliseconds since Unix epoch.</param>
    /// <returns>DateTimeOffset representation.</returns>
    public static DateTimeOffset FromMilliseconds(long milliseconds)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
    }

    /// <summary>
    /// Converts DateTimeOffset to milliseconds since epoch.
    /// </summary>
    /// <param name="dateTimeOffset">The datetime offset to convert.</param>
    /// <returns>Milliseconds since Unix epoch.</returns>
    public static long ToMilliseconds(DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToUnixTimeMilliseconds();
    }
}
