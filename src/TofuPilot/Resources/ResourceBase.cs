using System.Text;
using System.Web;
using TofuPilot.Http;

namespace TofuPilot.Resources;

/// <summary>
/// Base class for API resources.
/// </summary>
public abstract class ResourceBase
{
    /// <summary>
    /// Gets the HTTP client.
    /// </summary>
    protected ITofuPilotHttpClient HttpClient { get; }

    /// <summary>
    /// Gets the base path for this resource.
    /// </summary>
    protected abstract string BasePath { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceBase"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    protected ResourceBase(ITofuPilotHttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    /// <summary>
    /// Builds a URI with query parameters.
    /// </summary>
    /// <param name="path">The base path.</param>
    /// <param name="queryParams">The query parameters.</param>
    /// <returns>The URI with query parameters.</returns>
    protected static string BuildUri(string path, IDictionary<string, string?>? queryParams = null)
    {
        if (queryParams == null || queryParams.Count == 0)
        {
            return path;
        }

        var sb = new StringBuilder(path);
        var first = true;

        foreach (var (key, value) in queryParams)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }

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
    /// <param name="path">The base path.</param>
    /// <param name="queryParams">The query parameters.</param>
    /// <returns>The URI with query parameters.</returns>
    protected static string BuildUriWithArrayParams(string path, IDictionary<string, object?>? queryParams = null)
    {
        if (queryParams == null || queryParams.Count == 0)
        {
            return path;
        }

        var sb = new StringBuilder(path);
        var first = true;

        foreach (var (key, value) in queryParams)
        {
            if (value == null)
            {
                continue;
            }

            if (value is IEnumerable<string> stringValues)
            {
                foreach (var item in stringValues)
                {
                    sb.Append(first ? '?' : '&');
                    sb.Append(HttpUtility.UrlEncode(key));
                    sb.Append('=');
                    sb.Append(HttpUtility.UrlEncode(item));
                    first = false;
                }
            }
            else if (value is DateTimeOffset dto)
            {
                sb.Append(first ? '?' : '&');
                sb.Append(HttpUtility.UrlEncode(key));
                sb.Append('=');
                sb.Append(HttpUtility.UrlEncode(dto.ToString("o")));
                first = false;
            }
            else
            {
                var stringValue = value.ToString();
                if (!string.IsNullOrEmpty(stringValue))
                {
                    sb.Append(first ? '?' : '&');
                    sb.Append(HttpUtility.UrlEncode(key));
                    sb.Append('=');
                    sb.Append(HttpUtility.UrlEncode(stringValue));
                    first = false;
                }
            }
        }

        return sb.ToString();
    }
}
