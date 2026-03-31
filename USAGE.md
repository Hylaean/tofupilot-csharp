# Hylaean.Tofupilot — API Reference

Unofficial C# SDK for the [TofuPilot](https://tofupilot.com) hardware testing platform (V2 API).

---

## Table of Contents

- [Installation](#installation)
- [Authentication](#authentication)
- [Client Setup](#client-setup)
- [Dependency Injection](#dependency-injection)
- [Resources](#resources)
  - [Runs](#runs)
  - [Units](#units)
  - [Procedures](#procedures)
  - [Procedure Versions](#procedure-versions)
  - [Parts](#parts)
  - [Part Revisions](#part-revisions)
  - [Batches](#batches)
  - [Stations](#stations)
  - [Attachments](#attachments)
  - [Users](#users)
- [Pagination](#pagination)
- [Error Handling](#error-handling)
- [Configuration](#configuration)
- [Enums](#enums)

---

## Installation

```bash
dotnet add package Hylaean.Tofupilot
```

## Authentication

The SDK authenticates via a Bearer token (API key). It can be provided directly or read from the `TOFUPILOT_API_KEY` environment variable.

```csharp
// Explicit
using var client = new TofuPilotClient(apiKey: "tp_...");

// From environment variable TOFUPILOT_API_KEY
using var client = new TofuPilotClient();
```

## Client Setup

```csharp
using Hylaean.Tofupilot;

// Defaults: API key from env, base URL https://www.tofupilot.app/api/
using var client = new TofuPilotClient();

// Custom base URL
using var client = new TofuPilotClient(
    apiKey: "tp_...",
    baseUrl: "https://your-instance.tofupilot.com/api/"
);
```

The base URL can also be set via the `TOFUPILOT_URL` environment variable.

## Dependency Injection

```csharp
using Hylaean.Tofupilot.Configuration;

// Option 1: Configure with action
services.AddTofuPilot(options =>
{
    options.ApiKey = "tp_...";
    options.BaseUrl = "https://www.tofupilot.com";
    options.TimeoutSeconds = 30;
    options.Retry = new RetryOptions
    {
        MaxRetries = 3,
        InitialDelayMs = 1000,
        MaxDelayMs = 30000
    };
});

// Option 2: Bind from IConfiguration (reads section "TofuPilot")
services.AddTofuPilot(configuration);
```

Then inject `TofuPilotClient` into your services:

```csharp
public class MyService(TofuPilotClient client)
{
    public async Task DoWork()
    {
        var runs = await client.Runs.ListAsync();
    }
}
```

---

## Resources

All methods are async, accept an optional `CancellationToken`, and throw typed exceptions on failure.

---

### Runs

Manage test runs — the core entity in TofuPilot.

#### `Runs.ListAsync(ListRunsRequest?)`

Lists runs with optional filtering.

```csharp
var runs = await client.Runs.ListAsync(new ListRunsRequest
{
    Outcomes = [RunOutcome.PASS, RunOutcome.FAIL],
    ProcedureIds = ["proc-uuid"],
    SerialNumbers = ["SN-001"],
    StartedAfter = DateTimeOffset.UtcNow.AddDays(-7),
    Limit = 20,
    SortBy = "started_at",
    SortOrder = "desc"
});

foreach (var run in runs.Data)
    Console.WriteLine($"{run.Id} — {run.Outcome}");
```

**ListRunsRequest fields:**

| Field | Type | Description |
|-------|------|-------------|
| `SearchQuery` | `string?` | Full-text search |
| `Ids` | `IReadOnlyList<string>?` | Filter by run IDs |
| `Outcomes` | `IReadOnlyList<RunOutcome>?` | Filter by outcome |
| `ProcedureIds` | `IReadOnlyList<string>?` | Filter by procedure |
| `ProcedureVersions` | `IReadOnlyList<string>?` | Filter by procedure version |
| `SerialNumbers` | `IReadOnlyList<string>?` | Filter by serial number |
| `PartNumbers` | `IReadOnlyList<string>?` | Filter by part number |
| `RevisionNumbers` | `IReadOnlyList<string>?` | Filter by revision number |
| `DurationMin` | `double?` | Minimum duration |
| `DurationMax` | `double?` | Maximum duration |
| `StartedAfter` | `DateTimeOffset?` | Started after date |
| `StartedBefore` | `DateTimeOffset?` | Started before date |
| `EndedAfter` | `DateTimeOffset?` | Ended after date |
| `EndedBefore` | `DateTimeOffset?` | Ended before date |
| `CreatedAfter` | `DateTimeOffset?` | Created after date |
| `CreatedBefore` | `DateTimeOffset?` | Created before date |
| `CreatedByUserIds` | `IReadOnlyList<string>?` | Filter by creating user |
| `CreatedByStationIds` | `IReadOnlyList<string>?` | Filter by creating station |
| `OperatedByIds` | `IReadOnlyList<string>?` | Filter by operator |
| `Limit` | `int?` | Page size (default 50) |
| `Cursor` | `double?` | Pagination cursor |
| `SortBy` | `string?` | Sort field (default `"started_at"`) |
| `SortOrder` | `string?` | `"asc"` or `"desc"` (default `"desc"`) |

#### `Runs.CreateAsync(CreateRunRequest)`

Creates a test run with phases, measurements, and logs.

```csharp
var run = await client.Runs.CreateAsync(new CreateRunRequest
{
    Outcome = RunOutcome.PASS,
    ProcedureId = "proc-uuid",
    StartedAt = DateTimeOffset.UtcNow.AddMinutes(-10),
    EndedAt = DateTimeOffset.UtcNow,
    SerialNumber = "SN-001",
    PartNumber = "PART-A",
    RevisionNumber = "A",
    BatchNumber = "BATCH-01",
    ProcedureVersion = "1.0.0",
    SubUnits = ["SN-SUB-001"],
    Phases = [
        new CreateRunPhase
        {
            Name = "Voltage Test",
            Outcome = PhaseOutcome.PASS,
            StartedAt = DateTimeOffset.UtcNow.AddMinutes(-10),
            EndedAt = DateTimeOffset.UtcNow.AddMinutes(-5),
            Measurements = [
                new CreateRunMeasurement
                {
                    Name = "Output Voltage",
                    Outcome = MeasurementOutcome.PASS,
                    MeasuredValue = 5.02,
                    Units = "V",
                    LowerLimit = 4.8,
                    UpperLimit = 5.2
                }
            ]
        }
    ],
    Logs = [
        new CreateRunLog
        {
            Level = LogLevel.INFO,
            Timestamp = DateTimeOffset.UtcNow.ToString("o"),
            Message = "Test completed"
        }
    ]
});
```

**CreateRunRequest fields:**

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `Outcome` | `RunOutcome` | Yes | RUNNING, PASS, FAIL, ERROR, TIMEOUT, ABORTED |
| `ProcedureId` | `string` | Yes | Procedure UUID |
| `StartedAt` | `DateTimeOffset` | Yes | Run start time |
| `EndedAt` | `DateTimeOffset` | Yes | Run end time |
| `SerialNumber` | `string` | Yes | Unit serial number |
| `ProcedureVersion` | `string?` | No | Procedure version tag |
| `OperatedBy` | `string?` | No | Operator ID |
| `PartNumber` | `string?` | No | Part number |
| `RevisionNumber` | `string?` | No | Revision number |
| `BatchNumber` | `string?` | No | Batch number |
| `SubUnits` | `IReadOnlyList<string>?` | No | Sub-unit serial numbers |
| `Docstring` | `string?` | No | Documentation string |
| `Phases` | `IReadOnlyList<CreateRunPhase>?` | No | Test phases |
| `Logs` | `IReadOnlyList<CreateRunLog>?` | No | Log entries |

#### `Runs.GetAsync(string id)`

```csharp
var run = await client.Runs.GetAsync("run-uuid");
```

#### `Runs.UpdateAsync(string id, UpdateRunRequest)`

Currently used to attach files to a run.

```csharp
await client.Runs.UpdateAsync(runId, new UpdateRunRequest
{
    Attachments = [attachmentId]
});
```

#### `Runs.DeleteAsync(IEnumerable<string> ids)`

Bulk deletes runs by IDs.

```csharp
var result = await client.Runs.DeleteAsync(["run-1", "run-2"]);
// result.Id contains the list of deleted IDs
```

---

### Units

Manage units under test, including parent-child hierarchies.

#### `Units.ListAsync(ListUnitsRequest?)`

```csharp
var units = await client.Units.ListAsync(new ListUnitsRequest
{
    SerialNumbers = ["SN-001", "SN-002"],
    Limit = 20
});
```

#### `Units.CreateAsync(CreateUnitRequest)`

```csharp
var unit = await client.Units.CreateAsync(new CreateUnitRequest
{
    SerialNumber = "SN-001",
    PartNumber = "PART-A",
    RevisionNumber = "A",
    BatchNumber = "BATCH-01"       // optional
});
```

#### `Units.GetAsync(string serialNumber)`

```csharp
var unit = await client.Units.GetAsync("SN-001");
```

#### `Units.UpdateAsync(string serialNumber, UpdateUnitRequest)`

```csharp
var updated = await client.Units.UpdateAsync("SN-001", new UpdateUnitRequest
{
    SerialNumber = "SN-001-NEW"    // renames the serial number
});
```

#### `Units.DeleteAsync(string serialNumber)` / `DeleteAsync(IEnumerable<string>)`

```csharp
await client.Units.DeleteAsync("SN-001");
await client.Units.DeleteAsync(["SN-001", "SN-002"]);
```

#### `Units.AddChildAsync(string parentSn, string childSn)`

```csharp
var parent = await client.Units.AddChildAsync("PARENT-SN", "CHILD-SN");
```

#### `Units.RemoveChildAsync(string parentSn, string childSn)`

```csharp
var parent = await client.Units.RemoveChildAsync("PARENT-SN", "CHILD-SN");
```

---

### Procedures

Manage test procedures (test definitions).

#### `Procedures.ListAsync(ListProceduresRequest?)`

```csharp
var procs = await client.Procedures.ListAsync(new ListProceduresRequest
{
    SearchQuery = "battery"
});
```

#### `Procedures.CreateAsync(CreateProcedureRequest)`

```csharp
var proc = await client.Procedures.CreateAsync(new CreateProcedureRequest
{
    Name = "Battery Test",
    Description = "Full charge/discharge cycle"
});
```

#### `Procedures.GetAsync(string id)` / `UpdateAsync` / `DeleteAsync`

```csharp
var proc = await client.Procedures.GetAsync("proc-uuid");

var updated = await client.Procedures.UpdateAsync("proc-uuid", new UpdateProcedureRequest
{
    Name = "Battery Test v2",
    Description = "Updated description"
});

await client.Procedures.DeleteAsync("proc-uuid");
```

---

### Procedure Versions

Accessed via `client.Procedures.Versions`.

#### `Versions.CreateAsync(string procedureId, CreateProcedureVersionRequest)`

```csharp
var version = await client.Procedures.Versions.CreateAsync("proc-uuid",
    new CreateProcedureVersionRequest { Tag = "1.0.0" });
```

#### `Versions.GetAsync(string procedureId, string tag)`

```csharp
var version = await client.Procedures.Versions.GetAsync("proc-uuid", "1.0.0");
```

#### `Versions.DeleteAsync(string procedureId, string tag)`

```csharp
await client.Procedures.Versions.DeleteAsync("proc-uuid", "1.0.0");
```

---

### Parts

Manage part definitions.

#### `Parts.ListAsync(ListPartsRequest?)`

```csharp
var parts = await client.Parts.ListAsync(new ListPartsRequest
{
    SearchQuery = "capacitor",
    Limit = 10
});
```

#### `Parts.CreateAsync(CreatePartRequest)`

```csharp
var part = await client.Parts.CreateAsync(new CreatePartRequest
{
    PartNumber = "CAP-100UF",
    Name = "100µF Capacitor",
    Description = "Electrolytic, 25V"
});
```

#### `Parts.GetAsync` / `UpdateAsync` / `DeleteAsync`

```csharp
var part = await client.Parts.GetAsync("CAP-100UF");

var updated = await client.Parts.UpdateAsync("CAP-100UF", new UpdatePartRequest
{
    Name = "100µF Capacitor (updated)"
});

// Returns DeletePartResponse with Id and DeletedRevisionIds (cascade)
var result = await client.Parts.DeleteAsync("CAP-100UF");
```

---

### Part Revisions

Accessed via `client.Parts.Revisions`.

#### `Revisions.CreateAsync(string partNumber, CreatePartRevisionRequest)`

```csharp
var rev = await client.Parts.Revisions.CreateAsync("CAP-100UF",
    new CreatePartRevisionRequest { RevisionNumber = "B" });
```

#### `Revisions.GetAsync` / `UpdateAsync` / `DeleteAsync`

```csharp
var rev = await client.Parts.Revisions.GetAsync("CAP-100UF", "B");

var updated = await client.Parts.Revisions.UpdateAsync("CAP-100UF", "B",
    new UpdatePartRevisionRequest { RevisionNumber = "C" });

await client.Parts.Revisions.DeleteAsync("CAP-100UF", "C");
```

---

### Batches

Manage production batches.

#### `Batches.ListAsync(ListBatchesRequest?)`

```csharp
var batches = await client.Batches.ListAsync(new ListBatchesRequest
{
    SearchQuery = "2024-Q1"
});
```

#### `Batches.CreateAsync(CreateBatchRequest)`

```csharp
var batch = await client.Batches.CreateAsync(new CreateBatchRequest
{
    BatchNumber = "BATCH-2024-Q1",
    PartNumber = "CAP-100UF"        // optional
});
```

#### `Batches.GetAsync` / `UpdateAsync` / `DeleteAsync`

```csharp
var batch = await client.Batches.GetAsync("BATCH-2024-Q1");

var updated = await client.Batches.UpdateAsync("BATCH-2024-Q1", new UpdateBatchRequest
{
    BatchNumber = "BATCH-2024-Q1-FINAL"
});

await client.Batches.DeleteAsync("BATCH-2024-Q1-FINAL");
```

---

### Stations

Manage test stations (physical or logical test locations).

#### `Stations.ListAsync(ListStationsRequest?)`

```csharp
var stations = await client.Stations.ListAsync(new ListStationsRequest
{
    SearchQuery = "assembly"
});
```

#### `Stations.CreateAsync(CreateStationRequest)`

```csharp
var station = await client.Stations.CreateAsync(new CreateStationRequest
{
    Name = "Assembly Station 1",
    Description = "Main assembly line"
});
```

#### `Stations.GetAsync` / `GetCurrentAsync` / `UpdateAsync` / `RemoveAsync`

```csharp
var station = await client.Stations.GetAsync("station-uuid");

// Get the station identified by the current API key
var current = await client.Stations.GetCurrentAsync();

var updated = await client.Stations.UpdateAsync("station-uuid", new UpdateStationRequest
{
    Description = "Updated description"
});

await client.Stations.RemoveAsync("station-uuid");
```

---

### Attachments

Upload and manage file attachments. The SDK provides a one-line upload that wraps the 3-step flow (initialize → S3 PUT → finalize).

#### `Attachments.UploadAsync(string filePath)` / `UploadAsync(Stream, string)`

```csharp
// Upload from file path
var attachmentId = await client.Attachments.UploadAsync("report.pdf");

// Upload from stream
using var stream = File.OpenRead("data.csv");
var id = await client.Attachments.UploadAsync(stream, "data.csv");

// Link to a run
await client.Runs.UpdateAsync(runId, new UpdateRunRequest
{
    Attachments = [attachmentId]
});
```

#### `AttachmentsResource.DownloadAsync(string url, string localPath)`

```csharp
// Download from a signed URL (e.g. from a run's attachment)
await AttachmentsResource.DownloadAsync(attachment.DownloadUrl, "local-report.pdf");
```

#### Low-Level API

If you need more control over the upload flow:

```csharp
// 1. Initialize — get presigned URL
var init = await client.Attachments.InitializeAsync(
    new InitializeUploadRequest { FileName = "report.pdf" });

// 2. Upload to S3 (your own HTTP PUT to init.UploadUrl)
// ...

// 3. Finalize
var finalized = await client.Attachments.FinalizeAsync(init.Id!);
// finalized.Url = signed download URL
```

#### `Attachments.DeleteAsync`

```csharp
await client.Attachments.DeleteAsync("attachment-uuid");
await client.Attachments.DeleteAsync(["id-1", "id-2"]);
```

> **Note:** Attachments linked to runs cannot be deleted — unlink them first.

---

### Users

List organization users.

#### `Users.ListAsync(ListUsersRequest?)`

```csharp
// List all users
var users = await client.Users.ListAsync();

// Get only the current authenticated user
var me = await client.Users.ListAsync(new ListUsersRequest { Current = true });
```

**User fields:** `Id`, `Email`, `Name`, `Image`, `Banned`

---

## Pagination

List methods return `PaginatedResponse<T>` with cursor-based pagination:

```csharp
var allRuns = new List<Run>();
double? cursor = null;

do
{
    var page = await client.Runs.ListAsync(new ListRunsRequest
    {
        Limit = 50,
        Cursor = cursor
    });

    allRuns.AddRange(page.Data);
    cursor = page.HasMore ? page.NextCursor : null;

} while (cursor is not null);
```

**PaginatedResponse<T> properties:**

| Property | Type | Description |
|----------|------|-------------|
| `Data` | `IReadOnlyList<T>` | Items on this page |
| `Meta` | `PaginationMeta?` | Pagination metadata |
| `HasMore` | `bool` | Whether more pages exist |
| `NextCursor` | `double?` | Cursor for the next page |

---

## Extension Methods

Every model object has fluent extension methods so you can work directly with objects instead of passing IDs back to resource methods. Both styles coexist — use whichever fits:

```csharp
// Resource-first (existing API — unchanged)
await client.Runs.DeleteAsync([run.Id]);

// Model-first (extension methods — new)
await run.DeleteAsync(client);
```

### Run Extensions

```csharp
// Upload a file and attach it to a run in one call
var attachmentId = await run.AttachAsync(client, "report.pdf");

// Stream overload
var id = await run.AttachAsync(client, stream, "data.csv");

// Re-fetch latest state
run = await run.RefreshAsync(client);

// Delete
await run.DeleteAsync(client);
```

### Unit Extensions

```csharp
// Parent-child management
var parent = await unit.AddChildAsync(client, "CHILD-SN");
parent = await unit.RemoveChildAsync(client, "CHILD-SN");

// Update, refresh, delete
var updated = await unit.UpdateAsync(client, new UpdateUnitRequest { SerialNumber = "NEW-SN" });
unit = await unit.RefreshAsync(client);
await unit.DeleteAsync(client);
```

### Procedure Extensions

```csharp
var updated = await proc.UpdateAsync(client, new UpdateProcedureRequest { Name = "v2" });
var version = await proc.CreateVersionAsync(client, new CreateProcedureVersionRequest { Tag = "1.0.0" });
proc = await proc.RefreshAsync(client);
await proc.DeleteAsync(client);
```

### Part Extensions

```csharp
var updated = await part.UpdateAsync(client, new UpdatePartRequest { Name = "Updated" });
var rev = await part.CreateRevisionAsync(client, new CreatePartRevisionRequest { RevisionNumber = "B" });
part = await part.RefreshAsync(client);
await part.DeleteAsync(client);   // also deletes revisions
```

### Batch Extensions

```csharp
var updated = await batch.UpdateAsync(client, new UpdateBatchRequest { BatchNumber = "NEW-NUM" });
batch = await batch.RefreshAsync(client);
await batch.DeleteAsync(client);
```

### Station Extensions

```csharp
var updated = await station.UpdateAsync(client, new UpdateStationRequest { Description = "New desc" });
station = await station.RefreshAsync(client);
await station.RemoveAsync(client);
```

---

## Error Handling

All API errors throw typed exceptions inheriting from `TofuPilotException`:

```csharp
try
{
    var run = await client.Runs.GetAsync("invalid-id");
}
catch (NotFoundException ex)
{
    Console.WriteLine($"Not found: {ex.Message}");
}
catch (UnauthorizedException)
{
    Console.WriteLine("Invalid API key");
}
catch (RateLimitException ex)
{
    Console.WriteLine($"Rate limited — retry after {ex.RetryAfter}");
}
catch (TofuPilotException ex)
{
    Console.WriteLine($"API error {ex.StatusCode}: {ex.Message}");
    Console.WriteLine($"Response: {ex.ResponseBody}");
}
```

**Exception types:**

| Exception | HTTP Status | Description |
|-----------|-------------|-------------|
| `BadRequestException` | 400 | Invalid request parameters |
| `UnauthorizedException` | 401 | Invalid or missing API key |
| `ForbiddenException` | 403 | Access denied |
| `NotFoundException` | 404 | Resource not found |
| `ConflictException` | 409 | Resource conflict (duplicate) |
| `UnprocessableEntityException` | 422 | Validation error |
| `RateLimitException` | 429 | Rate limit exceeded (has `RetryAfter`) |
| `InternalServerErrorException` | 500 | Server error |
| `ServiceUnavailableException` | 503 | Service unavailable |
| `NetworkException` | — | Network/connectivity error |

All exceptions expose: `StatusCode`, `ErrorCode`, `ResponseBody`.

---

## Configuration

### TofuPilotOptions

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ApiKey` | `string?` | env `TOFUPILOT_API_KEY` | API key |
| `BaseUrl` | `string?` | `"https://www.tofupilot.com"` | API base URL (env `TOFUPILOT_URL`) |
| `TimeoutSeconds` | `int` | `30` | HTTP timeout |
| `Retry` | `RetryOptions` | see below | Retry configuration |

### RetryOptions

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Enabled` | `bool` | `true` | Enable automatic retries |
| `MaxRetries` | `int` | `3` | Maximum retry attempts |
| `InitialDelayMs` | `int` | `1000` | Initial delay between retries (ms) |
| `MaxDelayMs` | `int` | `30000` | Maximum delay between retries (ms) |
| `BackoffMultiplier` | `double` | `2.0` | Exponential backoff multiplier |
| `RetryableStatusCodes` | `int[]` | `[429, 500, 502, 503, 504]` | Status codes that trigger retries |

---

## Enums

### RunOutcome

`RUNNING` · `PASS` · `FAIL` · `ERROR` · `TIMEOUT` · `ABORTED`

### PhaseOutcome

`PASS` · `FAIL` · `SKIP` · `ERROR`

### MeasurementOutcome

`PASS` · `FAIL` · `UNSET`

### LogLevel

`DEBUG` · `INFO` · `WARNING` · `ERROR` · `CRITICAL`
