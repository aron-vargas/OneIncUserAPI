namespace OneIncUserAPI.Core;

public class EntityBase
{
    public string Id { get; set; } = default!;
    public DateTime? CreatedOn { get; set; } = default!;
    public string CreatedBy { get; set; } = default!;
    public DateTime? UpdateOn { get; set; } = default!;
    public string? UpdatedBy { get; set; } = default!;  
    public bool IsActive { get; set; } = default!;

    public void ValidateInsert(AppUser SystemUser)
    {
        CreatedOn = DateTime.UtcNow;
        UpdateOn = DateTime.UtcNow;
        CreatedBy = SystemUser.UserId ?? "System";
        UpdatedBy = SystemUser.UserId ?? "System";
        IsActive = true;
    }
    public void ValidateUpdate(AppUser SystemUser)
    {
        UpdateOn = DateTime.UtcNow;
        UpdatedBy = SystemUser.UserId ?? "System";

        if (CreatedOn is null)
        {
            CreatedOn = DateTime.UtcNow;
            CreatedBy = SystemUser.UserId ?? "System";
        }
    }   
}
