using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OneIncUserAPI.Core.Domain.Common;
using OneIncUserAPI.Core.Domain.Common.Interfaces;

namespace OneIncUserAPI.Core.Domain.Models;

/// <summary>
/// Represents a role in the application, including its unique identifier, name, and description.
/// </summary>
public class AppRole : EntityBase, IEntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the role.
    /// This property maps to <see cref="RoleId"/>.
    /// </summary>
    [NotMapped]
    public new string Id
    {
        get { return RoleId; }
        set { RoleId = value; }
    }

    /// <summary>
    /// Gets or sets the unique identifier for the role.
    /// </summary>
    [Key]
    public string RoleId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public string RoleName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the description of the role.
    /// </summary>
    public string? Description { get; set; } = default!;
}
