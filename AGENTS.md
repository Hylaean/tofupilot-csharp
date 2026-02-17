# AGENTS.md

> **Full project guide:** See [CLAUDE.md](CLAUDE.md) for project structure, API resource reference, architecture details, and versioning.

## What This Repo Is

**tofupilot-csharp** — Official open-source C# SDK for the TofuPilot hardware testing platform. Manages test runs, units, procedures, parts, batches, and stations. Published on NuGet as `TofuPilot`.

## Safety Rules

- **Never** run integration tests against production without explicit instruction — they create/modify real test runs.
- **Never** hardcode API keys. Use environment variables: `TOFUPILOT_API_KEY`, `TOFUPILOT_URL`, `TOFUPILOT_PROCEDURE_ID`.
- **Never** push directly to `main`.

## Build & Test

```bash
# Restore + build
dotnet restore
dotnet build

# Run unit tests (no credentials needed)
dotnet test tests/TofuPilot.Tests

# Run all tests (integration requires env vars)
dotnet test

# Build NuGet packages
dotnet pack -c Release
```

**Integration test env vars:**
- `TOFUPILOT_URL` — server URL (default: https://www.tofupilot.com)
- `TOFUPILOT_API_KEY` — API key
- `TOFUPILOT_PROCEDURE_ID` — a valid procedure ID

## Key Directories

| Path | Purpose |
|------|---------|
| `src/TofuPilot/` | Main SDK — HTTP client, resources, DI extensions |
| `src/TofuPilot.Abstractions/` | Shared types, enums, exceptions, configuration |
| `tests/TofuPilot.Tests/` | Unit tests |
| `tests/TofuPilot.IntegrationTests/` | Integration tests |

## Conventions

- **Target framework**: .NET 10.0
- **Commit format**: Conventional Commits (`feat:`, `fix:`, `feat!:` for breaking changes)
- **Async**: all HTTP operations async with `CancellationToken` support
- **Models**: C# records for immutable request/response types
- **Version**: auto-derived from git history via `GitVersion.MsBuild` — tag to release (`git tag vX.Y.Z`)
