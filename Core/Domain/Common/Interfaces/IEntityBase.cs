namespace OneIncUserAPI.Core.Domain.Common.Interfaces;

public interface IEntityBase
{
    public string Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdateOn { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public bool ValidateInsert();

    public bool ValidateUpdate();
}

