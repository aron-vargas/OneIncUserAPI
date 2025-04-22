using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core.Domain.Models;
using OneIncUserAPI.Core.Application.Interfaces;
using OneIncUserAPI.Core.Application;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task<APIResult> GetOne(string RoleId)
    {
        Logger.LogInformation($"Getting role with ID: {RoleId}");
        AppRole? Role = await Repo.GetByIdAsync(RoleId);

        APIResult Result = new APIResult();
        if (Role is null)
        {
            Result = new APIResult()
            {
                Success = false,
                Message = "Role not found.",
                StatusCode = 404
            };
        }
        else
        {
            Result = new APIResult()
            {
                Success = true,
                Message = "Role found.",
                Data = Role,
                StatusCode = 200
            };
        }

        return Result;
    }

    /// <summary>
    /// Retrieves all roles in the system.
    /// </summary>
    /// <returns>A collection of <see cref="AppRole"/> entities.</returns>
    [HttpGet("all")]
    public async Task<APIResult> GetAll()
    {
        Logger.LogInformation("Getting all Roles");
        IEnumerable<AppRole> Roles = await Repo.GetAllAsync();

        APIResult Result = new APIResult();
        if (Roles is null)
        {
            Result = new APIResult()
            {
                Success = false,
                Message = "No Role found.",
                StatusCode = 404
            };
        }
        else
        {
            Result = new APIResult()
            {
                Success = true,
                Message = "Roles found.",
                Data = Roles,
                StatusCode = 200
            };
        }
        return Result;
    }

    /// <summary>
    /// Adds a new role to the system.
    /// </summary>
    /// <param name="NewRole">The <see cref="AppRole"/> entity to add.</param>
    /// <returns>The added <see cref="AppRole"/> entity.</returns>
    [HttpPost("add")]
    public async Task<APIResult> AddRole(AppRole NewRole)
    {
        Logger.LogInformation($"Adding Role with Name: {NewRole.RoleName}, Description: {NewRole.Description}");
        Guid nGuid = Guid.NewGuid();
        NewRole.RoleId = nGuid.ToString();
        AppRole Role = await Repo.AddAsync(NewRole);

        APIResult Result = new APIResult();
        if (Role is null)
        {
            Result = new APIResult()
            {
                Success = false,
                Message = "Role could not be added",
                StatusCode = 404
            };
        }
        else
        {
            Result = new APIResult()
            {
                Success = true,
                Message = "Role was sucessfully added.",
                Data = Role,
                StatusCode = 200
            };
        }
        return Result;
    }

    /// <summary>
    /// Deletes a role by its unique identifier.
    /// </summary>
    /// <param name="RoleId">The unique identifier of the role to delete.</param>
    /// <returns>The deleted <see cref="AppRole"/> entity if found; otherwise, null.</returns>
    [HttpDelete("{RoleId}")]
    public async Task<APIResult> DeleteRole(string RoleId)
    {
        Logger.LogInformation($"Deleting role with ID: {RoleId}");
        AppRole? Role = await Repo.GetByIdAsync(RoleId);

        APIResult Result = new APIResult();
        if (Role is null)
        {
            Logger.LogWarning($"Role not found.");
            Result = new APIResult()
            {
                Success = false,
                Message = "Role could not be found",
                StatusCode = 404
            };
        }
        else
        {
            // Delete the role
            await Repo.DeleteAsync(Role);
            Logger.LogInformation($"Role with ID: {RoleId} was deactivated.");

            Result = new APIResult()
            {
                Success = true,
                Message = "Role was sucessfully removed.",
                Data = Role,
                StatusCode = 200
            };
        }

        return Result;
    }
}
