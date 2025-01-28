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
        var issue = await _issueRepository.GetIssueById(issueId) ?? throw new ArgumentException("Issue not found");
        return _mapper.Map<IssueOutDTO>(issue);
    }
    
    public async Task<IssueOutDTO> CreateIssue(CreateIssueDTO issueDTO)
    {
        var issue = _mapper.Map<Issue>(issueDTO);
        var createdIssue = await _issueRepository.CreateIssue(issue);
        return _mapper.Map<IssueOutDTO>(createdIssue);
    }

    public async Task<IssueOutDTO> UpdateIssue(UpdateIssueDTO issueDTO)
    {
        var issue = _mapper.Map<Issue>(issueDTO);
        var issueToUpdate = await _issueRepository.GetIssueById(issue.Id) ?? throw new ArgumentException("Issue not found");
        // Assign the new values to the issueToUpdate
        if (issue.Title != null)
        {
            issueToUpdate.Title = issue.Title;
        }
        if (issue.Description != null)
        {
            issueToUpdate.Description = issue.Description;
        }
        if (issue.Deadline != null)
        {
            issueToUpdate.Deadline = issue.Deadline;
        }
        if (issue.AssigneeId != null)
        {
            issueToUpdate.AssigneeId = issue.AssigneeId;
        }
        issueToUpdate.Status = issue.Status;
        // Update the issue
        var updatedIssue = await _issueRepository.UpdateIssue(issueToUpdate);
        return _mapper.Map<IssueOutDTO>(updatedIssue);
    }

    public async Task<IssueOutDTO> DeleteIssue(int issueId)
    {
        var deletedIssue = await _issueRepository.DeleteIssue(issueId);
        return _mapper.Map<IssueOutDTO>(deletedIssue);
    }
}