using JJIMP.API.Model;

namespace JJIMP.API.DTO;

public class CreateProjectDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public int ProjectManagerId { get; set; }
}

public class UpdateProjectDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? ProjectManagerId { get; set; }
    public List<int>? UserIds { get; set; }
}

public class ProjectOutDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public User ProjectManager { get; set; } = null!;
    public List<User> Users { get; set; } = [];
    public List<Issue> Issues { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Optionally include related data,->  list of user IDs or project issues, add here.
    // public List<int> UserIds { get; set; } = new List<int>();
    // public List<int> IssueIds { get; set; } = new List<int>();
}
