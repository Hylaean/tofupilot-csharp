# Error Handling

The SDK throws typed exceptions that map to HTTP status codes. All exceptions derive from `TofuPilotException`.

## Example

```csharp
using Hylaean.TofuPilot.Abstractions.Exceptions;

try
{
    var run = await client.Runs.GetAsync("invalid-id");
}
catch (NotFoundException ex)
{
    Console.WriteLine($"Not found: {ex.Message}");
}
catch (UnauthorizedException ex)
{
    Console.WriteLine($"Invalid API key: {ex.Message}");
}
catch (BadRequestException ex)
{
    Console.WriteLine($"Bad request: {ex.Message}");
    Console.WriteLine($"Response body: {ex.ResponseBody}");
}
catch (TofuPilotException ex)
{
    Console.WriteLine($"API error ({ex.StatusCode}): {ex.Message}");
}
```

## Exception Types

| Exception | HTTP Status | Description |
|-----------|-------------|-------------|
| `BadRequestException` | 400 | Invalid request parameters |
| `UnauthorizedException` | 401 | Invalid or missing API key |
| `ForbiddenException` | 403 | Access denied |
| `NotFoundException` | 404 | Resource not found |
| `ConflictException` | 409 | Resource conflict |
| `UnprocessableEntityException` | 422 | Validation error |
| `RateLimitException` | 429 | Rate limit exceeded (includes `RetryAfter`) |
| `InternalServerErrorException` | 500 | Server error |
| `ServiceUnavailableException` | 503 | Service unavailable |
| `NetworkException` | — | Connection / DNS / timeout errors |

## Base Exception Properties

Every `TofuPilotException` exposes:

| Property | Type | Description |
|----------|------|-------------|
| `StatusCode` | `HttpStatusCode?` | HTTP status code |
| `ErrorCode` | `string?` | Machine-readable error code from the API |
| `ResponseBody` | `string?` | Raw JSON response body |
