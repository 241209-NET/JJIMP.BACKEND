using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public enum StatusEnum
{
    Inactive,
    Active,
    Review,
    Complete,
}

public class Issue
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public StatusEnum? Status { get; set; }
    public DateOnly? Deadline { get; set; }

    // Relations
    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public List<Comment> Comments { get; set; } = [];

    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
