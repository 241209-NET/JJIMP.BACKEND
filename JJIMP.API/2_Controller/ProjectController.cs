using JJIMP.API.DTO;
using JJIMP.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        try
        {
            var project = await _projectService.GetProjectById(id);
            return Ok(project);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/Project
    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDTO projectDto)
    {
        try
        {
            var project = await _projectService.CreateProject(projectDto);
            return Ok(project);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    // PUT: api/Project
    [HttpPut]
    public async Task<IActionResult> UpdateProject(UpdateProjectDTO projectDto)
    {
        try
        {
            var project = await _projectService.UpdateProject(projectDto);
            return Ok(project);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
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
        try
        {
            var project = await _projectService.AddUserToProject(id, userId);
            return Ok(project);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}/users/{userId}")]
    public async Task<IActionResult> RemoveUserFromProject(int id, int userId)
    {
        try
        {
            var project = await _projectService.RemoveUserFromProject(id, userId);
            return Ok(project);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}
