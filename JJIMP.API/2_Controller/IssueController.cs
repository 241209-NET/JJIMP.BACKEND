using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class IssueController : ControllerBase
{
    private readonly IIssueService _issueService;
    private readonly ICommentService _commentService;

    public IssueController(IIssueService issueService, ICommentService commentService)
    {
        _issueService = issueService;
        _commentService = commentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetIssue(int id)
    {
        var issue = await _issueService.GetIssueById(id);
        return Ok(issue);
    }

    [HttpGet("{id}/comments")]
    public async Task<ActionResult> GetIssueComments(int id)
    {
        var comments = await _commentService.GetCommentsByIssueId(id);
        return Ok(comments);
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