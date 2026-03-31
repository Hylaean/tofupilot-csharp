using Hylaean.TofuPilot.Models.Common;
using Hylaean.TofuPilot.Models.Units;

namespace Hylaean.TofuPilot;

/// <summary>
/// Fluent extension methods for <see cref="Unit"/>.
/// </summary>
public static class UnitExtensions
{
    /// <summary>Adds a child unit to this unit.</summary>
    public static Task<Unit> AddChildAsync(this Unit unit, TofuPilotClient client, string childSerialNumber, CancellationToken cancellationToken = default) =>
        client.Units.AddChildAsync(unit.SerialNumber!, childSerialNumber, cancellationToken);

    /// <summary>Removes a child unit from this unit.</summary>
    public static Task<Unit> RemoveChildAsync(this Unit unit, TofuPilotClient client, string childSerialNumber, CancellationToken cancellationToken = default) =>
        client.Units.RemoveChildAsync(unit.SerialNumber!, childSerialNumber, cancellationToken);

    /// <summary>Updates this unit.</summary>
    public static Task<Unit> UpdateAsync(this Unit unit, TofuPilotClient client, UpdateUnitRequest request, CancellationToken cancellationToken = default) =>
        client.Units.UpdateAsync(unit.SerialNumber!, request, cancellationToken);

    /// <summary>Re-fetches this unit from the API.</summary>
    public static Task<Unit> RefreshAsync(this Unit unit, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Units.GetAsync(unit.SerialNumber!, cancellationToken);

    /// <summary>Deletes this unit.</summary>
    public static Task<BulkDeleteResponse> DeleteAsync(this Unit unit, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Units.DeleteAsync(unit.SerialNumber!, cancellationToken);
}
