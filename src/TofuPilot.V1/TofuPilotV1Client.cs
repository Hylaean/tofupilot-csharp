using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TofuPilot.Abstractions.Exceptions;
using TofuPilot.V1.Constants;
using TofuPilot.V1.Models;
using TofuPilot.V1.Models.Responses;
using TofuPilot.V1.Utils;

namespace TofuPilot.V1;

/// <summary>
/// Client for the TofuPilot V1 (Legacy) API.
/// </summary>
public sealed class TofuPilotV1Client : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly bool _ownsHttpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotV1Client"/> class.
    /// </summary>
    /// <param name="apiKey">The API key for authentication. Defaults to TOFUPILOT_API_KEY environment variable.</param>
    /// <param name="baseUrl">The base URL for the API. Defaults to TOFUPILOT_URL environment variable or https://www.tofupilot.com.</param>
    public TofuPilotV1Client(string? apiKey = null, string? baseUrl = null)
    {
        var resolvedApiKey = apiKey ?? Environment.GetEnvironmentVariable("TOFUPILOT_API_KEY");
        if (string.IsNullOrEmpty(resolvedApiKey))
        {
            throw new ArgumentException(
                "API key is required. Set TOFUPILOT_API_KEY environment variable or pass it to the constructor.",
                nameof(apiKey));
        }

        var resolvedBaseUrl = baseUrl ?? Environment.GetEnvironmentVariable("TOFUPILOT_URL") ?? ApiConstants.DefaultEndpoint;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(resolvedBaseUrl)
        };
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resolvedApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _ownsHttpClient = true;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotV1Client"/> class with an existing HTTP client.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public TofuPilotV1Client(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _ownsHttpClient = false;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    /// <summary>
    /// Creates a test run with the specified parameters.
    /// </summary>
    /// <param name="unitUnderTest">The unit being tested.</param>
    /// <param name="runPassed">Whether the test run passed.</param>
    /// <param name="procedureId">The procedure ID (optional).</param>
    /// <param name="procedureName">The procedure name (optional).</param>
    /// <param name="procedureVersion">The procedure version (optional).</param>
    /// <param name="startedAt">When the run started (optional).</param>
    /// <param name="duration">The duration of the run (optional).</param>
    /// <param name="phases">The test phases (optional).</param>
    /// <param name="subUnits">The sub-units (optional).</param>
    /// <param name="attachments">File paths to attach (optional).</param>
    /// <param name="logs">Log entries (optional).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The create run response.</returns>
    public async Task<CreateRunResponse> CreateRunAsync(
        UnitUnderTest unitUnderTest,
        bool runPassed,
        string? procedureId = null,
        string? procedureName = null,
        string? procedureVersion = null,
        DateTimeOffset? startedAt = null,
        TimeSpan? duration = null,
        IReadOnlyList<Phase>? phases = null,
        IReadOnlyList<SubUnit>? subUnits = null,
        IReadOnlyList<string>? attachments = null,
        IReadOnlyList<Log>? logs = null,
        CancellationToken cancellationToken = default)
    {
        // Validate attachments if provided
        if (attachments != null && attachments.Count > 0)
        {
            FileValidator.ValidateFiles(attachments);
        }

        var payload = new Dictionary<string, object?>
        {
            ["unit_under_test"] = unitUnderTest,
            ["run_passed"] = runPassed,
            ["procedure_id"] = procedureId,
            ["procedure_name"] = procedureName,
            ["procedure_version"] = procedureVersion,
            ["client"] = "CSharp",
            ["client_version"] = GetType().Assembly.GetName().Version?.ToString() ?? "1.0.0"
        };

        if (startedAt.HasValue)
        {
            payload["started_at"] = DateTimeHelper.ToIso8601(startedAt.Value);
        }

        if (duration.HasValue)
        {
            payload["duration"] = DateTimeHelper.ToIso8601Duration(duration.Value);
        }

        if (phases != null)
        {
            payload["phases"] = phases;
        }

        if (subUnits != null)
        {
            payload["sub_units"] = subUnits;
        }

        if (logs != null)
        {
            payload["logs"] = logs;
        }

        var response = await PostAsync<CreateRunResponse>($"{ApiConstants.ApiPath}/runs", payload, cancellationToken)
            .ConfigureAwait(false);

        // Upload attachments if run was created successfully
        if (response.Id != null && attachments != null && attachments.Count > 0)
        {
            await UploadAttachmentsAsync(response.Id, attachments, cancellationToken).ConfigureAwait(false);
        }

        return response;
    }

    /// <summary>
    /// Gets runs by serial number.
    /// </summary>
    /// <param name="serialNumber">The serial number to search for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The runs response.</returns>
    public async Task<GetRunsResponse> GetRunsAsync(string serialNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(serialNumber))
        {
            throw new ArgumentException("Serial number is required.", nameof(serialNumber));
        }

        var uri = $"{ApiConstants.ApiPath}/runs?serial_number={Uri.EscapeDataString(serialNumber)}";
        return await GetAsync<GetRunsResponse>(uri, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> PostAsync<T>(string uri, object payload, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(payload, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<T>(responseBody, _jsonOptions);
            return result ?? throw new TofuPilotException("Failed to deserialize response");
        }

        var errorMessage = TryGetErrorMessage(responseBody) ?? $"API error: {response.StatusCode}";
        throw new TofuPilotException(errorMessage, response.StatusCode, responseBody: responseBody);
    }

    private async Task UploadAttachmentsAsync(
        string runId,
        IEnumerable<string> filePaths,
        CancellationToken cancellationToken)
    {
        foreach (var filePath in filePaths)
        {
            await UploadFileAsync(runId, filePath, cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task UploadFileAsync(string runId, string filePath, CancellationToken cancellationToken)
    {
        // First, initialize the upload
        var fileName = Path.GetFileName(filePath);
        var contentType = GetContentType(filePath);
        var fileInfo = new FileInfo(filePath);

        var initPayload = new
        {
            file_name = fileName,
            content_type = contentType,
            file_size = fileInfo.Length
        };

        var initResponse = await PostAsync<InitializeUploadResponse>($"{ApiConstants.ApiPath}/attachments/initialize", initPayload, cancellationToken)
            .ConfigureAwait(false);

        // Upload the file to the presigned URL
        await using var fileStream = File.OpenRead(filePath);
        using var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        using var uploadRequest = new HttpRequestMessage(HttpMethod.Put, initResponse.PresignedUrl)
        {
            Content = streamContent
        };

        var uploadResponse = await _httpClient.SendAsync(uploadRequest, cancellationToken).ConfigureAwait(false);
        uploadResponse.EnsureSuccessStatusCode();

        // Link the attachment to the run
        var linkPayload = new { attachments = new[] { initResponse.UploadId } };
        await PostAsync<object>($"{ApiConstants.ApiPath}/runs/{runId}", linkPayload, cancellationToken).ConfigureAwait(false);
    }

    private static string GetContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch
        {
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".csv" => "text/csv",
            ".txt" => "text/plain",
            ".pdf" => "application/pdf",
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".zip" => "application/zip",
            ".tar" => "application/x-tar",
            ".gz" => "application/gzip",
            _ => "application/octet-stream"
        };
    }

    private static string? TryGetErrorMessage(string responseBody)
    {
        try
        {
            using var doc = JsonDocument.Parse(responseBody);
            if (doc.RootElement.TryGetProperty("message", out var message))
            {
                return message.GetString();
            }
            if (doc.RootElement.TryGetProperty("error", out var error))
            {
                if (error.ValueKind == JsonValueKind.String)
                {
                    return error.GetString();
                }
                if (error.TryGetProperty("message", out var errorMessage))
                {
                    return errorMessage.GetString();
                }
            }
        }
        catch (JsonException)
        {
            // Ignore parsing errors
        }
        return null;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_disposed)
        {
            if (_ownsHttpClient)
            {
                _httpClient.Dispose();
            }
            _disposed = true;
        }
    }

    private record InitializeUploadResponse
    {
        public required string UploadId { get; init; }
        public required string PresignedUrl { get; init; }
    }
}
