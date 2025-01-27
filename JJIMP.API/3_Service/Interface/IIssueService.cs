using JJIMP.API.DTO;

namespace JJIMP.API.Service;

public interface IIssueService
{
    Task<IEnumerable<IssueOutDTO>> GetIssuesByProjectId(int issueId);
    Task<IssueOutDTO> GetIssueById(int id);
    Task<IssueOutDTO> CreateIssue(CreateIssueDTO issueDTO);
    Task<IssueOutDTO> UpdateIssue(UpdateIssueDTO issueDTO);
    Task<IssueOutDTO> DeleteIssue(int id);
}