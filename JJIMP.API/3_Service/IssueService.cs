using AutoMapper;
using JJIMP.API.DTO;
using JJIMP.API.Model;
using JJIMP.API.Repository;

namespace JJIMP.API.Service;

public class IssueService : IIssueService
{
    private readonly IIssueRepository _issueRepository;
    private readonly IMapper _mapper;

    public IssueService(IIssueRepository issueRepository, IMapper mapper)
    {
        _issueRepository = issueRepository;
        _mapper = mapper;
    }

    public async Task<IssueOutDTO> GetIssueById(int issueId)
    {
        var issue =
            await _issueRepository.GetIssueById(issueId)
            ?? throw new ArgumentException("Issue not found");
        return _mapper.Map<IssueOutDTO>(issue);
    }

    public async Task<IssueOutDTO> CreateIssue(CreateIssueDTO issueDTO)
    {
        var issue = _mapper.Map<Issue>(issueDTO);
        var createdIssue =
            await _issueRepository.CreateIssue(issue)
            ?? throw new ArgumentException("Issue not created");
        return _mapper.Map<IssueOutDTO>(createdIssue);
    }

    public async Task<IssueOutDTO> UpdateIssue(UpdateIssueDTO issueDTO)
    {
        var issueToUpdate = _mapper.Map<Issue>(issueDTO);
        var updatedIssue =
            await _issueRepository.UpdateIssue(issueToUpdate)
            ?? throw new ArgumentException("Issue not found");
        return _mapper.Map<IssueOutDTO>(updatedIssue);
    }

    public async Task<IssueOutDTO> DeleteIssue(int issueId)
    {
        var deletedIssue = await _issueRepository.DeleteIssue(issueId);
        return _mapper.Map<IssueOutDTO>(deletedIssue);
    }
}
