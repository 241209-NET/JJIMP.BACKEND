using JJIMP.API.Data;
using JJIMP.API.Model;
using Microsoft.EntityFrameworkCore;

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
        return await _dbContext
            .Projects.Include(p => p.ProjectManager)
            .Include(p => p.Users)
            .Include(p => p.Issues)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<Project> CreateProject(Project project)
    {
        var user =
            await _dbContext.Users.FindAsync(project.ProjectManagerId)
            ?? throw new ArgumentException("User not found");
        project.Users.Add(user);
        project.CreatedAt = DateTime.Now;
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> UpdateProject(Project project, List<int>? userIds)
    {
        var projectToUpdate = await _dbContext
            .Projects.Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == project.Id);

        if (projectToUpdate == null)
        {
            return null;
        }

        if (project.Name != null)
        {
            projectToUpdate.Name = project.Name;
        }
        if (project.Description != null)
        {
            projectToUpdate.Description = project.Description;
        }
        if (project.ProjectManagerId != 0)
        {
            var user =
                await _dbContext.Users.FindAsync(project.ProjectManagerId)
                ?? throw new ArgumentException("Project Manager not found");
            projectToUpdate.ProjectManager = user;
        }

        //  Handle updating assigned users
        if (userIds != null)
        {
            var users = await _dbContext.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();

            projectToUpdate.Users = users; // Update users assigned to project
        }

        projectToUpdate.UpdatedAt = DateTime.UtcNow;
        _dbContext.Projects.Update(projectToUpdate);
        await _dbContext.SaveChangesAsync();
        return projectToUpdate;
    }

    public async Task<Project?> DeleteProject(int projectId)
    {
        var project = await _dbContext
            .Projects.Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
        {
            return null;
        }

        //  removing user associations
        project.Users.Clear();
        await _dbContext.SaveChangesAsync(); // saving changes before deleting the project

        // delete the project
        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();

        return project;
    }

    public async Task<Project?> AddUserToProject(int projectId, int userId)
    {
        var project = await _dbContext
            .Projects.Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == projectId);
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
        var project = await _dbContext
            .Projects.Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == projectId);
        var user = await _dbContext.Users.FindAsync(userId);
        if (project == null || user == null)
        {
            return null;
        }

        project.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _dbContext
            .Projects.Include(p => p.ProjectManager)
            .Include(p => p.Users)
            .ToListAsync();
    }
}
