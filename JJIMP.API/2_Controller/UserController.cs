using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IProjectService _projectService;

    public UserController(IUserService userService, IProjectService projectService)
    {
        _userService = userService;
        _projectService = projectService;
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await _projectService.GetProjectsByUserId(id);
        return Ok(user);
    }

    [HttpGet("{id}/projects")]
    public async Task<ActionResult> GetUserProjects(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var user = await _userService.GetAllUsers();
        return Ok(user);
    }

    [HttpGet("{id}/info")]
    public async Task<ActionResult> GetUserInfo(int id)
    {
        var user = await _userService.GetUserInfoById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserDTO userDTO)
    {
        var user = await _userService.CreateUser(userDTO);
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(UpdateUserDTO userDTO)
    {
        var user = await _userService.UpdateUser(userDTO);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUserById(id);
        return Ok(user);
    }
}