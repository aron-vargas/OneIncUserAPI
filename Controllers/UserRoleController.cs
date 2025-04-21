using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core;

namespace OneIncUserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly ILogger<UserRoleController> Logger;
    private readonly ApplicationRepository<UserRole> Repo;

    public UserRoleController(ApplicationRepository<UserRole> repo, ILogger<UserRoleController> logger)
    {
        Repo = repo;
        Logger = logger;
    }

    [HttpPost("/add")]
    public async Task<UserRole> AddUser(UserRole Entity)
    {
        Logger.LogInformation($"Adding User Role with UserId: {Entity.UserId}, RoleId: {Entity.RoleId}");
   
        await Repo.AddAsync(Entity);

        return Entity;
    }

    [HttpGet("/allroles/{UserId}")]
    public async Task<IEnumerable<AppRole>> GetAll(string UserId)
    {
        Logger.LogInformation("Getting all Roles with UserId {UserId}");
        IEnumerable<AppRole> Roles = await Repo.Find();

        return Roles;
    }

    [HttpGet("/allusers/{RoleId}")]
    public async Task<IEnumerable<AppRole>> GetAll(string RoleId)
    {
        Logger.LogInformation("Getting all users with RoleId: {RoleId}");
        IEnumerable<AppRole> Roles = await Repo.Find();

        return Roles;
    }
}