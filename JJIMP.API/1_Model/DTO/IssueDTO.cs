using System.ComponentModel.DataAnnotations;
using JJIMP.API.Model;

namespace JJIMP.API.DTO;

public class CreateIssueDTO
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public DateOnly? Deadline { get; set; }
    public int? AssigneeId { get; set; }
    [Required]
    public required int CreatedById { get; set; }
    [Required]
    public required int ProjectId { get; set; }
}

public class UpdateIssueDTO
{
    [Required]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public StatusEnum? Status { get; set; }
    public DateOnly? Deadline { get; set; }
    public int? AssigneeId { get; set; }
}

public class IssueOutDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public StatusEnum Status { get; set; }
    public DateOnly? Deadline { get; set; }
    public User? Assignee { get; set; }
    public User CreatedBy { get; set; } = null!;
    public int ProjectId { get; set; }
    public List<Comment> Comments { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}