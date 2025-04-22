using OneIncUserAPI.Core.Domain.Common.Interfaces;

namespace OneIncUserAPI.Core.Domain.Common;

/// <summary>
/// Represents the base class for all entities in the application, providing common properties and methods.
/// </summary>
public class EntityBase : IEntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime? CreatedOn { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user who created the entity.
    /// </summary>
    public string CreatedBy { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    public DateTime? UpdateOn { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user who last updated the entity.
    /// </summary>
    public string? UpdatedBy { get; set; } = default!;

    /// <summary>
    /// Gets or sets a value indicating whether the entity is active.
    /// </summary>
    public bool IsActive { get; set; } = default!;

    /// <summary>
    /// Validates the entity before it is inserted into the database.
    /// Sets default values for <see cref="CreatedOn"/>, <see cref="UpdateOn"/>, <see cref="CreatedBy"/>, <see cref="UpdatedBy"/>, and <see cref="IsActive"/>.
    /// </summary>
    public bool ValidateInsert()
    {
        bool Valid = true;

        CreatedOn = DateTime.UtcNow;
        UpdateOn = DateTime.UtcNow;
        if (CreatedBy is null)
            CreatedBy = "System";
        if (UpdatedBy is null)
            UpdatedBy = "System";
        IsActive = true;

        return Valid;
    }

    /// <summary>
    /// Validates the entity before it is updated in the database.
    /// Ensures that <see cref="UpdateOn"/> and <see cref="UpdatedBy"/> are set, and sets default values for <see cref="CreatedOn"/> and <see cref="CreatedBy"/> if they are null.
    /// </summary>
    public bool ValidateUpdate()
    {
        bool Valid = true;

        UpdateOn = DateTime.UtcNow;
        if (UpdatedBy is null)
            UpdatedBy = "System";
        if (CreatedOn is null)
            CreatedOn = DateTime.UtcNow;
        if (CreatedBy is null)
            CreatedBy = "System";

        return Valid;
    }
}
