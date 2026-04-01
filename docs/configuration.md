# Configuration

## Direct Instantiation

```csharp
using Hylaean.TofuPilot;

// Minimal — reads TOFUPILOT_API_KEY and TOFUPILOT_URL from environment
using var client = new TofuPilotClient();

// Explicit values
using var client = new TofuPilotClient(
    apiKey: "your-api-key",
    baseUrl: "https://your-instance.tofupilot.com"
);
```

## Dependency Injection (ASP.NET Core)

```csharp
// In Program.cs or Startup.cs
services.AddTofuPilot(options =>
{
    options.ApiKey = configuration["TofuPilot:ApiKey"];
    options.BaseUrl = "https://www.tofupilot.com";
    options.TimeoutSeconds = 30;
    options.Retry = new RetryOptions
    {
        MaxRetries = 3,
        InitialDelayMs = 1000,
        MaxDelayMs = 30000
    };
});

// Inject TofuPilotClient anywhere
public class TestRunService(TofuPilotClient client)
{
    public Task<Run> CreateTestRunAsync(string serialNumber) =>
        client.Runs.CreateAsync(new CreateRunRequest
        {
            ProcedureId = "proc-123",
            Outcome = RunOutcome.PASS,
            SerialNumber = serialNumber,
            StartedAt = DateTimeOffset.UtcNow.AddMinutes(-5),
            EndedAt = DateTimeOffset.UtcNow
        });
}
```

You can also bind directly from `IConfiguration`:

```csharp
services.AddTofuPilot(configuration);
```

This reads from the `"TofuPilot"` section in `appsettings.json`:

```json
{
  "TofuPilot": {
    "ApiKey": "your-api-key",
    "BaseUrl": "https://www.tofupilot.com",
    "TimeoutSeconds": 30,
    "Retry": {
      "MaxRetries": 3,
      "InitialDelayMs": 1000,
      "MaxDelayMs": 30000
    }
  }
}
```

## Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `TOFUPILOT_API_KEY` | API key (used when none is passed explicitly) | — |
| `TOFUPILOT_URL` | Base URL | `https://www.tofupilot.app/api/` (direct) / `https://www.tofupilot.com` (DI) |

## Retry Options

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Enabled` | `bool` | `true` | Enable/disable retries |
| `MaxRetries` | `int` | `3` | Maximum retry attempts |
| `InitialDelayMs` | `int` | `1000` | First retry delay (ms) |
| `MaxDelayMs` | `int` | `30000` | Cap on retry delay (ms) |
| `BackoffMultiplier` | `double` | `2.0` | Exponential backoff factor |
| `RetryableStatusCodes` | `int[]` | `[429, 500, 502, 503, 504]` | Status codes that trigger retry |
