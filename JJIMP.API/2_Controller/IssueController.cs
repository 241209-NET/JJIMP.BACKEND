using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class IssueController : ControllerBase
{
    private readonly IIssueService _issueService;

    public IssueController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetIssue(int id)
    {
        var issue = await _issueService.GetIssueById(id);
        return Ok(issue);
    }

    [HttpPost]
    public async Task<ActionResult> CreateIssue(CreateIssueDTO issueDTO)
    {
        var issue = await _issueService.CreateIssue(issueDTO);
        return Ok(issue);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateIssue(UpdateIssueDTO issueDTO)
    {
        var issue = await _issueService.UpdateIssue(issueDTO);
        return Ok(issue);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIssue(int id)
    {
        var issue = await _issueService.DeleteIssue(id);
        return Ok(issue);
    }
}