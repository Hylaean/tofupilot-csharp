using TofuPilot.Http;
using TofuPilot.Resources;

namespace TofuPilot;

/// <summary>
/// Client for the TofuPilot V2 API.
/// </summary>
public sealed class TofuPilotClient : IDisposable
{
    private readonly HttpClient? _ownedHttpClient;
    private bool _disposed;

    /// <summary>
    /// Gets the runs resource.
    /// </summary>
    public RunsResource Runs { get; }

    /// <summary>
    /// Gets the units resource.
    /// </summary>
    public UnitsResource Units { get; }

    /// <summary>
    /// Gets the procedures resource.
    /// </summary>
    public ProceduresResource Procedures { get; }

    /// <summary>
    /// Gets the parts resource.
    /// </summary>
    public PartsResource Parts { get; }

    /// <summary>
    /// Gets the batches resource.
    /// </summary>
    public BatchesResource Batches { get; }

    /// <summary>
    /// Gets the stations resource.
    /// </summary>
    public StationsResource Stations { get; }

    /// <summary>
    /// Gets the attachments resource.
    /// </summary>
    public AttachmentsResource Attachments { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public TofuPilotClient(ITofuPilotHttpClient httpClient)
    {
        Runs = new RunsResource(httpClient);
        Units = new UnitsResource(httpClient);
        Procedures = new ProceduresResource(httpClient);
        Parts = new PartsResource(httpClient);
        Batches = new BatchesResource(httpClient);
        Stations = new StationsResource(httpClient);
        Attachments = new AttachmentsResource(httpClient);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotClient"/> class with an API key.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="baseUrl">The base URL for the API. Defaults to https://www.tofupilot.com.</param>
    public TofuPilotClient(string? apiKey = null, string? baseUrl = null)
    {
        var resolvedApiKey = apiKey ?? Environment.GetEnvironmentVariable("TOFUPILOT_API_KEY");
        var resolvedBaseUrl = baseUrl ?? Environment.GetEnvironmentVariable("TOFUPILOT_URL") ?? "https://www.tofupilot.com";

        _ownedHttpClient = new HttpClient
        {
            BaseAddress = new Uri(resolvedBaseUrl),
            DefaultRequestHeaders =
            {
                { "Authorization", $"Bearer {resolvedApiKey}" }
            }
        };

        var httpClient = new TofuPilotHttpClient(_ownedHttpClient);

        Runs = new RunsResource(httpClient);
        Units = new UnitsResource(httpClient);
        Procedures = new ProceduresResource(httpClient);
        Parts = new PartsResource(httpClient);
        Batches = new BatchesResource(httpClient);
        Stations = new StationsResource(httpClient);
        Attachments = new AttachmentsResource(httpClient);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_disposed)
        {
            _ownedHttpClient?.Dispose();
            _disposed = true;
        }
    }
}
