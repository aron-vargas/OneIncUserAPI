using System.ComponentModel.DataAnnotations.Schema;

namespace OneIncUserAPI.Core;

public class AppRole : EntityBase
{
    [NotMapped]
    public new string Id 
    {
        get { return RoleId; }
        set { RoleId = value; } 
    }
    public string RoleId { get; set; } = default!;
    public string RoleName { get; set; } = default!;
    public string? Description { get; set; } = default!;
}
