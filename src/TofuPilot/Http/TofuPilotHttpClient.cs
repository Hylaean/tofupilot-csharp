namespace TofuPilot.Http;

/// <summary>
/// HTTP client implementation for TofuPilot API.
/// </summary>
public sealed class TofuPilotHttpClient(HttpClient httpClient, JsonSerializerOptions? jsonOptions = null) : ITofuPilotHttpClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly JsonSerializerOptions _jsonOptions = jsonOptions ?? CreateDefaultOptions();

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        options.TypeInfoResolverChain.Add(TofuPilotJsonContext.Default);
        return options;
    }

    /// <inheritdoc/>
    public async Task<TResponse> GetAsync<TResponse>(string uri, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, options: _jsonOptions);
        var response = await _httpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse> PatchAsync<TRequest, TResponse>(string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync(uri, content, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse> DeleteAsync<TResponse>(string uri, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync(uri, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse> UploadFileAsync<TResponse>(
        string uri,
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(streamContent, "file", fileName);

        var response = await _httpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<TResponse> HandleResponseAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<TResponse>(responseBody, _jsonOptions)
                   ?? throw new TofuPilotException("Failed to deserialize response");

        throw CreateException(response.StatusCode, responseBody);
    }

    private static TofuPilotException CreateException(HttpStatusCode statusCode, string responseBody)
    {
        var errorMessage = TryGetErrorMessage(responseBody) ?? $"API error: {statusCode}";

        return statusCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestException(errorMessage, responseBody: responseBody),
            HttpStatusCode.Unauthorized => new UnauthorizedException(errorMessage, responseBody: responseBody),
            HttpStatusCode.Forbidden => new ForbiddenException(errorMessage, responseBody: responseBody),
            HttpStatusCode.NotFound => new NotFoundException(errorMessage, responseBody: responseBody),
            HttpStatusCode.Conflict => new ConflictException(errorMessage, responseBody: responseBody),
            HttpStatusCode.UnprocessableEntity => new UnprocessableEntityException(errorMessage, responseBody: responseBody),
            (HttpStatusCode)429 => new RateLimitException(errorMessage, responseBody: responseBody),
            HttpStatusCode.InternalServerError => new InternalServerErrorException(errorMessage, responseBody: responseBody),
            HttpStatusCode.ServiceUnavailable => new ServiceUnavailableException(errorMessage, responseBody: responseBody),
            _ => new TofuPilotException(errorMessage, statusCode, responseBody: responseBody)
        };
    }

    private static string? TryGetErrorMessage(string responseBody)
    {
        try
        {
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            if (root.TryGetProperty("message", out var message))
                return message.GetString();

            if (root.TryGetProperty("error", out var error))
            {
                return error.ValueKind == JsonValueKind.String
                    ? error.GetString()
                    : error.TryGetProperty("message", out var errorMessage)
                        ? errorMessage.GetString()
                        : null;
            }
        }
        catch (JsonException)
        {
            // Ignore parsing errors
        }
        return null;
    }
}
