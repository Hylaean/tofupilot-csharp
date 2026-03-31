using Hylaean.TofuPilot.Models.Common;
using Hylaean.TofuPilot.Models.Runs;

namespace Hylaean.TofuPilot;

/// <summary>
/// Fluent extension methods for <see cref="Run"/>.
/// </summary>
public static class RunExtensions
{
    /// <summary>Uploads a file and attaches it to this run.</summary>
    public static async Task<string> AttachAsync(this Run run, TofuPilotClient client, string filePath, CancellationToken cancellationToken = default)
    {
        var id = await client.Attachments.UploadAsync(filePath, cancellationToken).ConfigureAwait(false);
        await client.Runs.UpdateAsync(run.Id, new UpdateRunRequest { Attachments = [id] }, cancellationToken).ConfigureAwait(false);
        return id;
    }

    /// <summary>Uploads a stream and attaches it to this run.</summary>
    public static async Task<string> AttachAsync(this Run run, TofuPilotClient client, Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        var id = await client.Attachments.UploadAsync(stream, fileName, cancellationToken).ConfigureAwait(false);
        await client.Runs.UpdateAsync(run.Id, new UpdateRunRequest { Attachments = [id] }, cancellationToken).ConfigureAwait(false);
        return id;
    }

    /// <summary>Re-fetches this run from the API.</summary>
    public static Task<Run> RefreshAsync(this Run run, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Runs.GetAsync(run.Id, cancellationToken);

    /// <summary>Deletes this run.</summary>
    public static Task<BulkDeleteResponse> DeleteAsync(this Run run, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Runs.DeleteAsync([run.Id], cancellationToken);
}
