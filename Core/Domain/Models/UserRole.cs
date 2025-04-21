using System.ComponentModel.DataAnnotations;
using OneIncUserAPI.Core.Domain.Common;
using OneIncUserAPI.Core.Domain.Common.Interfaces;

namespace OneIncUserAPI.Core.Domain.Models;

/// <summary>
/// Represents the association between a user and a role in the application.
/// </summary>
public class UserRole : EntityBase, IEntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the user-role association.
    /// </summary>
    [Key]
    public new string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the role.
    /// </summary>
    public string UserId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier of the role associated with the user.
    /// </summary>
    public string RoleId { get; set; } = default!;
}
