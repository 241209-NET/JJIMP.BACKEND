using JJIMP.API.DTO;

namespace JJIMP.API.Service;

public interface IIssueService
{
    Task<IssueOutDTO> GetIssueById(int id);
    Task<IssueOutDTO> CreateIssue(CreateIssueDTO issueDTO);
    Task<IssueOutDTO> UpdateIssue(UpdateIssueDTO issueDTO);
    Task<IssueOutDTO> DeleteIssue(int id);
    Task<IEnumerable<IssueOutDTO>> GetAllIssues();
}
