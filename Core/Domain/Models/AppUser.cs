using System.ComponentModel.DataAnnotations;
using OneIncUserAPI.Core.Domain.Common;
using OneIncUserAPI.Core.Domain.Common.Interfaces;

namespace OneIncUserAPI.Core.Domain.Models;

/// <summary>
/// Represents a user in the application, including their unique identifier, personal details, and last login information.
/// </summary>
public class AppUser : EntityBase, IEntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// This property maps to <see cref="UserId"/>.
    /// </summary>
    public new string Id
    {
        get { return UserId; }
        set { UserId = value; }
    }

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    [Key]
    public string UserId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string? FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string? LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string? Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date and time of the user's last login.
    /// </summary>
    public DateTime LastLogin { get; set; } = default!;
}
