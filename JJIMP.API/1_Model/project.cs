using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class Project
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id;
    public required string name;
    public string? description;
    public JsonContent? labels;

    [ForeignKey("User")]
    public int? userId;
    [Timestamp]
    public DateTime? createdAt;
    [Timestamp]
    public DateTime? updatedAt;

}