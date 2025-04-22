using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using OneIncUserAPI.Core.Domain.Common;
using OneIncUserAPI.Core.Domain.Common.Interfaces;

namespace OneIncUserAPI.Core.Domain.Models;

/// <summary>
/// Represents a user in the application, including their unique identifier, personal details, and last login information.
/// </summary>
public class AppUser : EntityBase, IEntityBase
{
    public string? FirstName { get; set; } = default!;

    public string? LastName { get; set; } = default!;

    public string? Email { get; set; } = default!;

    public DateTime LastLogin { get; set; } = default!;


    /// <summary>
    /// Validates the entity before it is inserted into the database.
    /// Sets default values for <see cref="CreatedOn"/>, <see cref="UpdateOn"/>, <see cref="CreatedBy"/>, <see cref="UpdatedBy"/>, and <see cref="IsActive"/>.
    /// </summary>
    public new bool ValidateInsert()
    {
        ValidateUser();

        bool Valid = base.ValidateInsert();

        // Assign a new GUID to Id
        Guid NewGuid = Guid.NewGuid();
        Id = NewGuid.ToString();

        return Valid;
    }

    /// <summary>
    /// Validates the entity before it is updated in the database.
    /// Ensures that <see cref="UpdateOn"/> and <see cref="UpdatedBy"/> are set, and sets default values for <see cref="CreatedOn"/> and <see cref="CreatedBy"/> if they are null.
    /// </summary>
    public new bool ValidateUpdate()
    { 
        ValidateUser();
        bool Valid = base.ValidateUpdate();
        return Valid;
    }

    /// <summary>
    /// Validate email, first and last name before inserting or updating the user.
    /// </summary>
    public void ValidateUser()
    {
        if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
            throw new ArgumentNullException("Invalid email address");

        if (string.IsNullOrEmpty(FirstName))
            throw new ArgumentNullException("FistName cannot be empty");

        if (string.IsNullOrEmpty(LastName))
            throw new ArgumentNullException("FistName cannot be empty");
    }

    private bool IsValidEmail(string Email)
    {
        try
        {
            MailAddress Addr = new System.Net.Mail.MailAddress(Email);
            return Addr.Address == Email;
        }
        catch
        {
            return false;
        }
    }
}
