using System.ComponentModel.DataAnnotations;
using JJIMP.API.Model;

namespace JJIMP.API.DTO;

public class CreateUserDTO
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}

public class UpdateUserDTO
{
    [Required]
    public required int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginUserDTO
{
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
    public List<ProjectOutDTO> Projects { get; set; } = [];
    public List<IssueOutDTO> CreatedIssues { get; set; } = [];
    public List<IssueOutDTO> AssignedIssues { get; set; } = [];
    public List<CommentOutDTO> Comments { get; set; } = [];
    public List<ProjectOutDTO> ManagedProjects { get; set; } = [];
}

public class PartialUserOutDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}