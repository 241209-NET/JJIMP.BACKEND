using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public enum StatusEnum
{

}

public class Issue
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id;
    public required string title;
    public string? description;
    public JsonContent? labels;
    public StatusEnum? status;
    public DateOnly? deadline;
    [Timestamp]
    public DateTime? createdAt;
    [Timestamp]
    public DateTime? updatedAt;
    [ForeignKey("User")]
    public int? assignee;
    [ForeignKey("User")]
    public int? createdBy;
    [ForeignKey("Project")]
    public int? projectId;
    [ForeignKey("Comment")]
    public int[]? comments;

}