using System.Net;
using Microsoft.Extensions.Options;
using TofuPilot.Abstractions.Configuration;

namespace TofuPilot.Http;

/// <summary>
/// HTTP message handler that implements retry logic with exponential backoff.
/// </summary>
public sealed class RetryHandler : DelegatingHandler
{
    private readonly RetryOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryHandler"/> class.
    /// </summary>
    /// <param name="options">The TofuPilot options.</param>
    public RetryHandler(IOptions<TofuPilotOptions> options)
    {
        _options = options.Value.Retry;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!_options.Enabled)
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        var attempt = 0;
        HttpResponseMessage? response = null;

        while (attempt <= _options.MaxRetries)
        {
            try
            {
                // Clone request for retry (original request can only be sent once)
                var clonedRequest = await CloneRequestAsync(request).ConfigureAwait(false);
                response = await base.SendAsync(clonedRequest, cancellationToken).ConfigureAwait(false);

                if (!ShouldRetry(response.StatusCode) || attempt == _options.MaxRetries)
                {
                    return response;
                }

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
            var delay = CalculateDelay(attempt);
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }

        // Should not reach here, but return last response if we do
        return response ?? throw new InvalidOperationException("No response received");
    }

    private bool ShouldRetry(HttpStatusCode statusCode)
    {
        return _options.RetryableStatusCodes.Contains((int)statusCode);
    }

    private TimeSpan CalculateDelay(int attempt)
    {
        var delay = _options.InitialDelayMs * Math.Pow(_options.BackoffMultiplier, attempt - 1);
        delay = Math.Min(delay, _options.MaxDelayMs);

        // Add jitter (±10%)
        var jitter = delay * 0.1 * (Random.Shared.NextDouble() * 2 - 1);
        delay += jitter;

        return TimeSpan.FromMilliseconds(delay);
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri)
        {
            Version = request.Version
        };

        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (request.Content != null)
        {
            var contentBytes = await request.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            clone.Content = new ByteArrayContent(contentBytes);

            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        return clone;
    }
}
