using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class Comment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id;
    public string content;
    public DateTime? createdAt;
    public DateTime? updatedAt;
    [ForeignKey("User")]
    public int? postedBy;
    [ForeignKey("Issue")]
    public int? issueID;

}