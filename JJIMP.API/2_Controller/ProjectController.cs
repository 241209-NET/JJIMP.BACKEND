using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

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

    // GET: api/Project
    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllProjects();
        return Ok(projects);
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
    public async Task<IActionResult> CreateProject(ProjectInDTO projectDto)
    {
        var createdProject = await _projectService.CreateProject(projectDto);
        return Ok(createdProject);
    }

    // PUT: api/Project
    [HttpPut]
    public async Task<IActionResult> UpdateProject(ProjectInDTO projectDto)
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
}
