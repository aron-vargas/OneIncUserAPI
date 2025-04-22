using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core.Application.Interfaces;
using OneIncUserAPI.Core.Application;
using OneIncUserAPI.Core.Domain.Models;
using OneIncUserAPI.Core.Persistence;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OneIncUserAPI.Controllers;

/// <summary>
/// Controller for managing user roles in the application.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly ILogger<UserRoleController> Logger;
    private readonly IApplicationRepository<UserRole> Repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleController"/> class.
    /// </summary>
    /// <param name="repo">The repository for managing <see cref="UserRole"/> entities.</param>
    /// <param name="logger">The logger for logging information and errors.</param>
    public UserRoleController(IApplicationRepository<UserRole> repo, ILogger<UserRoleController> logger)
    {
        Repo = repo;
        Logger = logger;
    }

    /// <summary>
    /// Adds a new userrole to the system.
    /// </summary>
    /// <param name="Entity">The <see cref="UserRole"/> entity to add.</param>
    /// <returns>The added <see cref="UserRole"/> entity.</returns>
    [HttpPost("add")]
    public async Task<APIResult> AddUserRole(UserRole Entity)
    {
        Logger.LogInformation($"Adding User Role with UserId: {Entity.UserId}, RoleId: {Entity.RoleId}");
        await Repo.AddAsync(Entity);

        APIResult Result = new APIResult();
        if (Entity is null)
        {
            Result = new APIResult()
            {
                Success = false,
                Message = "UserRole not found.",
                StatusCode = 404
            };
        }
        else
        {
            Result = new APIResult()
            {
                Success = true,
                Message = "UserRole found.",
                Data = Entity,
                StatusCode = 200
            };
        }

        return Result;
    }

    /// <summary>
    /// Removes an existing userrole from the system.
    /// </summary>
    /// <param name="Entity">The <see cref="UserRole"/> entity to remove.</param>
    /// <returns>The removed <see cref="UserRole"/> entity.</returns>
    [HttpDelete("remove")]
    public async Task<APIResult> RemoveUserRole(UserRole Entity)
    {
        Logger.LogInformation($"Removing User Role with UserId: {Entity.UserId}, RoleId: {Entity.RoleId}");
        await Repo.DeleteAsync(Entity);

        APIResult Result = new APIResult()
        {
            Success = true,
            Message = "UserRole was sucessfully removed.",
            Data = Entity,
            StatusCode = 200
        };

        return Result;
    }

    /// <summary>
    /// Retrieves all userroles associated with a specific user.
    /// </summary>
    /// <param name="UserId">The ID of the user.</param>
    /// <returns>A collection of <see cref="UserRole"/> entities associated with the user.</returns>
    [HttpGet("allroles/{UserId}")]
    public Task<APIResult> GetAllRoles(string UserId)
    {
        Logger.LogInformation($"Getting all UserRoles with UserId {UserId}");
        IEnumerable<UserRole> Entities = Repo.Entities.Where(e => e.UserId == UserId);

        APIResult Result = new APIResult();
        if (Entities is null)
        {
            Result = new APIResult()
            {
                Success = false,
                Message = "No UserRole found.",
                StatusCode = 404
            };
        }
        else
        {
            Result = new APIResult()
            {
                Success = true,
                Message = "UserRoles found.",
                Data = Entities,
                StatusCode = 200
            };
        }
        return Task.FromResult(Result);
    }

    /// <summary>
    /// Retrieves all userroles associated with a specific role.
    /// </summary>
    /// <param name="RoleId">The ID of the role.</param>
    /// <returns>A collection of <see cref="UserRole"/> entities associated with the role.</returns>
    [HttpGet("allusers/{RoleId}")]
    public Task<APIResult> GetAllUsers(string RoleId)
    {
        Logger.LogInformation($"Getting all UserRoles with RoleId: {RoleId}");
        IEnumerable<UserRole> Entities = Repo.Entities.Where(e => e.RoleId == RoleId);

        APIResult Result = new APIResult();
        if (Entities is null)
        {
            Result = new APIResult()
            {
                Success = false,
                Message = "No UserRole found.",
                StatusCode = 404
            };
        }
        else
        {
            Result = new APIResult()
            {
                Success = true,
                Message = "UserRoles found.",
                Data = Entities,
                StatusCode = 200
            };
        }
        return Task.FromResult(Result);
    }
}
