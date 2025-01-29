using System.ComponentModel.DataAnnotations;
using JJIMP.API.Model;

namespace JJIMP.API.DTO;

public class CreateCommentDTO
{
    [Required]
    public required string Content { get; set; }

    [Required]
    public required int PostedById { get; set; }

    [Required]
    public required int IssueId { get; set; }
}

public class UpdateCommentDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Content { get; set; }
}

public class CommentOutDTO
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public User PostedBy { get; set; } = null!;
    public int IssueId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
