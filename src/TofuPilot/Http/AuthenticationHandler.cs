using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TofuPilot.Abstractions.Configuration;

namespace TofuPilot.Http;

/// <summary>
/// HTTP message handler that adds authentication headers to requests.
/// </summary>
public sealed class AuthenticationHandler : DelegatingHandler
{
    private readonly TofuPilotOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationHandler"/> class.
    /// </summary>
    /// <param name="options">The TofuPilot options.</param>
    public AuthenticationHandler(IOptions<TofuPilotOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var apiKey = GetApiKey();
        if (!string.IsNullOrEmpty(apiKey))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private string? GetApiKey()
    {
        // First check options, then environment variable
        if (!string.IsNullOrEmpty(_options.ApiKey))
        {
            return _options.ApiKey;
        }

        return Environment.GetEnvironmentVariable("TOFUPILOT_API_KEY");
    }
}
