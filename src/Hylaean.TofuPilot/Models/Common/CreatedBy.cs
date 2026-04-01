namespace Hylaean.TofuPilot.Models.Common;

/// <summary>
/// Represents the user who created a resource.
/// </summary>
public record CreatedByUser
{
    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the user's display name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the user's email address.
    /// </summary>
    public string? Email { get; init; }
}

/// <summary>
/// Represents the station that created a resource.
/// </summary>
public record CreatedByStation
{
    /// <summary>
    /// Gets the station ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the station name.
    /// </summary>
    public string? Name { get; init; }
}
