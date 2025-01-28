using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetProjectsByUserId(int userId);
    Task<Project?> GetProjectById(int projectId);
    Task<Project> CreateProject(Project project);
    Task<Project> UpdateProject(Project project);
    Task<Project?> DeleteProject(int projectId);
    Task<Project?> AddUserToProject(int projectId, int userId);
    Task<Project?> RemoveUserFromProject(int projectId, int userId);
}