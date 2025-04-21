using Microsoft.AspNetCore.Mvc;
using OneIncUserAPI.Core;

namespace OneIncUserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> Logger;
    private readonly ApplicationRepository<AppUser> Repo;

    public UserController(ApplicationRepository<AppUser> repo, ILogger<UserController> logger)
    {
        Repo = repo;
        Logger = logger;
    }

    [HttpGet("{UserId}")]
    public async Task<AppUser?> GetOne(string UserId)
    {
        Logger.LogInformation($"Getting user with ID: {UserId}");

        AppUser? User = await Repo.GetByIdAsync(UserId);

        return User;
    }

    [HttpGet("/all")]
    public async Task<IEnumerable<AppUser>> GetAll()
    {
        Logger.LogInformation("Getting all users"); 
        IEnumerable<AppUser> Users = await Repo.GetAllAsync();

        return Users;
    }

    [HttpPost("/add")]
    public AppUser AddUser(AppUser NewUser)
    {
        Logger.LogInformation($"Adding user with First Name: {NewUser.FirstName}, Last Name: {NewUser.LastName}");    
        AppUser User = new AppUser();

        return User;
    }

    [HttpDelete("{UserId}")]
    public AppUser Get(string UserId)
    {
        Logger.LogInformation($"Deleting user with ID: {UserId}");
        AppUser User = new AppUser();

        return User;
    }
}
