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
            .Projects.Select(p => new Project
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ProjectManager = new User
                {
                    Id = p.ProjectManager.Id,
                    Name = p.ProjectManager.Name,
                    Email = p.ProjectManager.Email,
                    Password = null!,
                },
                Users = p
                    .Users.Select(u => new User
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        Password = null!,
                    })
                    .ToList(),
                Issues = p
                    .Issues.Select(i => new Issue
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Description = i.Description,
                        Status = i.Status,
                        CreatedAt = i.CreatedAt,
                        UpdatedAt = i.UpdatedAt,
                    })
                    .ToList(),
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
            })
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

    public async Task<Project?> UpdateProject(Project projectToUpdate)
    {
        var project = await _dbContext.Projects.FindAsync(projectToUpdate.Id);
        if (project == null)
        {
            return null;
        }
        if (projectToUpdate.Name != null)
        {
            project.Name = projectToUpdate.Name;
        }
        if (projectToUpdate.Description != null)
        {
            project.Description = projectToUpdate.Description;
        }
        if (projectToUpdate.ProjectManagerId != 0)
        {
            var user =
                await _dbContext.Users.FindAsync(projectToUpdate.ProjectManagerId)
                ?? throw new ArgumentException("User not found");
            project.ProjectManager = user;
        }
        project.UpdatedAt = DateTime.Now;
        var updatedProject = _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();
        return updatedProject.Entity;
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
}
