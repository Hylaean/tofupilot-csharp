namespace Hylaean.TofuPilot.Models.Users;

/// <summary>
/// Represents a TofuPilot user.
/// </summary>
public record User
{
    /// <summary>Gets the unique identifier.</summary>
    public required string Id { get; init; }

    /// <summary>Gets the email address.</summary>
    public required string Email { get; init; }

    /// <summary>Gets the display name.</summary>
    public string? Name { get; init; }

    /// <summary>Gets the profile image URL.</summary>
    public string? Image { get; init; }

    /// <summary>Gets whether the user is banned.</summary>
    public bool Banned { get; init; }
}

/// <summary>
/// Request parameters for listing users.
/// </summary>
public record ListUsersRequest
{
    /// <summary>
    /// If true, returns only the current authenticated user.
    /// </summary>
    public bool? Current { get; init; }
}
