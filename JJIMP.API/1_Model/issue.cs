using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public enum StatusEnum {

}

public class Issue
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id;
    public string title;
    public string? description;
    public JsonContent? labels;
    public StatusEnum? status;
    public DateTime? deadline;
    public DateTime? createdAt;
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