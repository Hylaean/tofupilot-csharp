using Hylaean.Tofupilot.Models.Users;

namespace Hylaean.Tofupilot.Resources;

/// <summary>
/// Resource for listing users.
/// </summary>
public sealed class UsersResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/users";

    /// <summary>Lists users in the organization.</summary>
    public async Task<IReadOnlyList<User>> ListAsync(
        ListUsersRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListUsersRequest();

        var queryParams = new Dictionary<string, string?>
        {
            ["current"] = request.Current?.ToString().ToLowerInvariant(),
        };

        var uri = BuildUri(BasePath, queryParams);
        return await HttpClient.GetAsync<List<User>>(uri, cancellationToken).ConfigureAwait(false);
    }
}
