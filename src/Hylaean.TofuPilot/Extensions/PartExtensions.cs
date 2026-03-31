using Hylaean.TofuPilot.Models.Common;
using Hylaean.TofuPilot.Models.Parts;

namespace Hylaean.TofuPilot;

/// <summary>
/// Fluent extension methods for <see cref="Part"/>.
/// </summary>
public static class PartExtensions
{
    /// <summary>Updates this part.</summary>
    public static Task<Part> UpdateAsync(this Part part, TofuPilotClient client, UpdatePartRequest request, CancellationToken cancellationToken = default) =>
        client.Parts.UpdateAsync(part.PartNumber!, request, cancellationToken);

    /// <summary>Deletes this part and its revisions.</summary>
    public static Task<DeletePartResponse> DeleteAsync(this Part part, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Parts.DeleteAsync(part.PartNumber!, cancellationToken);

    /// <summary>Re-fetches this part from the API.</summary>
    public static Task<Part> RefreshAsync(this Part part, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Parts.GetAsync(part.PartNumber!, cancellationToken);

    /// <summary>Creates a new revision for this part.</summary>
    public static Task<PartRevision> CreateRevisionAsync(this Part part, TofuPilotClient client, CreatePartRevisionRequest request, CancellationToken cancellationToken = default) =>
        client.Parts.Revisions.CreateAsync(part.PartNumber!, request, cancellationToken);
}
