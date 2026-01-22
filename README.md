# TofuPilot C# SDK

[![NuGet](https://img.shields.io/nuget/v/TofuPilot.svg)](https://www.nuget.org/packages/TofuPilot)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)

The official open-source C# SDK for [TofuPilot](https://tofupilot.com). Quickly and seamlessly integrate all your hardware test runs into one app with just a few lines of C#.

> **Note**: This C# SDK was created with [Claude](https://claude.ai), Anthropic's AI assistant, as a port of the official [Python SDK](https://github.com/tofupilot/tofupilot).

## Table of Contents

- [Installation](#installation)
- [Quick Start](#quick-start)
- [V2 API (Modern)](#v2-api-modern)
- [V1 API (Legacy)](#v1-api-legacy)
- [Dependency Injection](#dependency-injection)
- [Available Resources](#available-resources)
- [Error Handling](#error-handling)
- [Contributing](#contributing)
- [License](#license)
- [Links](#links)

## Installation

Install via NuGet Package Manager:

```bash
dotnet add package TofuPilot
```

Or via Package Manager Console:

```powershell
Install-Package TofuPilot
```

## Quick Start

```csharp
using TofuPilot;

// Create a client with your API key
using var client = new TofuPilotClient(apiKey: "your-api-key");

// List recent runs
var runs = await client.Runs.ListAsync(new ListRunsRequest { Limit = 10 });

// Create a new test run
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

## V2 API (Modern)

The V2 API provides a clean, resource-based interface for all TofuPilot operations.

### Configuration

```csharp
using TofuPilot;

// Basic usage
using var client = new TofuPilotClient(apiKey: "your-api-key");

// With custom base URL
using var client = new TofuPilotClient(
    apiKey: "your-api-key",
    baseUrl: "https://your-instance.tofupilot.com"
);
```

### Creating a Run with Phases and Measurements

```csharp
var request = new CreateRunRequest
{
    ProcedureId = "proc-123",
    Outcome = RunOutcome.PASS,
    SerialNumber = "UNIT-001",
    StartedAt = DateTimeOffset.UtcNow.AddMinutes(-10),
    EndedAt = DateTimeOffset.UtcNow,
    Phases = new List<CreateRunPhase>
    {
        new()
        {
            Name = "Voltage Test",
            Outcome = PhaseOutcome.PASS,
            StartTimeMillis = DateTimeOffset.UtcNow.AddMinutes(-10).ToUnixTimeMilliseconds(),
            EndTimeMillis = DateTimeOffset.UtcNow.AddMinutes(-5).ToUnixTimeMilliseconds(),
            Measurements = new List<CreateRunMeasurement>
            {
                new()
                {
                    Name = "Output Voltage",
                    Outcome = MeasurementOutcome.PASS,
                    MeasuredValue = 5.02,
                    Units = "V",
                    LowerLimit = 4.8,
                    UpperLimit = 5.2
                }
            }
        }
    }
};

var run = await client.Runs.CreateAsync(request);
```

### Working with Units

```csharp
// List units
var units = await client.Units.ListAsync(new ListUnitsRequest { Limit = 20 });

// Create a unit
var unit = await client.Units.CreateAsync(new CreateUnitRequest
{
    SerialNumber = "UNIT-001",
    PartNumber = "PART-A"
});

// Add a child unit (for assemblies)
await client.Units.AddChildAsync(parentUnitId, new AddChildRequest
{
    ChildId = childUnitId
});
```

### Managing Procedures

```csharp
// List procedures
var procedures = await client.Procedures.ListAsync(new ListProceduresRequest());

// Create a procedure
var procedure = await client.Procedures.CreateAsync(new CreateProcedureRequest
{
    Name = "Battery Test",
    Description = "Full battery charge/discharge cycle test"
});

// Create a version
var version = await client.Procedures.Versions.CreateAsync(
    procedureId: procedure.Id,
    new CreateVersionRequest { Name = "v1.0.0" }
);
```

## V1 API (Legacy)

The V1 API provides backward compatibility with the original TofuPilot API.

```csharp
using TofuPilot.V1;
using TofuPilot.V1.Models;

using var client = new TofuPilotV1Client(apiKey: "your-api-key");

var response = await client.CreateRunAsync(
    unitUnderTest: new UnitUnderTest
    {
        SerialNumber = "SN-001",
        PartNumber = "PART-A"
    },
    runPassed: true,
    procedureName: "My Test Procedure",
    phases: new List<Phase>
    {
        new()
        {
            Name = "Phase 1",
            Outcome = "PASS",
            Measurements = new List<Measurement>
            {
                new()
                {
                    Name = "Temperature",
                    MeasuredValue = 25.5,
                    Units = "C",
                    LowerLimit = 20.0,
                    UpperLimit = 30.0
                }
            }
        }
    }
);

Console.WriteLine($"Run URL: {response.ReportUrl}");
```

## Dependency Injection

The SDK supports ASP.NET Core dependency injection:

```csharp
// In Program.cs or Startup.cs
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

// In your service
public class TestRunService
{
    private readonly TofuPilotClient _client;

    public TestRunService(TofuPilotClient client)
    {
        _client = client;
    }

    public async Task<Run> CreateTestRunAsync(string serialNumber)
    {
        return await _client.Runs.CreateAsync(new CreateRunRequest
        {
            SerialNumber = serialNumber,
            // ...
        });
    }
}
```

## Available Resources

| Resource | Methods |
|----------|---------|
| **Runs** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Units** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync, AddChildAsync, RemoveChildAsync |
| **Procedures** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Procedures.Versions** | ListAsync, CreateAsync, GetAsync, DeleteAsync |
| **Parts** | ListAsync, CreateAsync, GetAsync, UpdateAsync |
| **Parts.Revisions** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Batches** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Stations** | ListAsync, CreateAsync, GetAsync, UpdateAsync, RemoveAsync, LinkProcedureAsync, UnlinkProcedureAsync |
| **Attachments** | InitializeAsync, DeleteAsync |

## Error Handling

The SDK throws specific exceptions for different error types:

```csharp
using TofuPilot.Abstractions.Exceptions;

try
{
    var run = await client.Runs.GetAsync("invalid-id");
}
catch (NotFoundException ex)
{
    Console.WriteLine($"Run not found: {ex.Message}");
}
catch (UnauthorizedException ex)
{
    Console.WriteLine($"Invalid API key: {ex.Message}");
}
catch (BadRequestException ex)
{
    Console.WriteLine($"Invalid request: {ex.Message}");
    Console.WriteLine($"Response body: {ex.ResponseBody}");
}
catch (TofuPilotException ex)
{
    Console.WriteLine($"API error ({ex.StatusCode}): {ex.Message}");
}
```

### Exception Types

| Exception | HTTP Status | Description |
|-----------|-------------|-------------|
| `BadRequestException` | 400 | Invalid request parameters |
| `UnauthorizedException` | 401 | Invalid or missing API key |
| `ForbiddenException` | 403 | Access denied |
| `NotFoundException` | 404 | Resource not found |
| `ConflictException` | 409 | Resource conflict |
| `UnprocessableEntityException` | 422 | Validation error |
| `RateLimitException` | 429 | Rate limit exceeded |
| `InternalServerErrorException` | 500 | Server error |
| `ServiceUnavailableException` | 503 | Service unavailable |

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Links

- [TofuPilot Website](https://tofupilot.com)
- [TofuPilot Documentation](https://tofupilot.com/docs)
- [API Reference (V2)](https://tofupilot.com/docs/api/v2)
- [Python SDK](https://github.com/tofupilot/tofupilot)
- [Examples](https://github.com/tofupilot/examples)

## Contact

If you have any questions or feedback, feel free to open an issue or contact us at support@tofupilot.com.
