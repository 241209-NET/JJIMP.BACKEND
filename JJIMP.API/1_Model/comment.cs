using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJIMP.API.Model;

public class Comment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id;
    public required string content;
    [Timestamp]
    public DateTime? createdAt;
    [Timestamp]
    public DateTime? updatedAt;
    [ForeignKey("User")]
    public int? postedBy;
    [ForeignKey("Issue")]
    public int? issueID;

}