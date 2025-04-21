using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core.Domain.Models;
using OneIncUserAPI.Core.Application.Interfaces;

namespace OneIncUserAPI.Controllers;

/// <summary>
/// Controller for managing roles in the application.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> Logger;
    private readonly IApplicationRepository<AppRole> Repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleController"/> class.
    /// </summary>
    /// <param name="repo">The repository for managing <see cref="AppRole"/> entities.</param>
    /// <param name="logger">The logger for logging information and errors.</param>
    public RoleController(IApplicationRepository<AppRole> repo, ILogger<RoleController> logger)
    {
        Repo = repo;
        Logger = logger;
    }

    /// <summary>
    /// Retrieves a role by its unique identifier.
    /// </summary>
    /// <param name="RoleId">The unique identifier of the role.</param>
    /// <returns>The <see cref="AppRole"/> entity if found; otherwise, null.</returns>
    [HttpGet("{RoleId}")]
    public async Task<AppRole?> GetOne(string RoleId)
    {
        Logger.LogInformation($"Getting role with ID: {RoleId}");
        AppRole? Role = await Repo.GetByIdAsync(RoleId);
        return Role;
    }

    /// <summary>
    /// Retrieves all roles in the system.
    /// </summary>
    /// <returns>A collection of <see cref="AppRole"/> entities.</returns>
    [HttpGet("all")]
    public async Task<IEnumerable<AppRole>> GetAll()
    {
        Logger.LogInformation("Getting all Roles");
        IEnumerable<AppRole> Roles = await Repo.GetAllAsync();
        return Roles;
    }

    /// <summary>
    /// Adds a new role to the system.
    /// </summary>
    /// <param name="NewRole">The <see cref="AppRole"/> entity to add.</param>
    /// <returns>The added <see cref="AppRole"/> entity.</returns>
    [HttpPost("add")]
    public async Task<AppRole> AddRole(AppRole NewRole)
    {
        Logger.LogInformation($"Adding Role with Name: {NewRole.RoleName}, Description: {NewRole.Description}");
        NewRole.RoleId = new Guid().ToString();
        AppRole Role = await Repo.AddAsync(NewRole);
        return Role;
    }

    /// <summary>
    /// Deletes a role by its unique identifier.
    /// </summary>
    /// <param name="RoleId">The unique identifier of the role to delete.</param>
    /// <returns>The deleted <see cref="AppRole"/> entity if found; otherwise, null.</returns>
    [HttpDelete("{RoleId}")]
    public async Task<AppRole?> DeleteRole(string RoleId)
    {
        Logger.LogInformation($"Deleting role with ID: {RoleId}");
        AppRole? Role = await Repo.GetByIdAsync(RoleId);

        if (Role is not null)
            await Repo.DeleteAsync(Role);

        return Role;
    }
}
