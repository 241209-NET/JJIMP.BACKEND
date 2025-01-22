using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id;
    public string name;
    public string? email;
    public string? password;
    public DateTime? lastActivity;
    public DateTime? createdAt;
    public DateTime? updatedAt;
}