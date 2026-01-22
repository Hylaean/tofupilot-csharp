using System.Web;

namespace TofuPilot.Resources;

/// <summary>
/// Base class for API resources.
/// </summary>
public abstract class ResourceBase(ITofuPilotHttpClient httpClient)
{
    /// <summary>
    /// Gets the HTTP client.
    /// </summary>
    protected ITofuPilotHttpClient HttpClient { get; } = httpClient;

    /// <summary>
    /// Gets the base path for this resource.
    /// </summary>
    protected abstract string BasePath { get; }

    /// <summary>
    /// Builds a URI with query parameters.
    /// </summary>
    protected static string BuildUri(string path, IDictionary<string, string?>? queryParams = null)
    {
        if (queryParams is not { Count: > 0 })
            return path;

        var sb = new StringBuilder(path);
        var first = true;

        foreach (var (key, value) in queryParams)
        {
            if (string.IsNullOrEmpty(value))
                continue;

            sb.Append(first ? '?' : '&');
            sb.Append(HttpUtility.UrlEncode(key));
            sb.Append('=');
            sb.Append(HttpUtility.UrlEncode(value));
            first = false;
        }

        return sb.ToString();
    }

    /// <summary>
    /// Builds a URI with array query parameters.
    /// </summary>
    protected static string BuildUriWithArrayParams(string path, IDictionary<string, object?>? queryParams = null)
    {
        if (queryParams is not { Count: > 0 })
            return path;

        var sb = new StringBuilder(path);
        var first = true;

        foreach (var (key, value) in queryParams)
        {
            switch (value)
            {
                case null:
                    continue;
                case IEnumerable<string> stringValues:
                    foreach (var item in stringValues)
                    {
                        AppendParam(sb, ref first, key, item);
                    }
                    break;
                case DateTimeOffset dto:
                    AppendParam(sb, ref first, key, dto.ToString("o"));
                    break;
                default:
                    var stringValue = value.ToString();
                    if (!string.IsNullOrEmpty(stringValue))
                        AppendParam(sb, ref first, key, stringValue);
                    break;
            }
        }

        return sb.ToString();
    }

    private static void AppendParam(StringBuilder sb, ref bool first, string key, string value)
    {
        sb.Append(first ? '?' : '&');
        sb.Append(HttpUtility.UrlEncode(key));
        sb.Append('=');
        sb.Append(HttpUtility.UrlEncode(value));
        first = false;
    }
}
