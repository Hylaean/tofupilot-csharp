using Hylaean.Tofupilot.Models.Batches;
using Hylaean.Tofupilot.Models.Common;

namespace Hylaean.Tofupilot;

/// <summary>
/// Fluent extension methods for <see cref="Batch"/>.
/// </summary>
public static class BatchExtensions
{
    /// <summary>Updates this batch.</summary>
    public static Task<Batch> UpdateAsync(this Batch batch, TofuPilotClient client, UpdateBatchRequest request, CancellationToken cancellationToken = default) =>
        client.Batches.UpdateAsync(batch.BatchNumber!, request, cancellationToken);

    /// <summary>Deletes this batch.</summary>
    public static Task<BulkDeleteResponse> DeleteAsync(this Batch batch, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Batches.DeleteAsync(batch.BatchNumber!, cancellationToken);

    /// <summary>Re-fetches this batch from the API.</summary>
    public static Task<Batch> RefreshAsync(this Batch batch, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Batches.GetAsync(batch.BatchNumber!, cancellationToken);
}
