using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core.Application.Interfaces;
using OneIncUserAPI.Core.Domain.Models;
using OneIncUserAPI.Core.Persistence;

namespace OneIncUserAPI.Controllers;

/// <summary>
/// Controller for managing user-related operations in the application.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> Logger;
    private readonly IApplicationRepository<AppUser> Repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="repo">The repository for managing <see cref="AppUser"/> entities.</param>
    /// <param name="logger">The logger for logging information and errors.</param>
    public UserController(IApplicationRepository<AppUser> repo, ILogger<UserController> logger)
    {
        Repo = repo;
        Logger = logger;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <returns>The <see cref="AppUser"/> entity if found; otherwise, null.</returns>
    [HttpGet("{UserId}")]
    public async Task<AppUser?> GetOne(string UserId)
    {
        Logger.LogInformation($"Getting user with ID: {UserId}");
        AppUser? User = await Repo.GetByIdAsync(UserId);
        return User;
    }

    /// <summary>
    /// Retrieves all users in the system.
    /// </summary>
    /// <returns>A collection of <see cref="AppUser"/> entities.</returns>
    [HttpGet("all")]
    public async Task<IEnumerable<AppUser>> GetAll()
    {
        Logger.LogInformation("Getting all users");
        IEnumerable<AppUser> Users = await Repo.GetAllAsync();
        return Users;
    }

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <param name="NewUser">The <see cref="AppUser"/> entity to add.</param>
    /// <returns>The added <see cref="AppUser"/> entity.</returns>
    [HttpPost("add")]
    public async Task<AppUser> AddUser(AppUser NewUser)
    {
        Logger.LogInformation($"Adding user with First Name: {NewUser.FirstName}, Last Name: {NewUser.LastName}");
        NewUser.UserId = new Guid().ToString();
        AppUser User = await Repo.AddAsync(NewUser);
        return User;
    }

    /// <summary>
    /// Updates an existing user in the system.
    /// </summary>
    /// <param name="NewUser">The <see cref="AppUser"/> entity with updated information.</param>
    /// <returns>The updated <see cref="AppUser"/> entity.</returns>
    [HttpPost("update")]
    public async Task<AppUser> UpdateUser(AppUser NewUser)
    {
        Logger.LogInformation($"Updating user with First Name: {NewUser.FirstName}, Last Name: {NewUser.LastName}");
        AppUser User = await Repo.AddAsync(NewUser);
        return User;
    }

    /// <summary>
    /// Deactivates a user by their unique identifier.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user to deactivate.</param>
    /// <returns>The deactivated <see cref="AppUser"/> entity if found; otherwise, null.</returns>
    [HttpDelete("{UserId}")]
    public async Task<AppUser?> DeleteUser(string UserId)
    {
        Logger.LogInformation($"Deleting user with ID: {UserId}");
        AppUser? User = await Repo.GetByIdAsync(UserId);
        if (User is not null)
        {
            // Deactivate to not delete
            User.IsActive = false;
            await Repo.UpdateAsync(User);
        }
        else
        {
            Logger.LogWarning($"User not found.");
        }
        return User;
    }

    /// <summary>
    /// Throws an exception to test the error middleware.
    /// </summary>
    /// <param name="NewUser">A sample <see cref="AppUser"/> entity.</param>
    /// <exception cref="Exception">Always thrown to test middleware.</exception>
    [HttpPost("Test")]
    public Task TestErrorMiddleware(AppUser NewUser)
    {
        throw new Exception("Testing middleware");
    }
}
