using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core;

namespace OneIncUserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> Logger;
    private readonly ApplicationRepository<AppRole> Repo;

    public RoleController(ApplicationRepository<AppRole> repo, ILogger<RoleController> logger)
    {
        Repo = repo;
        Logger = logger;
    }

    [HttpGet("{RoleId}")]
    public async Task<AppRole?> GetOne(string RoleId)
    {
        Logger.LogInformation($"Getting role with ID: {RoleId}");

        AppRole? Role = await Repo.GetByIdAsync(RoleId);

        return Role;
    }

    [HttpGet("/all")]
    public async Task<IEnumerable<AppRole>> GetAll()
    {
        Logger.LogInformation("Getting all Roles");
        IEnumerable<AppRole> Roles = await Repo.GetAllAsync();

        return Roles;
    }

    [HttpPost("/add")]
    public AppRole AddUser(AppRole NewRole)
    {
        Logger.LogInformation($"Adding Role with Name: {NewRole.RoleName}, Description: {NewRole.Description}");
        AppRole Role = new AppRole();

        return Role;
    }

    [HttpDelete("{RoleId}")]
    public AppRole Get(string RoleId)
    {
        Logger.LogInformation($"Deleting role with ID: {RoleId}");
        AppRole Role = new AppRole();

        return Role;
    }
}