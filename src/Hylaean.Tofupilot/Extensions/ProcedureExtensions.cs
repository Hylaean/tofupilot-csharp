using Hylaean.Tofupilot.Models.Common;
using Hylaean.Tofupilot.Models.Procedures;

namespace Hylaean.Tofupilot;

/// <summary>
/// Fluent extension methods for <see cref="Procedure"/>.
/// </summary>
public static class ProcedureExtensions
{
    /// <summary>Updates this procedure.</summary>
    public static Task<Procedure> UpdateAsync(this Procedure proc, TofuPilotClient client, UpdateProcedureRequest request, CancellationToken cancellationToken = default) =>
        client.Procedures.UpdateAsync(proc.Id, request, cancellationToken);

    /// <summary>Deletes this procedure.</summary>
    public static Task<DeleteResponse> DeleteAsync(this Procedure proc, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Procedures.DeleteAsync(proc.Id, cancellationToken);

    /// <summary>Re-fetches this procedure from the API.</summary>
    public static Task<Procedure> RefreshAsync(this Procedure proc, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Procedures.GetAsync(proc.Id, cancellationToken);

    /// <summary>Creates a new version for this procedure.</summary>
    public static Task<ProcedureVersion> CreateVersionAsync(this Procedure proc, TofuPilotClient client, CreateProcedureVersionRequest request, CancellationToken cancellationToken = default) =>
        client.Procedures.Versions.CreateAsync(proc.Id, request, cancellationToken);
}
