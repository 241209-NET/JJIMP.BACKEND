using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public interface IIssueRepository
{
    Task<Issue?> GetIssueById(int id);
    Task<Issue> CreateIssue(Issue issue);
    Task<Issue?> UpdateIssue(Issue issue);
    Task<Issue?> DeleteIssue(int id);
    Task<IEnumerable<Issue>> GetAllIssues();
}
