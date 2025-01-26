using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserInDTO userDTO)
    {
        var user = await _userService.CreateUser(userDTO);
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(UserInDTO userDTO)
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