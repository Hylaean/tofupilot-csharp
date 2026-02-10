namespace TofuPilot.Http;

/// <summary>
/// HTTP message handler that implements retry logic with exponential backoff.
/// </summary>
public sealed class RetryHandler(IOptions<TofuPilotOptions> options) : DelegatingHandler
{
    private readonly RetryOptions _options = options.Value.Retry;

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!_options.Enabled)
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        var attempt = 0;
        HttpResponseMessage? response = null;

        while (attempt <= _options.MaxRetries)
        {
            try
            {
                var clonedRequest = await CloneRequestAsync(request).ConfigureAwait(false);
                response = await base.SendAsync(clonedRequest, cancellationToken).ConfigureAwait(false);

                if (!ShouldRetry(response.StatusCode) || attempt == _options.MaxRetries)
                    return response;

                response.Dispose();
            }
            catch (HttpRequestException) when (attempt < _options.MaxRetries)
            {
                // Network error, will retry
            }
            catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested && attempt < _options.MaxRetries)
            {
                // Timeout, will retry
            }

            attempt++;
            await Task.Delay(CalculateDelay(attempt), cancellationToken).ConfigureAwait(false);
        }

        return response ?? throw new InvalidOperationException("No response received");
    }

    private bool ShouldRetry(HttpStatusCode statusCode) =>
        _options.RetryableStatusCodes.Contains((int)statusCode);

    private TimeSpan CalculateDelay(int attempt)
    {
        var delay = _options.InitialDelayMs * Math.Pow(_options.BackoffMultiplier, attempt - 1);
        delay = Math.Min(delay, _options.MaxDelayMs);
        delay += delay * 0.1 * (Random.Shared.NextDouble() * 2 - 1); // ±10% jitter
        return TimeSpan.FromMilliseconds(delay);
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri) { Version = request.Version };

        foreach (var header in request.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        if (request.Content is { } content)
        {
            var contentBytes = await content.ReadAsByteArrayAsync().ConfigureAwait(false);
            clone.Content = new ByteArrayContent(contentBytes);

            foreach (var header in content.Headers)
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}
