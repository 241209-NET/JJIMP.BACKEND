using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IIssueService _issueService;
    private readonly IUserService _userService;

    public ProjectController(IProjectService projectService, IIssueService issueService, IUserService userService)
    {
        _projectService = projectService;
        _issueService = issueService;
        _userService = userService;
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetProjectUsers(int id)
    {
        var issues = await _userService.GetUsersByProjectId(id);
        return Ok(issues);
    }

    // GET: api/Project
    [HttpGet("{id}/issues")]
    public async Task<IActionResult> GetProjectIssues(int id)
    {
        var issues = await _issueService.GetIssuesByProjectId(id);
        return Ok(issues);
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projectService.GetProjectById(id);
        return Ok(project);
    }

    // POST: api/Project
    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDTO projectDto)
    {
        var createdProject = await _projectService.CreateProject(projectDto);
        return Ok(createdProject);
    }

    // PUT: api/Project
    [HttpPut]
    public async Task<IActionResult> UpdateProject(UpdateProjectDTO projectDto)
    {
        var updatedProject = await _projectService.UpdateProject(projectDto);
        return Ok(updatedProject);
    }

    // DELETE: api/Project/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var deletedProject = await _projectService.DeleteProject(id);
        return Ok(deletedProject);
    }

    [HttpPost("{id}/users/{userId}")]
    public async Task<IActionResult> AddUserToProject(int id, int userId)
    {
        var project = await _projectService.AddUserToProject(id, userId);
        return Ok(project);
    }

    [HttpDelete("{id}/users/{userId}")]
    public async Task<IActionResult> RemoveUserFromProject(int id, int userId)
    {
        var project = await _projectService.RemoveUserFromProject(id, userId);
        return Ok(project);
    }
}
