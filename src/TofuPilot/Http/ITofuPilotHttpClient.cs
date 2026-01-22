namespace TofuPilot.Http;

/// <summary>
/// Interface for the TofuPilot HTTP client.
/// </summary>
public interface ITofuPilotHttpClient
{
    /// <summary>
    /// Sends a GET request to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized response.</returns>
    Task<TResponse> GetAsync<TResponse>(string uri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request to the specified URI.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="request">The request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized response.</returns>
    Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a PATCH request to the specified URI.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="request">The request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized response.</returns>
    Task<TResponse> PatchAsync<TRequest, TResponse>(string uri, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a DELETE request to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized response.</returns>
    Task<TResponse> DeleteAsync<TResponse>(string uri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a file to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="fileStream">The file stream to upload.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="contentType">The content type of the file.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized response.</returns>
    Task<TResponse> UploadFileAsync<TResponse>(
        string uri,
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);
}
