using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class Project
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID;
    public string name;
    public string? description;
    public JsonContent? labels;

    [ForeignKey("User")]
    public int? userId;
    public DateTime? createdAt;
    public DateTime? updatedAt;

}