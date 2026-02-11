using TofuPilot;
using TofuPilot.Abstractions.Exceptions;
using TofuPilot.Models.Batches;
using TofuPilot.Models.Parts;
using TofuPilot.Models.Procedures;
using TofuPilot.Models.Runs;
using TofuPilot.Models.Stations;
using TofuPilot.Models.Units;

const string Prefix = "tfcs-";

var apiKey = Environment.GetEnvironmentVariable("TOFUPILOT_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("TOFUPILOT_API_KEY not set — skipping cleanup.");
    return;
}

using var client = new TofuPilotClient();

Console.WriteLine($"TofuPilot Cleanup — hunting for '{Prefix}' entities...");
Console.WriteLine();

// 1. Collect tfcs- procedures (need their IDs to find related runs)
var procedures = await CollectAll(cursor =>
    client.Procedures.ListAsync(new ListProceduresRequest { SearchQuery = Prefix, Limit = 50, Cursor = cursor }));
var tfcsProcedures = procedures.Where(p => p.Name?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true).ToList();

// 2. Delete runs belonging to tfcs- procedures
var runCount = 0;
foreach (var proc in tfcsProcedures)
{
    var runs = await CollectAll(cursor =>
        client.Runs.ListAsync(new ListRunsRequest { ProcedureIds = [proc.Id], Limit = 50, Cursor = cursor }));
    if (runs.Count > 0)
    {
        await TryAsync(() => client.Runs.DeleteAsync(runs.Select(r => r.Id)));
        runCount += runs.Count;
    }
}
// Also find runs with tfcs- serial numbers (from units not tied to a procedure)
var runsFromSearch = await CollectAll(cursor =>
    client.Runs.ListAsync(new ListRunsRequest { SearchQuery = Prefix, Limit = 50, Cursor = cursor }));
var extraRuns = runsFromSearch.Where(r =>
    r.SerialNumber?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true ||
    r.PartNumber?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true).ToList();
if (extraRuns.Count > 0)
{
    await TryAsync(() => client.Runs.DeleteAsync(extraRuns.Select(r => r.Id)));
    runCount += extraRuns.Count;
}
Report("Runs", runCount);

// 3. Delete tfcs- units
var units = await CollectAll(cursor =>
    client.Units.ListAsync(new ListUnitsRequest { SearchQuery = Prefix, Limit = 50, Cursor = cursor }));
var tfcsUnits = units.Where(u => u.SerialNumber?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true).ToList();
foreach (var unit in tfcsUnits)
    await TryAsync(() => client.Units.DeleteAsync(unit.SerialNumber!));
Report("Units", tfcsUnits.Count);

// 4. Delete tfcs- batches
var batches = await CollectAll(cursor =>
    client.Batches.ListAsync(new ListBatchesRequest { SearchQuery = Prefix, Limit = 50, Cursor = cursor }));
var tfcsBatches = batches.Where(b => b.BatchNumber?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true).ToList();
foreach (var batch in tfcsBatches)
    await TryAsync(() => client.Batches.DeleteAsync(batch.BatchNumber!));
Report("Batches", tfcsBatches.Count);

// 5. Delete tfcs- parts (revisions are cascade-deleted)
var parts = await CollectAll(cursor =>
    client.Parts.ListAsync(new ListPartsRequest { SearchQuery = Prefix, Limit = 50, Cursor = cursor }));
var tfcsParts = parts.Where(p => p.PartNumber?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true).ToList();
foreach (var part in tfcsParts)
    await TryAsync(() => client.Parts.DeleteAsync(part.PartNumber!));
Report("Parts", tfcsParts.Count);

// 6. Delete tfcs- procedures
foreach (var proc in tfcsProcedures)
    await TryAsync(() => client.Procedures.DeleteAsync(proc.Id));
Report("Procedures", tfcsProcedures.Count);

// 7. Delete tfcs- stations
var stations = await CollectAll(cursor =>
    client.Stations.ListAsync(new ListStationsRequest { SearchQuery = Prefix, Limit = 50, Cursor = cursor }));
var tfcsStations = stations.Where(s => s.Name?.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) == true).ToList();
foreach (var station in tfcsStations)
    await TryAsync(() => client.Stations.RemoveAsync(station.Id));
Report("Stations", tfcsStations.Count);

Console.WriteLine();
Console.WriteLine("Cleanup complete.");
return;

// --- helpers ---

static void Report(string resource, int count)
{
    Console.WriteLine($"  {resource}: {count} found and deleted");
}

static async Task TryAsync(Func<Task> action)
{
    try { await action(); }
    catch (TofuPilotException) { /* entity may already be gone */ }
}

static async Task<List<T>> CollectAll<T>(Func<double?, Task<TofuPilot.Models.Common.PaginatedResponse<T>>> fetcher)
{
    var all = new List<T>();
    double? cursor = null;
    do
    {
        var page = await fetcher(cursor);
        all.AddRange(page.Data);
        cursor = page.HasMore ? page.NextCursor : null;
    } while (cursor is not null);
    return all;
}
