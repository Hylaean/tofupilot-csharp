using Hylaean.Tofupilot.Models.Common;
using Hylaean.Tofupilot.Models.Stations;

namespace Hylaean.Tofupilot;

/// <summary>
/// Fluent extension methods for <see cref="Station"/>.
/// </summary>
public static class StationExtensions
{
    /// <summary>Updates this station.</summary>
    public static Task<Station> UpdateAsync(this Station station, TofuPilotClient client, UpdateStationRequest request, CancellationToken cancellationToken = default) =>
        client.Stations.UpdateAsync(station.Id, request, cancellationToken);

    /// <summary>Removes this station.</summary>
    public static Task<DeleteResponse> RemoveAsync(this Station station, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Stations.RemoveAsync(station.Id, cancellationToken);

    /// <summary>Re-fetches this station from the API.</summary>
    public static Task<Station> RefreshAsync(this Station station, TofuPilotClient client, CancellationToken cancellationToken = default) =>
        client.Stations.GetAsync(station.Id, cancellationToken);
}
