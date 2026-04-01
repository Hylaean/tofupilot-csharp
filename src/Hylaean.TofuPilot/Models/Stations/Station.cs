using System.Text.Json.Serialization;

namespace Hylaean.TofuPilot.Models.Stations;

/// <summary>
/// Represents a test station.
/// </summary>
public record Station
{
    /// <summary>
    /// Gets the unique identifier of the station.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets the station name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the API key prefix (full key only shown on creation).
    /// </summary>
    public string? ApiKey { get; init; }

    /// <summary>
    /// Gets the procedures linked to this station.
    /// </summary>
    public IReadOnlyList<StationProcedure>? Procedures { get; init; }

    /// <summary>
    /// Gets the total number of procedures (LIST only).
    /// </summary>
    public int? ProceduresCount { get; init; }

    /// <summary>
    /// Gets the organization slug.
    /// </summary>
    public string? OrganizationSlug { get; init; }

    /// <summary>
    /// Gets the connection status.
    /// </summary>
    public string? ConnectionStatus { get; init; }

    /// <summary>
    /// Gets the team this station belongs to.
    /// </summary>
    public StationTeam? Team { get; init; }
}

/// <summary>
/// Represents a procedure linked to a station.
/// </summary>
public record StationProcedure
{
    /// <summary>
    /// Gets the procedure ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the procedure identifier (slug).
    /// </summary>
    public string? Identifier { get; init; }

    /// <summary>
    /// Gets the procedure name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the number of runs in the last 7 days (GET/current only).
    /// </summary>
    public int? RunsCount { get; init; }

    /// <summary>
    /// Gets the deployment information (GET/current only).
    /// </summary>
    public StationProcedureDeployment? Deployment { get; init; }
}

/// <summary>
/// Represents deployment info for a procedure on a station.
/// </summary>
public record StationProcedureDeployment
{
    /// <summary>
    /// Gets when the procedure was deployed.
    /// </summary>
    public string? DeployedAt { get; init; }

    /// <summary>
    /// Gets the commit information.
    /// </summary>
    public StationDeploymentCommit? Commit { get; init; }

    /// <summary>
    /// Gets the repository information.
    /// </summary>
    public StationDeploymentRepository? Repository { get; init; }
}

/// <summary>
/// Represents a git commit in a station deployment.
/// </summary>
public record StationDeploymentCommit
{
    /// <summary>
    /// Gets the commit SHA.
    /// </summary>
    public string? Sha { get; init; }

    /// <summary>
    /// Gets the commit message.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Gets the branch name.
    /// </summary>
    public string? Branch { get; init; }
}

/// <summary>
/// Represents a repository in a station deployment.
/// </summary>
public record StationDeploymentRepository
{
    /// <summary>
    /// Gets the repository owner.
    /// </summary>
    public string? Owner { get; init; }

    /// <summary>
    /// Gets the repository name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the git provider.
    /// </summary>
    public string? Provider { get; init; }

    /// <summary>
    /// Gets the GitLab project ID (only for GitLab repos).
    /// </summary>
    public long? GitlabProjectId { get; init; }
}

/// <summary>
/// Represents a team assigned to a station.
/// </summary>
public record StationTeam
{
    /// <summary>
    /// Gets the team ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the team name.
    /// </summary>
    public string? Name { get; init; }
}

/// <summary>
/// Request to create a station.
/// </summary>
public record CreateStationRequest
{
    /// <summary>
    /// Gets or sets the station name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the station description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request to update a station.
/// </summary>
public record UpdateStationRequest
{
    /// <summary>
    /// Gets or sets the station name.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the station description.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
}

/// <summary>
/// Request parameters for listing stations.
/// </summary>
public record ListStationsRequest
{
    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? SearchQuery { get; init; }

    /// <summary>
    /// Gets or sets specific station IDs to filter by.
    /// </summary>
    public IReadOnlyList<string>? Ids { get; init; }

    /// <summary>
    /// Gets or sets the page size limit.
    /// </summary>
    public int? Limit { get; init; } = 50;

    /// <summary>
    /// Gets or sets the cursor for pagination.
    /// </summary>
    public double? Cursor { get; init; }
}
