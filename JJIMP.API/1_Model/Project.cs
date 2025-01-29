using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class Project
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Relations
    public int ProjectManagerId { get; set; }
    public User ProjectManager { get; set; } = null!;
    public List<User> Users { get; set; } = [];
    public List<Issue> Issues { get; set; } = [];

    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
