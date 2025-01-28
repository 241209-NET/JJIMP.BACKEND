using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IProjectService _projectService;

    public UserController(IUserService userService, IConfiguration configuration, IProjectService projectService)
    {
        _userService = userService;
        _configuration = configuration;
        _projectService = projectService;
    }

    
    [HttpGet("id/{id}"), Authorize]
    public async Task<ActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }
    [HttpGet("username/{username}"), Authorize]
    public async Task<ActionResult> GetUserByName(string username)
    {
        var user = await _userService.GetUserByName(username);
        return Ok(user);
    }

    [HttpGet("{id}/projects")]
    public async Task<ActionResult> GetUserProjects(int id)
    {
        var user = await _projectService.GetProjectsByUserId(id);
        return Ok(user);
    }
    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var user = await _userService.GetAllUsers();
        return Ok(user);
    }
    
    // GET: api/User/current - adding for easy token use, decodes here
    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            // Extract the user ID from the JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Invalid or missing token." });
            }

            // Parse the user ID from the claim
            var userId = int.Parse(userIdClaim.Value);

            // Retrieve the user details using the user ID
            var user = await _userService.GetUserById(userId)!;
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "An unexpected error occurred.", details = ex.Message }
            );
        }
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
    // POST: api/User/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CreateUserDTO userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Name))
            return BadRequest("Username cannot be empty.");

        // 1) Look up user by username //Update the GetUserByUsername in Controller with GetUserWithToken
        var user = await _userService.GetUserByName(userDto.Name)!;
        if (user == null)
            return NotFound("User not found.");

        // 2) verify password
        if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
        {
            return Unauthorized("Invalid credentials.");
        }

        if (user != null)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.Name!.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
            );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenValue, User = user });
        }
        return NoContent();
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