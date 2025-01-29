using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JJIMP.API.Model;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    // Relations
    public List<Project> Projects { get; set; } = [];
    public List<Issue> CreatedIssues { get; set; } = [];
    public List<Issue> AssignedIssues { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<Project> ManagedProjects { get; set; } = [];
}
