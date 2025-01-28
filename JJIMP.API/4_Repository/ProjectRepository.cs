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

    public async Task<Project?> GetProjectById(int projectId)
    {
        return await _dbContext.Projects
            .Include(p => p.Users)
            .Include(p => p.Issues)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<Project> CreateProject(Project project)
    {
        var user = await _dbContext.Users.FindAsync(project.ProjectManagerId) ?? throw new ArgumentException("User not found");
        project.Users.Add(user);
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

    public async Task<Project?> AddUserToProject(int projectId, int userId)
    {
        var project = await _dbContext.Projects.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
        var user = await _dbContext.Users.FindAsync(userId);
        if (project == null || user == null)
        {
            return null;
        }

        project.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> RemoveUserFromProject(int projectId, int userId)
    {
        var project = await _dbContext.Projects.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
        var user = await _dbContext.Users.FindAsync(userId);
        if (project == null || user == null)
        {
            return null;
        }

        project.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return project;
    }
}