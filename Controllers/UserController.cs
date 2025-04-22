﻿using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core.Application;
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

    [HttpGet("{UserId}")]
    public async Task<APIResult> GetOne(string UserId)
    {
        Logger.LogInformation($"Getting user with ID: {UserId}");
        APIResult Result = new APIResult();

        try
        {
            AppUser? User = await Repo.GetByIdAsync(UserId);
            if (User is null)
            {
                Result = new APIResult()
                {
                    Success = false,
                    Message = "User not found.",
                    StatusCode = 404
                };
            }
            else
            {
                Result = new APIResult()
                {
                    Success = true,
                    Message = "User found.",
                    Data = User,
                    StatusCode = 200
                };
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, "Error retrieving user with ID: {UserId}", UserId);
            Result = new APIResult()
            {
                Success = false,
                Message = Ex.Message,
                StatusCode = 500
            };
        }

        return Result;
    }

    [HttpGet("all")]
    public async Task<APIResult> GetAll()
    {
        Logger.LogInformation("Getting all users");
        APIResult Result = new APIResult();

        try
        {
            IEnumerable<AppUser> Users = await Repo.GetAllAsync();

            if (User is null)
            {
                Result = new APIResult()
                {
                    Success = false,
                    Message = "No User found.",
                    StatusCode = 404
                };
            }
            else
            {
                Result = new APIResult()
                {
                    Success = true,
                    Message = "Users found.",
                    Data = Users,
                    StatusCode = 200
                };
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, "Error retrieving all users");
            Result = new APIResult()
            {
                Success = false,
                Message = Ex.Message,
                StatusCode = 500
            };
        }
        return Result;
    }

    [HttpPost("add")]
    public async Task<APIResult> AddUser(AppUser NewUser)
    {
        Logger.LogInformation($"Adding user with First Name: {NewUser.FirstName}, Last Name: {NewUser.LastName}");
        APIResult Result = new APIResult();

        try
        {
            if (NewUser.ValidateInsert())
            {
                AppUser User = await Repo.AddAsync(NewUser);
                Result = new APIResult()
                {
                    Success = true,
                    Message = "User was sucessfully added.",
                    Data = User,
                    StatusCode = 200
                };
            }
            else
            { 
                Result = new APIResult()
                {
                    Success = false,
                    Message = "User could not be added",
                    StatusCode = 404
                };
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, "Error while adding user");
            Result = new APIResult()
            {
                Success = false,
                Message = Ex.Message,
                StatusCode = 500
            };
        }

        return Result;
    }

    [HttpPost("update")]
    public async Task<APIResult> UpdateUser(AppUser NewUser)
    {
        Logger.LogInformation($"Updating user with First Name: {NewUser.FirstName}, Last Name: {NewUser.LastName}");
        APIResult Result = new APIResult();

        try
        {
            if (NewUser.ValidateUpdate())
            {
                AppUser User = await Repo.UpdateAsync(NewUser);
                Result = new APIResult()
                {
                    Success = true,
                    Message = "Users was sucessfully updated.",
                    Data = User,
                    StatusCode = 200
                };
            }
            else
            {
                Result = new APIResult()
                {
                    Success = false,
                    Message = "User could not be updated",
                    StatusCode = 404
                };
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, "Error while updating user");
            Result = new APIResult()
            {
                Success = false,
                Message = Ex.Message,
                StatusCode = 500
            };
        }

        return Result;
    }

    [HttpDelete("{UserId}")]
    public async Task<APIResult> DeleteUser(string UserId)
    {
        Logger.LogInformation($"Deleting user with ID: {UserId}");
        APIResult Result = new APIResult();

        try
        {
            AppUser? User = await Repo.GetByIdAsync(UserId);
            if (User is null)
            {
                Logger.LogWarning($"User not found.");
                Result = new APIResult()
                {
                    Success = false,
                    Message = "User could not be found",
                    StatusCode = 404
                };
            }
            else
            {
                // Deactivate to not delete
                User.IsActive = false;
                await Repo.DeleteAsync(User);
                Logger.LogInformation($"User with ID: {UserId} was deactivated.");

                Result = new APIResult()
                {
                    Success = true,
                    Message = "Users was sucessfully removed.",
                    Data = User,
                    StatusCode = 200
                };
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, "Error while deleting user");
            Result = new APIResult()
            {
                Success = false,
                Message = Ex.Message,
                StatusCode = 500
            };
        }

        return Result;
    }
}
