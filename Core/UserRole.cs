namespace OneIncUserAPI.Core;

public class UserRole : EntityBase
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
}
