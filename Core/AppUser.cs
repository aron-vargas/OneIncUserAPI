namespace OneIncUserAPI.Core;

public class AppUser : EntityBase
{
    public new string Id
    {
        get { return UserId; }
        set { UserId = value; }
    }
    public string UserId { get; set; } = default!;
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public DateTime LastLogin { get; set; } = default!;

}
