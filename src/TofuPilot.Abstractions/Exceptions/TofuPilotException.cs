using System.Net;

namespace TofuPilot.Abstractions.Exceptions;

/// <summary>
/// Base exception for all TofuPilot SDK errors.
/// </summary>
public class TofuPilotException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with this exception, if any.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Gets the error code returned by the API, if any.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Gets the raw response body, if available.
    /// </summary>
    public string? ResponseBody { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotException"/> class.
    /// </summary>
    public TofuPilotException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public TofuPilotException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public TofuPilotException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public TofuPilotException(
        string message,
        HttpStatusCode? statusCode,
        string? errorCode = null,
        string? responseBody = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TofuPilotException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public TofuPilotException(
        string message,
        HttpStatusCode? statusCode,
        Exception innerException,
        string? errorCode = null,
        string? responseBody = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        ResponseBody = responseBody;
    }
}
