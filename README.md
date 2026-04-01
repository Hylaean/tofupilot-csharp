# TofuPilot C# SDK

[![NuGet](https://img.shields.io/nuget/v/Hylaean.TofuPilot.svg)](https://www.nuget.org/packages/Hylaean.TofuPilot)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)

Unofficial open-source C# SDK for [TofuPilot](https://tofupilot.com). Integrate all your hardware test runs into one app with just a few lines of C#.

<sub>Looking for the official client? See [tofupilot/csharp-client](https://github.com/tofupilot/csharp-client).</sub>

## Installation

```bash
dotnet add package Hylaean.TofuPilot
```

## Quick Start

```csharp
using Hylaean.TofuPilot;
using Hylaean.TofuPilot.Models.Runs;
using Hylaean.TofuPilot.Abstractions.Models;

// Create a client (reads TOFUPILOT_API_KEY from environment by default)
using var client = new TofuPilotClient(apiKey: "your-api-key");

// List recent runs
var runs = await client.Runs.ListAsync(new ListRunsRequest { Limit = 10 });

// Create a test run
var run = await client.Runs.CreateAsync(new CreateRunRequest
{
    ProcedureId = "your-procedure-id",
    Outcome = RunOutcome.PASS,
    SerialNumber = "SN-001",
    StartedAt = DateTimeOffset.UtcNow.AddMinutes(-5),
    EndedAt = DateTimeOffset.UtcNow
});

Console.WriteLine($"Created run: {run.Id}");
```

## Dependency Injection

```csharp
services.AddTofuPilot(options =>
{
    options.ApiKey = configuration["TofuPilot:ApiKey"];
    options.BaseUrl = "https://www.tofupilot.com";
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

## Documentation

| Topic | Description |
|-------|-------------|
| [Resources](docs/resources.md) | Full reference for all API resources (runs, units, procedures, parts, batches, stations, attachments, users) |
| [Configuration](docs/configuration.md) | Direct instantiation, DI setup, environment variables, retry options |
| [Error Handling](docs/error-handling.md) | Typed exceptions, status codes, base exception properties |
| [API Coverage](API_COVERAGE.md) | Operation-by-operation mapping to the OpenAPI spec |

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Links

- [TofuPilot Website](https://tofupilot.com)
- [TofuPilot Documentation](https://tofupilot.com/docs)
- [API Reference (V2)](https://tofupilot.com/docs/api/v2)
- [Official C# Client](https://github.com/tofupilot/csharp-client)
- [Python SDK](https://github.com/tofupilot/tofupilot)
- [Examples](https://github.com/tofupilot/examples)
