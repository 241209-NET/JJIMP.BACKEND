namespace JJIMP.API.DTO;

public class ProjectInDTO
{
    // This property helps identify the project during update.
    public int? Id { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }

    // May want to assign or reassign a ProjectManager.
    public int ProjectManagerId { get; set; }
    // Add a list of user IDs if needed.
    // public List<int> UserIds { get; set; } = new List<int>();
}

public class ProjectOutDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ProjectManagerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Optionally include related data,->  list of user IDs or project issues, add here.
    // public List<int> UserIds { get; set; } = new List<int>();
    // public List<int> IssueIds { get; set; } = new List<int>();
}
