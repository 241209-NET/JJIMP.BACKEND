using System.ComponentModel.DataAnnotations;

namespace JJIMP.API.DTO;

public class UserInDTO
{
    [Required]
    public required int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}

public class UserOutDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}