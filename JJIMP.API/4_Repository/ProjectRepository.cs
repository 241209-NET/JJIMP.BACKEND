using Microsoft.EntityFrameworkCore;
using JJIMP.API.Data;
using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly JjimpContext _dbContext;

    public ProjectRepository(JjimpContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _dbContext.Projects.ToListAsync();
    }

    public async Task<Project?> GetProjectById(int projectId)
    {
        return await _dbContext.Projects.FindAsync(projectId);
    }

    public async Task<Project> CreateProject(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Project> UpdateProject(Project project)
    {
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> DeleteProject(int projectId)
    {
        var project = await _dbContext.Projects.FindAsync(projectId);
        if (project == null)
        {
            return null;
        }

        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();
        return project;
    }
}