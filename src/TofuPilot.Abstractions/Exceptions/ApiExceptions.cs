using System.Net;

namespace TofuPilot.Abstractions.Exceptions;

/// <summary>
/// Exception thrown when a bad request (400) is returned from the API.
/// </summary>
public class BadRequestException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public BadRequestException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.BadRequest, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when authentication fails (401).
/// </summary>
public class UnauthorizedException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public UnauthorizedException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.Unauthorized, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when access is forbidden (403).
/// </summary>
public class ForbiddenException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public ForbiddenException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.Forbidden, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when a resource is not found (404).
/// </summary>
public class NotFoundException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public NotFoundException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.NotFound, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when there is a conflict (409).
/// </summary>
public class ConflictException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public ConflictException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.Conflict, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when the request is unprocessable (422).
/// </summary>
public class UnprocessableEntityException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnprocessableEntityException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public UnprocessableEntityException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.UnprocessableEntity, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when rate limited (429).
/// </summary>
public class RateLimitException : TofuPilotException
{
    /// <summary>
    /// Gets the time to wait before retrying, if provided.
    /// </summary>
    public TimeSpan? RetryAfter { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RateLimitException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="retryAfter">The time to wait before retrying.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public RateLimitException(
        string message,
        TimeSpan? retryAfter = null,
        string? errorCode = null,
        string? responseBody = null)
        : base(message, (HttpStatusCode)429, errorCode, responseBody)
    {
        RetryAfter = retryAfter;
    }
}

/// <summary>
/// Exception thrown when an internal server error occurs (500).
/// </summary>
public class InternalServerErrorException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public InternalServerErrorException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.InternalServerError, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown when the service is unavailable (503).
/// </summary>
public class ServiceUnavailableException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code from the API.</param>
    /// <param name="responseBody">The raw response body.</param>
    public ServiceUnavailableException(string message, string? errorCode = null, string? responseBody = null)
        : base(message, HttpStatusCode.ServiceUnavailable, errorCode, responseBody)
    {
    }
}

/// <summary>
/// Exception thrown for network-related errors.
/// </summary>
public class NetworkException : TofuPilotException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public NetworkException(string message, Exception? innerException = null)
        : base(message, innerException ?? new Exception())
    {
    }
}
