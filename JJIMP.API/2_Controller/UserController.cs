using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JJIMP.API.DTO;
using JJIMP.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpGet("{id}"), Authorize]
    public async Task<ActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    // GET: api/User/current - adding for easy token use, decodes here
    [HttpGet("current"), Authorize]
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

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserDTO userDTO)
    {
        var user = await _userService.CreateUser(userDTO);
        return Ok(user);
    }

    // POST: api/User/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userDto)
    {
        // Check for valid credentials
        var user = await _userService.AuthenticateUser(userDto);
        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        // Generate JWT token
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser(UpdateUserDTO userDTO)
    {
        try
        {
            var user = await _userService.UpdateUser(userDTO);
            return Ok(user);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUserById(id);
        return Ok(user);
    }
}
