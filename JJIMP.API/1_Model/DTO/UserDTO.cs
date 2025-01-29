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

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}

public class UserOutDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public List<Project> Projects { get; set; } = [];
    public List<Issue> CreatedIssues { get; set; } = [];
    public List<Issue> AssignedIssues { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<Project> ManagedProjects { get; set; } = [];
}
