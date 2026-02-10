# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TofuPilot C# SDK - Official open-source C# SDK for the TofuPilot hardware testing platform. Manages test runs, units, procedures, parts, batches, and stations. Published on NuGet as `TofuPilot`.

## Build and Development Commands

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run all tests
dotnet test

# Run specific test project
dotnet test tests/TofuPilot.Tests

# Run tests with verbose output
dotnet test --logger "console;verbosity=detailed"

# Run a specific test class
dotnet test --filter "FullyQualifiedName~RunsResourceTests"

# Build for release
dotnet build -c Release

# Pack NuGet packages
dotnet pack -c Release
```

## Testing

Tests require these environment variables for integration tests:

- `TOFUPILOT_URL` - Server URL (defaults to https://www.tofupilot.com)
- `TOFUPILOT_API_KEY` - API key for authentication
- `TOFUPILOT_PROCEDURE_ID` - A valid procedure ID for creating test runs

Unit tests can be run without environment variables.

## Architecture

### Project Structure

```
tofupilot-csharp/
├── src/
│   ├── TofuPilot.Abstractions/            # Shared types, enums, exceptions
│   │   ├── Configuration/                 # TofuPilotOptions, RetryOptions
│   │   ├── Exceptions/                    # TofuPilotException, API exceptions
│   │   └── Models/Enums/                  # RunOutcome, PhaseOutcome, etc.
│   └── TofuPilot/                         # Main API
│       ├── Configuration/                 # DI extensions
│       ├── Http/                          # HTTP client, handlers
│       ├── Models/                        # Request/response models
│       ├── Resources/                     # API resource clients
│       ├── Serialization/                 # JSON serialization context
│       └── TofuPilotClient.cs             # Main entry point
├── tests/
│   ├── TofuPilot.Tests/                   # Unit tests
│   └── TofuPilot.IntegrationTests/        # Integration tests
├── TofuPilot.slnx
├── Directory.Build.props                  # Shared build properties
└── CLAUDE.md
```

### Key Components

**TofuPilotClient**
```csharp
// Direct instantiation
using var client = new TofuPilotClient(apiKey: "your-key");
var runs = await client.Runs.ListAsync();

// Or with DI
services.AddTofuPilot(options => {
    options.ApiKey = "your-key";
    options.BaseUrl = "https://www.tofupilot.com";
});
```

### API Methods

| Resource | Methods |
|----------|---------|
| Runs | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| Units | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync, AddChildAsync, RemoveChildAsync |
| Procedures | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| Procedures.Versions | ListAsync, CreateAsync, GetAsync, DeleteAsync |
| Parts | ListAsync, CreateAsync, GetAsync, UpdateAsync |
| Parts.Revisions | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| Batches | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| Stations | ListAsync, CreateAsync, GetAsync, UpdateAsync, RemoveAsync, LinkProcedureAsync, UnlinkProcedureAsync |
| Attachments | InitializeAsync, DeleteAsync |

## Conventions

- **Target Framework**: .NET 10.0
- **Commit format**: Conventional Commits (`fix:`, `feat:`, `feat!:` for breaking)
- **Code style**: C# conventions, nullable reference types enabled
- **Async**: All HTTP operations are async with cancellation token support
- **Models**: C# records for immutable request/response types
- **Version**: Automated via GitVersion.MsBuild from git history (see `GitVersion.yml`)
