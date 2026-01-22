namespace TofuPilot.Http;

/// <summary>
/// HTTP message handler that adds authentication headers to requests.
/// </summary>
public sealed class AuthenticationHandler(IOptions<TofuPilotOptions> options) : DelegatingHandler
{
    private readonly TofuPilotOptions _options = options.Value;

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (GetApiKey() is { } apiKey)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private string? GetApiKey() =>
        !string.IsNullOrEmpty(_options.ApiKey)
            ? _options.ApiKey
            : Environment.GetEnvironmentVariable("TOFUPILOT_API_KEY");
}
