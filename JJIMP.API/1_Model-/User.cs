using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

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