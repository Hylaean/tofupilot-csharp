using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TofuPilot.Abstractions.Exceptions;
using TofuPilot.Abstractions.Models;
using TofuPilot.Models.Attachments;
using TofuPilot.Models.Batches;
using TofuPilot.Models.Parts;
using TofuPilot.Models.Procedures;
using TofuPilot.Models.Runs;
using TofuPilot.Models.Stations;
using TofuPilot.Models.Units;
using Xunit;

namespace TofuPilot.IntegrationTests;

/// <summary>
/// Comprehensive integration tests that exercise every v2 API endpoint.
/// Requires environment variables:
/// - TOFUPILOT_URL: The TofuPilot server URL
/// - TOFUPILOT_API_KEY: A valid API key
/// </summary>
[Trait("Category", "Integration")]
public class FullApiIntegrationTests : IDisposable
{
    private readonly TofuPilotClient? _client;
    private readonly bool _canRunTests;
    private readonly string _testId = Guid.NewGuid().ToString("N")[..8];

    public FullApiIntegrationTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<FullApiIntegrationTests>()
            .AddEnvironmentVariables()
            .Build();

        var apiKey = configuration["TOFUPILOT_API_KEY"];
        var baseUrl = configuration["TOFUPILOT_URL"];

        _canRunTests = !string.IsNullOrEmpty(apiKey);

        if (_canRunTests)
        {
            _client = new TofuPilotClient(apiKey, baseUrl);
        }
    }

    [Fact]
    public async Task Parts_FullLifecycle()
    {
        if (!_canRunTests) return;

        var partNumber = $"PART-{_testId}";

        // Create
        var part = await _client!.Parts.CreateAsync(new CreatePartRequest
        {
            PartNumber = partNumber,
            Name = "Integration Test Part",
            Description = "Created by integration test"
        });
        part.Should().NotBeNull();
        part.Id.Should().NotBeNullOrEmpty();

        // List
        var parts = await _client.Parts.ListAsync(new ListPartsRequest { Limit = 5 });
        parts.Should().NotBeNull();
        parts.Data.Should().NotBeNull();

        // Get (uses part number, not UUID)
        var retrieved = await _client.Parts.GetAsync(partNumber);
        retrieved.Id.Should().Be(part.Id);
        retrieved.PartNumber.Should().Be(partNumber);
        retrieved.Name.Should().Be("Integration Test Part");

        // Update (uses part number)
        var updated = await _client.Parts.UpdateAsync(partNumber, new UpdatePartRequest
        {
            Name = "Updated Integration Test Part"
        });
        updated.Should().NotBeNull();
    }

    [Fact]
    public async Task PartRevisions_FullLifecycle()
    {
        if (!_canRunTests) return;

        var partNumber = $"PARTREV-{_testId}";

        // Setup: create a part
        await _client!.Parts.CreateAsync(new CreatePartRequest
        {
            PartNumber = partNumber,
            Name = "Part for Revision Test"
        });

        // Create revision (uses part number)
        var revision = await _client.Parts.Revisions.CreateAsync(partNumber, new CreatePartRevisionRequest
        {
            RevisionNumber = "B1"
        });
        revision.Should().NotBeNull();
        revision.Id.Should().NotBeNullOrEmpty();

        // Get revision (uses part number + revision number)
        var retrieved = await _client.Parts.Revisions.GetAsync(partNumber, "B1");
        retrieved.Id.Should().Be(revision.Id);
        retrieved.RevisionNumber.Should().Be("B1");

        // Update revision
        var updated = await _client.Parts.Revisions.UpdateAsync(partNumber, "B1", new UpdatePartRevisionRequest
        {
            RevisionNumber = "B2"
        });
        updated.Should().NotBeNull();

        // Delete revision (uses part number + revision number)
        var deleteResult = await _client.Parts.Revisions.DeleteAsync(partNumber, "B2");
        deleteResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Procedures_FullLifecycle()
    {
        if (!_canRunTests) return;

        // Create
        var procedure = await _client!.Procedures.CreateAsync(new CreateProcedureRequest
        {
            Name = $"Procedure-{_testId}",
            Description = "Integration test procedure"
        });
        procedure.Should().NotBeNull();
        procedure.Id.Should().NotBeNullOrEmpty();

        // List
        var procedures = await _client.Procedures.ListAsync(new ListProceduresRequest { Limit = 5 });
        procedures.Should().NotBeNull();
        procedures.Data.Should().NotBeNull();

        // Get (uses UUID)
        var retrieved = await _client.Procedures.GetAsync(procedure.Id);
        retrieved.Id.Should().Be(procedure.Id);
        retrieved.Name.Should().Be($"Procedure-{_testId}");

        // Update (name is required by the API)
        var updated = await _client.Procedures.UpdateAsync(procedure.Id, new UpdateProcedureRequest
        {
            Name = $"Procedure-{_testId}-Updated",
            Description = "Updated description"
        });
        updated.Should().NotBeNull();

        // Cleanup
        await _client.Procedures.DeleteAsync(procedure.Id);
    }

    [Fact]
    public async Task ProcedureVersions_FullLifecycle()
    {
        if (!_canRunTests) return;

        // Setup: create a procedure
        var procedure = await _client!.Procedures.CreateAsync(new CreateProcedureRequest
        {
            Name = $"ProcVer-{_testId}"
        });

        try
        {
            // Create version (field is 'tag')
            var version = await _client.Procedures.Versions.CreateAsync(procedure.Id, new CreateProcedureVersionRequest
            {
                Tag = "1.0.0"
            });
            version.Should().NotBeNull();
            version.Id.Should().NotBeNullOrEmpty();

            // Get version (uses tag, not UUID)
            var retrieved = await _client.Procedures.Versions.GetAsync(procedure.Id, "1.0.0");
            retrieved.Id.Should().Be(version.Id);
            retrieved.Tag.Should().Be("1.0.0");

            // Delete version (uses tag)
            var deleteResult = await _client.Procedures.Versions.DeleteAsync(procedure.Id, "1.0.0");
            deleteResult.Should().NotBeNull();
        }
        finally
        {
            await _client.Procedures.DeleteAsync(procedure.Id);
        }
    }

    [Fact]
    public async Task Stations_FullLifecycle()
    {
        if (!_canRunTests) return;

        // Create station
        var station = await _client!.Stations.CreateAsync(new CreateStationRequest
        {
            Name = $"Station-{_testId}",
            Description = "Integration test station"
        });
        station.Should().NotBeNull();
        station.Id.Should().NotBeNullOrEmpty();

        // List
        var stations = await _client.Stations.ListAsync(new ListStationsRequest { Limit = 5 });
        stations.Should().NotBeNull();
        stations.Data.Should().NotBeNull();

        // Get (uses UUID)
        var retrieved = await _client.Stations.GetAsync(station.Id);
        retrieved.Id.Should().Be(station.Id);

        // Update
        var updated = await _client.Stations.UpdateAsync(station.Id, new UpdateStationRequest
        {
            Description = "Updated station description"
        });
        updated.Should().NotBeNull();

        // Remove station
        var removeResult = await _client.Stations.RemoveAsync(station.Id);
        removeResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Batches_FullLifecycle()
    {
        if (!_canRunTests) return;

        var batchNumber = $"BATCH-{_testId}";

        // Create
        var batch = await _client!.Batches.CreateAsync(new CreateBatchRequest
        {
            BatchNumber = batchNumber
        });
        batch.Should().NotBeNull();
        batch.Id.Should().NotBeNullOrEmpty();

        // List
        var batches = await _client.Batches.ListAsync(new ListBatchesRequest { Limit = 5 });
        batches.Should().NotBeNull();
        batches.Data.Should().NotBeNull();

        // Get (uses batch number, not UUID)
        var retrieved = await _client.Batches.GetAsync(batchNumber);
        retrieved.Id.Should().Be(batch.Id);
        retrieved.BatchNumber.Should().Be(batchNumber);

        // Update (uses batch number)
        var updated = await _client.Batches.UpdateAsync(batchNumber, new UpdateBatchRequest
        {
            BatchNumber = $"BATCH-{_testId}-UPD"
        });
        updated.Should().NotBeNull();

        // Delete (uses batch number — updated one)
        var deleteResult = await _client.Batches.DeleteAsync($"BATCH-{_testId}-UPD");
        deleteResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Units_FullLifecycle()
    {
        if (!_canRunTests) return;

        var partNumber = $"UPART-{_testId}";
        var parentSn = $"PARENT-{_testId}";
        var childSn = $"CHILD-{_testId}";

        // Setup: create a part (needed for unit creation)
        await _client!.Parts.CreateAsync(new CreatePartRequest
        {
            PartNumber = partNumber,
            Name = "Part for Unit Test"
        });

        // Create parent unit (uses part number + default revision "A")
        var parent = await _client.Units.CreateAsync(new CreateUnitRequest
        {
            SerialNumber = parentSn,
            PartNumber = partNumber,
            RevisionNumber = "A"
        });
        parent.Should().NotBeNull();
        parent.Id.Should().NotBeNullOrEmpty();

        // Create child unit
        var child = await _client.Units.CreateAsync(new CreateUnitRequest
        {
            SerialNumber = childSn,
            PartNumber = partNumber,
            RevisionNumber = "A"
        });
        child.Should().NotBeNull();

        // List
        var units = await _client.Units.ListAsync(new ListUnitsRequest { Limit = 5 });
        units.Should().NotBeNull();
        units.Data.Should().NotBeNull();

        // Get (uses serial number, not UUID)
        var retrieved = await _client.Units.GetAsync(parentSn);
        retrieved.Id.Should().Be(parent.Id);
        retrieved.SerialNumber.Should().Be(parentSn);

        // Update (uses serial number)
        var updated = await _client.Units.UpdateAsync(parentSn, new UpdateUnitRequest
        {
            SerialNumber = $"PARENT-{_testId}-UPD"
        });
        updated.Should().NotBeNull();
        var updatedParentSn = $"PARENT-{_testId}-UPD";

        // Add child (uses serial numbers)
        var withChild = await _client.Units.AddChildAsync(updatedParentSn, childSn);
        withChild.Should().NotBeNull();

        // Remove child (uses serial numbers)
        var withoutChild = await _client.Units.RemoveChildAsync(updatedParentSn, childSn);
        withoutChild.Should().NotBeNull();

        // Delete (uses serial numbers)
        await _client.Units.DeleteAsync(childSn);
        await _client.Units.DeleteAsync(updatedParentSn);
    }

    [Fact]
    public async Task Runs_FullLifecycle()
    {
        if (!_canRunTests) return;

        // Setup: create a procedure and a part for the run
        var procedure = await _client!.Procedures.CreateAsync(new CreateProcedureRequest
        {
            Name = $"RunProc-{_testId}"
        });
        var runPartNumber = $"RUNPART-{_testId}";
        await _client.Parts.CreateAsync(new CreatePartRequest
        {
            PartNumber = runPartNumber,
            Name = "Part for Run Test"
        });

        try
        {
            var serialNumber = $"RUN-SN-{_testId}";
            var startedAt = DateTimeOffset.UtcNow.AddMinutes(-5);
            var endedAt = DateTimeOffset.UtcNow;

            // Create run with phases, measurements, and logs
            var run = await _client.Runs.CreateAsync(new CreateRunRequest
            {
                Outcome = RunOutcome.PASS,
                ProcedureId = procedure.Id,
                StartedAt = startedAt,
                EndedAt = endedAt,
                SerialNumber = serialNumber,
                PartNumber = runPartNumber,
                Phases = new List<CreateRunPhase>
                {
                    new()
                    {
                        Name = "Power On",
                        Outcome = PhaseOutcome.PASS,
                        StartedAt = startedAt,
                        EndedAt = startedAt.AddMinutes(1),
                        Measurements = new List<CreateRunMeasurement>
                        {
                            new()
                            {
                                Name = "Voltage",
                                Outcome = MeasurementOutcome.PASS,
                                MeasuredValue = 12.1,
                                Units = "V",
                                LowerLimit = 11.5,
                                UpperLimit = 12.5
                            },
                            new()
                            {
                                Name = "Current",
                                Outcome = MeasurementOutcome.PASS,
                                MeasuredValue = 2.3,
                                Units = "A",
                                LowerLimit = 2.0,
                                UpperLimit = 3.0
                            }
                        }
                    },
                    new()
                    {
                        Name = "Functional Test",
                        Outcome = PhaseOutcome.PASS,
                        StartedAt = startedAt.AddMinutes(1),
                        EndedAt = endedAt
                    }
                },
                Logs = new List<CreateRunLog>
                {
                    new()
                    {
                        Level = Abstractions.Models.LogLevel.INFO,
                        Timestamp = startedAt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        Message = "Test started",
                        SourceFile = "IntegrationTest.cs",
                        LineNumber = 1
                    },
                    new()
                    {
                        Level = Abstractions.Models.LogLevel.DEBUG,
                        Timestamp = endedAt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        Message = "Test completed successfully",
                        SourceFile = "IntegrationTest.cs",
                        LineNumber = 2
                    }
                }
            });
            run.Should().NotBeNull();
            run.Id.Should().NotBeNullOrEmpty();

            // List with filters
            var runs = await _client.Runs.ListAsync(new ListRunsRequest
            {
                Limit = 5,
                Outcomes = new[] { RunOutcome.PASS },
                ProcedureIds = new[] { procedure.Id }
            });
            runs.Should().NotBeNull();
            runs.Data.Should().NotBeNull();

            // Get (uses UUID)
            var retrieved = await _client.Runs.GetAsync(run.Id);
            retrieved.Id.Should().Be(run.Id);
            retrieved.Outcome.Should().Be(RunOutcome.PASS);

            // Delete
            var deleteResult = await _client.Runs.DeleteAsync(new[] { run.Id });
            deleteResult.Should().NotBeNull();
        }
        finally
        {
            await _client.Procedures.DeleteAsync(procedure.Id);
        }
    }

    [Fact]
    public async Task Attachments_Initialize()
    {
        if (!_canRunTests) return;

        // Initialize upload
        var upload = await _client!.Attachments.InitializeAsync(new InitializeUploadRequest
        {
            FileName = $"test-{_testId}.txt"
        });
        upload.Should().NotBeNull();
        upload.Id.Should().NotBeNullOrEmpty();
        upload.UploadUrl.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task FullWorkflow_EndToEnd()
    {
        if (!_canRunTests) return;

        var partNumber = $"E2E-PART-{_testId}";
        var batchNumber = $"E2E-BATCH-{_testId}";
        var unitSn = $"E2E-UNIT-{_testId}";
        var subUnitSn = $"E2E-SUB-{_testId}";

        // 1. Create a part
        await _client!.Parts.CreateAsync(new CreatePartRequest
        {
            PartNumber = partNumber,
            Name = "E2E Test Part"
        });

        // 2. Create a part revision (uses part number)
        await _client.Parts.Revisions.CreateAsync(partNumber, new CreatePartRevisionRequest
        {
            RevisionNumber = "R1"
        });

        // 3. Create a batch linked to the part
        await _client.Batches.CreateAsync(new CreateBatchRequest
        {
            BatchNumber = batchNumber,
            PartNumber = partNumber
        });

        // 4. Create a procedure
        var procedure = await _client.Procedures.CreateAsync(new CreateProcedureRequest
        {
            Name = $"E2E-Procedure-{_testId}",
            Description = "End-to-end test procedure"
        });

        // 5. Create a procedure version (field is 'tag')
        await _client.Procedures.Versions.CreateAsync(procedure.Id, new CreateProcedureVersionRequest
        {
            Tag = "2.0.0"
        });

        // 6. Create a station
        var station = await _client.Stations.CreateAsync(new CreateStationRequest
        {
            Name = $"E2E-Station-{_testId}",
            Description = "End-to-end test station"
        });

        // 7. Create a unit with part, revision, and batch
        await _client.Units.CreateAsync(new CreateUnitRequest
        {
            SerialNumber = unitSn,
            PartNumber = partNumber,
            RevisionNumber = "R1",
            BatchNumber = batchNumber
        });

        // 8. Create a sub-unit and establish parent-child relationship
        await _client.Units.CreateAsync(new CreateUnitRequest
        {
            SerialNumber = subUnitSn,
            PartNumber = partNumber,
            RevisionNumber = "R1"
        });
        await _client.Units.AddChildAsync(unitSn, subUnitSn);

        // 9. Create a run tying everything together
        var startedAt = DateTimeOffset.UtcNow.AddMinutes(-2);
        var endedAt = DateTimeOffset.UtcNow;
        var run = await _client.Runs.CreateAsync(new CreateRunRequest
        {
            Outcome = RunOutcome.PASS,
            ProcedureId = procedure.Id,
            ProcedureVersion = "2.0.0",
            StartedAt = startedAt,
            EndedAt = endedAt,
            SerialNumber = unitSn,
            PartNumber = partNumber,
            RevisionNumber = "R1",
            BatchNumber = batchNumber,
            SubUnits = new[] { subUnitSn },
            Phases = new List<CreateRunPhase>
            {
                new()
                {
                    Name = "Initialization",
                    Outcome = PhaseOutcome.PASS,
                    StartedAt = startedAt,
                    EndedAt = startedAt.AddSeconds(30),
                    Measurements = new List<CreateRunMeasurement>
                    {
                        new()
                        {
                            Name = "Boot Time",
                            Outcome = MeasurementOutcome.PASS,
                            MeasuredValue = 1.2,
                            Units = "s",
                            UpperLimit = 5.0
                        }
                    }
                }
            },
            Logs = new List<CreateRunLog>
            {
                new()
                {
                    Level = Abstractions.Models.LogLevel.INFO,
                    Timestamp = startedAt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    Message = "E2E test run started",
                    SourceFile = "IntegrationTest.cs",
                    LineNumber = 1
                }
            }
        });

        // 10. Verify the run was created (uses UUID)
        var retrieved = await _client.Runs.GetAsync(run.Id);
        retrieved.Id.Should().Be(run.Id);
        retrieved.Outcome.Should().Be(RunOutcome.PASS);
        retrieved.Phases.Should().NotBeNullOrEmpty();

        // 11. Initialize an attachment
        var upload = await _client.Attachments.InitializeAsync(new InitializeUploadRequest
        {
            FileName = "e2e-report.pdf"
        });
        upload.UploadUrl.Should().NotBeNullOrEmpty();

        // Cleanup in reverse dependency order (ignore 404s from cascade deletes)
        await TryCleanupAsync(() => _client.Runs.DeleteAsync(new[] { run.Id }));
        await TryCleanupAsync(() => _client.Units.RemoveChildAsync(unitSn, subUnitSn));
        await TryCleanupAsync(() => _client.Units.DeleteAsync(subUnitSn));
        await TryCleanupAsync(() => _client.Units.DeleteAsync(unitSn));
        await TryCleanupAsync(() => _client.Stations.RemoveAsync(station.Id));
        await TryCleanupAsync(() => _client.Procedures.Versions.DeleteAsync(procedure.Id, "2.0.0"));
        await TryCleanupAsync(() => _client.Procedures.DeleteAsync(procedure.Id));
        await TryCleanupAsync(() => _client.Batches.DeleteAsync(batchNumber));
        await TryCleanupAsync(() => _client.Parts.Revisions.DeleteAsync(partNumber, "R1"));
    }

    private static async Task TryCleanupAsync(Func<Task> action)
    {
        try { await action(); }
        catch (TofuPilotException) { /* Ignore cleanup failures */ }
    }

    public void Dispose()
    {
        _client?.Dispose();
    }
}
