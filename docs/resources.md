# Resources

Full reference for every resource exposed by the `TofuPilotClient`.

> See also [`API_COVERAGE.md`](../API_COVERAGE.md) for the operation-by-operation mapping to the OpenAPI spec.

## Runs

```csharp
// List (paginated, with filters)
var page = await client.Runs.ListAsync(new ListRunsRequest
{
    Limit = 10,
    Outcomes = [RunOutcome.PASS],
    StartedAfter = DateTimeOffset.UtcNow.AddDays(-7)
});

// Create
var run = await client.Runs.CreateAsync(new CreateRunRequest
{
    ProcedureId = "proc-123",
    Outcome = RunOutcome.PASS,
    SerialNumber = "SN-001",
    StartedAt = DateTimeOffset.UtcNow.AddMinutes(-5),
    EndedAt = DateTimeOffset.UtcNow
});

// Get / Update / Delete
var run = await client.Runs.GetAsync("run-id");
var updated = await client.Runs.UpdateAsync("run-id", new UpdateRunRequest { ... });
var deleted = await client.Runs.DeleteAsync(["run-id-1", "run-id-2"]);
```

### Phases & Measurements

```csharp
var run = await client.Runs.CreateAsync(new CreateRunRequest
{
    ProcedureId = "proc-123",
    Outcome = RunOutcome.PASS,
    SerialNumber = "SN-001",
    StartedAt = DateTimeOffset.UtcNow.AddMinutes(-10),
    EndedAt = DateTimeOffset.UtcNow,
    Phases =
    [
        new CreateRunPhase
        {
            Name = "Voltage Test",
            Outcome = PhaseOutcome.PASS,
            StartedAt = DateTimeOffset.UtcNow.AddMinutes(-10),
            EndedAt = DateTimeOffset.UtcNow.AddMinutes(-5),
            Measurements =
            [
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
    ]
});
```

## Units

```csharp
// List
var units = await client.Units.ListAsync(new ListUnitsRequest { Limit = 20 });

// Create
var unit = await client.Units.CreateAsync(new CreateUnitRequest
{
    SerialNumber = "UNIT-001",
    PartNumber = "PART-A"
});

// Get / Update / Delete
var unit = await client.Units.GetAsync("UNIT-001");
var updated = await client.Units.UpdateAsync("UNIT-001", new UpdateUnitRequest { ... });
var deleted = await client.Units.DeleteAsync("UNIT-001");

// Sub-unit assembly
var parent = await client.Units.AddChildAsync("PARENT-SN", "CHILD-SN");
var parent = await client.Units.RemoveChildAsync("PARENT-SN", "CHILD-SN");
```

## Procedures

```csharp
// List / Create
var procedures = await client.Procedures.ListAsync();
var procedure = await client.Procedures.CreateAsync(new CreateProcedureRequest
{
    Name = "Battery Test",
    Description = "Full battery charge/discharge cycle test"
});

// Get / Update / Delete
var proc = await client.Procedures.GetAsync("proc-id");
var updated = await client.Procedures.UpdateAsync("proc-id", new UpdateProcedureRequest { ... });
await client.Procedures.DeleteAsync("proc-id");
```

### Procedure Versions

```csharp
var version = await client.Procedures.Versions.CreateAsync(
    procedureId: "proc-id",
    new CreateProcedureVersionRequest { Tag = "v1.0.0" }
);

var version = await client.Procedures.Versions.GetAsync("proc-id", "v1.0.0");
await client.Procedures.Versions.DeleteAsync("proc-id", "v1.0.0");
```

## Parts

```csharp
var parts = await client.Parts.ListAsync();
var part = await client.Parts.CreateAsync(new CreatePartRequest { ... });
var part = await client.Parts.GetAsync("PART-A");
var updated = await client.Parts.UpdateAsync("PART-A", new UpdatePartRequest { ... });
await client.Parts.DeleteAsync("PART-A");
```

### Part Revisions

```csharp
var rev = await client.Parts.Revisions.CreateAsync("PART-A", new CreatePartRevisionRequest { ... });
var rev = await client.Parts.Revisions.GetAsync("PART-A", "rev-1");
var rev = await client.Parts.Revisions.UpdateAsync("PART-A", "rev-1", new UpdatePartRevisionRequest { ... });
await client.Parts.Revisions.DeleteAsync("PART-A", "rev-1");
```

## Batches

```csharp
var batches = await client.Batches.ListAsync();
var batch = await client.Batches.CreateAsync(new CreateBatchRequest { ... });
var batch = await client.Batches.GetAsync("BATCH-001");
var updated = await client.Batches.UpdateAsync("BATCH-001", new UpdateBatchRequest { ... });
await client.Batches.DeleteAsync("BATCH-001");
```

## Stations

```csharp
var stations = await client.Stations.ListAsync();
var station = await client.Stations.CreateAsync(new CreateStationRequest { ... });
var current = await client.Stations.GetCurrentAsync();
var station = await client.Stations.GetAsync("station-id");
var updated = await client.Stations.UpdateAsync("station-id", new UpdateStationRequest { ... });
await client.Stations.RemoveAsync("station-id");
```

## Attachments

```csharp
// Upload a file (initialize + S3 PUT + finalize in one call)
var attachmentId = await client.Attachments.UploadAsync("report.pdf");

// Or upload from a stream
using var stream = File.OpenRead("data.csv");
var id = await client.Attachments.UploadAsync(stream, "data.csv");

// Download from a signed URL
await AttachmentsResource.DownloadAsync(downloadUrl, "local-report.pdf");

// Delete
await client.Attachments.DeleteAsync("attachment-id");
```

## Users

```csharp
var users = await client.Users.ListAsync();
```

## Method Summary

| Resource | Methods |
|----------|---------|
| **Runs** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Units** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync, AddChildAsync, RemoveChildAsync |
| **Procedures** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Procedures.Versions** | CreateAsync, GetAsync, DeleteAsync |
| **Parts** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Parts.Revisions** | CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Batches** | ListAsync, CreateAsync, GetAsync, UpdateAsync, DeleteAsync |
| **Stations** | ListAsync, CreateAsync, GetCurrentAsync, GetAsync, UpdateAsync, RemoveAsync |
| **Attachments** | UploadAsync, InitializeAsync, FinalizeAsync, DownloadAsync, DeleteAsync |
| **Users** | ListAsync |
