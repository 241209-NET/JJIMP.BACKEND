using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class Comment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Content { get; set; }
    // Relations
    public int PostedById { get; set; }
    public User PostedBy { get; set; } = null!;
    public int IssueId { get; set; }
    public Issue Issue { get; set; } = null!;
    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
